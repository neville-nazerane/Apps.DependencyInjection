﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.Forms.DependencyInjection
{
    public class NavigationOptions
    {

        public bool SelfNavigate { get; set; }

        internal Action<IServiceProvider> Passable { get; set; }

        public void PassData<TData>(TData data) 
            => Passable = p => p.GetService<Passable<TData>>().Data = data;

        public void PassData<TData>(Passable<TData> passableData)
            => PassData(passableData.Data);

        internal NavigationOptions()
        {
            SelfNavigate = true;
        }

    }
}
