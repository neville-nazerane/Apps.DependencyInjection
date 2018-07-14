using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection
{

    /// <summary>
    /// Controls a collection of delegates to be run after all 
    /// </summary>
    public interface IInitializer
    {

        void Add(Action action);

        void Init();

    }
}
