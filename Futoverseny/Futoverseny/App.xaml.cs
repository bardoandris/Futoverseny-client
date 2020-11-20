using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Futoverseny
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			
			MainPage = new MainPage();
			NavigationPage Race_Page = new NavigationPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
