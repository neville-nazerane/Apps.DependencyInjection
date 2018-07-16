using Apps.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApps.Helpers
{
    class Listeners : ListenersManager
    {

        public DataListener<string> MyData { get; set; }

        public DataListener<string> ToShow { get; set; }

    }
}
