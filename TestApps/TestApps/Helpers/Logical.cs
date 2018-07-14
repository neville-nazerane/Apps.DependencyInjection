using System;
using System.Collections.Generic;
using System.Text;

namespace TestApps.Helpers
{
    class Logical
    {
        private readonly Listeners listeners;

        public Logical(Listeners listeners)
        {
            this.listeners = listeners;
        }

        public void Update()
        {
            listeners.ToShow.Data = listeners.MyData + " _Hello";
        }

    }
}
