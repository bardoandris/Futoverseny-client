using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;



namespace Futoverseny
{

	public partial class App : Application
	{
		static INotificationManager notificationManager;
		static HttpClient client = new HttpClient();
		public App()
		{
			notificationManager = DependencyService.Get<INotificationManager>();
			notificationManager.NotificationReceived += (sender, eventArgs) =>
			{
				var evtData = (NotificationEventArgs)eventArgs;

			};
			InitializeComponent();

			MainPage = new MainPage();
			NavigationPage Race_Page = new NavigationPage();
#if (DEBUG)
			notificationManager.ScheduleNotification("Test", "Test");
#endif
		}

		public static void TrySend(Data data)
		{
			string uri = $@"mailto:pinter.laszlo@crnl.hu?subject=Versenyeredmény&body={data.Mailify()}";
		
			
			Launcher.OpenAsync(new Uri(uri));
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
