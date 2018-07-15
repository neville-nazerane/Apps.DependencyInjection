using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection
{
    public class ListenersManager
    {

        public ListenersManager()
        {

        }

        protected DataListenerImplicitBuilder Build()
            => new DataListenerImplicitBuilder {

            };

    }
}
