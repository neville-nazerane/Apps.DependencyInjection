using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.DependencyInjection
{
    public class Passable<TData>
    {

        public TData Data { get; set; }

        public static implicit operator TData(Passable<TData> passable)
            => passable.Data;
        
    }

    public interface IAccepting<TData>
    {

        TData Passed { get; set; }

    }

}
