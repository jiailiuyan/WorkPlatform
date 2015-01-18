using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlatformCommon.Plugin;

namespace ControlLib
{
    public class AddControlArgs : EventArgs
    {
        public IPluginObject PluginObject { get; set; }

    }

}
