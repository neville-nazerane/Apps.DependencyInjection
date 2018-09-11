using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DependencyInjection
{
    public interface IDataInitializer<TManager>
        where TManager : ListenersManager
    {

        void Add<TData> (
                    Func<TManager, DataListener<TData>> getData,
                    Func<TManager, Task<TData>> getDefault, 
                    bool autoInitialize = true
            );

        void Init(TManager manager);

    }
}
