using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DependencyInjection
{
    class DataInitializer<TManager> : IDataInitializer<TManager>
        where TManager : ListenersManager
    {

        internal List<Action<TManager>> assigners;
        internal List<Action<TManager>> initializers;

        public DataInitializer()
        {
            assigners = new List<Action<TManager>>();
            initializers = new List<Action<TManager>>();
        }

        public void Add<TData> (
                Func<TManager, DataListener<TData>> getData, 
                Func<Task<TData>> getDefault, 
                bool autoInitialize = true
            )
        {
            assigners.Add(m => getData(m).SetDefault(getDefault));
            if (autoInitialize)
                initializers.Add(async m => await getData(m).LoadAsync());
        }

        public void Init(TManager manager)
        {
            foreach (var assign in assigners) assign(manager);
            foreach (var init in initializers) init(manager);
        }



    }
}
