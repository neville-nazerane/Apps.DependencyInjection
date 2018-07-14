using Apps.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApps.Helpers
{
    class Listeners : ListenersManager
    {

        public DataListener<string> MyData;

        public DataListener<string> ToShow;

        public Listeners(IInitializer initializer) : base(initializer)
        {
            MyData = Build();
            ToShow = Build();
        }
    }
}
