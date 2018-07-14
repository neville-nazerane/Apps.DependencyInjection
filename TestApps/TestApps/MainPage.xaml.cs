using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Apps.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TestApps.Helpers;

namespace TestApps
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            IServiceProvider services = new ServiceCollection()
                                        .AddListenersManager<Listeners>()
                                        .AddScoped<Logical>()
                                        .BuildServiceProvider();

            var listen = services.GetService<Listeners>();
            var logic = services.GetService<Logical>();

            listen.ToShow.OnSet = s => printer.Text = s;

            info.TextChanged += delegate {
                listen.MyData.Data = info.Text;
                logic.Update();
            };

		}
	}
}
