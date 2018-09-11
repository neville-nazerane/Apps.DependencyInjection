using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DependencyInjection
{
    public class DataListener<TData>
    {

        private TData _data;
        Func<Task<TData>> getDefault;

        internal DataListener()
        {

        }

        Action<TData> onSet;
        public Action<TData> OnSet {
            set
            {
                onSet = value;
                value?.Invoke(_data);
            }
        }

        Func<TData, TData> onGet;
        public Func<TData, TData> OnGet
        {
            set => onGet = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getDefault"></param>
        public void SetDefault(Func<Task<TData>> getDefault) => this.getDefault = getDefault;

        /// <summary>
        /// Loads if a default delegate was set using the 
        /// SetDefault function
        /// </summary>
        public async Task ReloadAsync()
        {
            if (getDefault != null) Set(await getDefault());
        }

        /// <summary>
        /// Loads only if method setter is configured
        /// </summary>
        public async Task ReloadIfConfiguredAsync()
        {
            if (IsConfigued) await ReloadAsync();
        }

        public bool IsConfigued => onSet != null;

        public TData Data
        {
            get => Get(_data);
            set => Set(value);
        }

        TData Get(TData data)
        {
            if (onGet == null) return _data;
            else return onGet(_data);
        }

        void Set(TData data)
        {
            _data = data;
            onSet?.Invoke(data);
        }

        public async Task<bool> SetIfConfiguredAsync(Func<Task<TData>> set)
        {
            bool can = IsConfigued;
            if (can) Data = await set();
            return can;
        }

        #region implicit_operators 

        public static implicit operator TData(DataListener<TData> dataListener) => dataListener.Data;

        public static implicit operator DataListener<TData>(DataListenerImplicitBuilder builder) 
            => new DataListener<TData> {  };

        #endregion

    }

    public class DataListenerImplicitBuilder {


        internal DataListenerImplicitBuilder()
        {

        }

    }

}
