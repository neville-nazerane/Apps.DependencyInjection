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

        internal IInitializer Initializer { get; set; }

        internal DataListener()
        {

        }

        Action<TData> onSet;
        public Action<TData> OnSet {
            set
            {
                onSet = value;
                if (Data != null) onSet(Data);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="getDefault"></param>
        /// <param name="addToInitializer"></param>
        public void SetDefaultAsync(Func<Task<TData>> getDefault, bool addToInitializer = true)
        {
            this.getDefault = getDefault;
            if (addToInitializer)
                Initializer.Add(async () => await LoadIfConfiguredAsync());
        }

        /// <summary>
        /// Loads if a default delegate was set using the 
        /// SetDefault function
        /// </summary>
        public async Task LoadAsync()
        {
            if (getDefault != null) Set(await getDefault());
        }

        /// <summary>
        /// Loads only if method setter is configured
        /// </summary>
        public async Task LoadIfConfiguredAsync()
        {
            if (IsConfigued) await LoadAsync();
        }

        public bool IsConfigued => onSet != null;

        public TData Data { get => _data; set => Set(value); }

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

        public static implicit operator TData(DataListener<TData> dataListener)
            => dataListener.Data;

        public static implicit operator DataListener<TData>(DataListenerImplicitBuilder builder) 
            => new DataListener<TData> { Initializer = builder.Initializer };

        #endregion

    }

    public class DataListenerImplicitBuilder {

        internal IInitializer Initializer { get; set; }

        internal DataListenerImplicitBuilder()
        {

        }

    }

}
