using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace Sodao.FastSocket.Server
{
    /// <summary>
    /// Socket server manager.
    /// </summary>
    public class SocketServerManager
    {
        #region Private Members
        private static readonly List<SocketBase.IHost> _listHosts = new List<SocketBase.IHost>();
        #endregion

        #region Static Methods
        /// <summary>
        /// 初始化Socket Server
        /// </summary>
        public static void Init()
        {
            Init("socketServer");
        }
        /// <summary>
        /// 初始化Socket Server
        /// </summary>
        /// <param name="sectionName"></param>
        public static void Init(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName)) throw new ArgumentNullException("sectionName");
            Init(ConfigurationManager.GetSection(sectionName) as Config.SocketServerConfig);
        }

        public static void InitServer(string ip, int port)
        {
            Config.Server serverConfig = new Config.Server();

            //         <server name="binary"
            //          port="12000"
            //          socketBufferSize="8192"
            //          messageBufferSize="8192"
            //          maxMessageSize="102400"
            //          maxConnections="20000"
            //          serviceType="ServerManage.WorkService, ServerManage"
            //          protocol="asyncBinary"/>
            //</servers>

            IPAddress ipaddress = IPAddress.Parse(ip);

            serverConfig.Name = "binary";
            serverConfig.Port = port;
            serverConfig.SocketBufferSize = 8192;
            serverConfig.MaxMessageSize = 102400;
            serverConfig.MessageBufferSize = 8192;
            serverConfig.MaxConnections = 20000;
            serverConfig.ServiceType = "ServerManage.WorkService, ServerManage";
            serverConfig.Protocol = "asyncBinary";

            var objProtocol = GetProtocol(serverConfig.Protocol);
            if (objProtocol == null) throw new InvalidOperationException("protocol");

            //init custom service
            var tService = Type.GetType(serverConfig.ServiceType, false);
            if (tService == null) throw new InvalidOperationException("serviceType");

            var serviceFace = tService.GetInterface(typeof(ISocketService<>).Name);
            if (serviceFace == null) throw new InvalidOperationException("serviceType");

            var objService = Activator.CreateInstance(tService);
            if (objService == null) throw new InvalidOperationException("serviceType");

            //init host.
            var host = Activator.CreateInstance(typeof(SocketServer<>).MakeGenericType(
                serviceFace.GetGenericArguments()),
                objService,
                objProtocol,
                serverConfig.SocketBufferSize,
                serverConfig.MessageBufferSize,
                serverConfig.MaxMessageSize,
                serverConfig.MaxConnections) as BaseSocketServer;

            host.AddListener(serverConfig.Name, new IPEndPoint(ipaddress, serverConfig.Port));

            _listHosts.Add(host);
        }

        /// <summary>
        /// 初始化Socket Server
        /// </summary>
        /// <param name="config"></param>
        static public void Init(Config.SocketServerConfig config)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (config.Servers == null) return;

            foreach (Config.Server serverConfig in config.Servers)
            {
                //inti protocol
                var objProtocol = GetProtocol(serverConfig.Protocol);
                if (objProtocol == null) throw new InvalidOperationException("protocol");

                //init custom service
                var tService = Type.GetType(serverConfig.ServiceType, false);
                if (tService == null) throw new InvalidOperationException("serviceType");

                var serviceFace = tService.GetInterface(typeof(ISocketService<>).Name);
                if (serviceFace == null) throw new InvalidOperationException("serviceType");

                var objService = Activator.CreateInstance(tService);
                if (objService == null) throw new InvalidOperationException("serviceType");

                //init host.
                var host = Activator.CreateInstance(typeof(SocketServer<>).MakeGenericType(
                    serviceFace.GetGenericArguments()),
                    objService,
                    objProtocol,
                    serverConfig.SocketBufferSize,
                    serverConfig.MessageBufferSize,
                    serverConfig.MaxMessageSize,
                    serverConfig.MaxConnections) as BaseSocketServer;

                host.AddListener(serverConfig.Name, new IPEndPoint(IPAddress.Any, serverConfig.Port));

                _listHosts.Add(host);
            }
        }
        /// <summary>
        /// get protocol.
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns></returns>
        static public object GetProtocol(string protocol)
        {
            switch (protocol)
            {
                case Protocol.ProtocolNames.AsyncBinary: return new Protocol.AsyncBinaryProtocol();
                case Protocol.ProtocolNames.Thrift: return new Protocol.ThriftProtocol();
                case Protocol.ProtocolNames.CommandLine: return new Protocol.CommandLineProtocol();
            }
            return Activator.CreateInstance(Type.GetType(protocol, false));
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        static public void Start()
        {
            foreach (var server in _listHosts) server.Start();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        static public void Stop()
        {
            foreach (var server in _listHosts) server.Stop();
        }
        #endregion
    }
}