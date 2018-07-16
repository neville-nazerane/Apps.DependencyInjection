using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apps.DependencyInjection
{
    public class ListenersManager
    {
        
        public ListenersManager()
        {
            foreach (var p in GetType().GetProperties())
            {
                Type type = p.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DataListener<>))
                    p.SetValue(this, Activator.CreateInstance(type, true));
            }
        }

        protected DataListenerImplicitBuilder Build()
            => new DataListenerImplicitBuilder {

            };

    }
}
