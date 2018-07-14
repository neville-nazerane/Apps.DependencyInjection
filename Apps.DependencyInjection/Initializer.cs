using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.DependencyInjection
{
    public class Initializer : IInitializer
    {
        readonly List<Action> Actions;

        public Initializer()
        {
            Actions = new List<Action>();
        }

        public void Add(Action action) => Actions.Add(action);

        public void Init()
        {
            foreach (var action in Actions) action();
        }
    }
}
