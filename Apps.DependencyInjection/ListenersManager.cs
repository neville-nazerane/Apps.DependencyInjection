using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection
{
    public class ListenersManager
    {

        
        private readonly IInitializer initializer;

        public ListenersManager(IInitializer initializer)
        {
            this.initializer = initializer;
        }

        protected DataListenerImplicitBuilder Build()
            => new DataListenerImplicitBuilder {
                Initializer = initializer
            };

    }
}
