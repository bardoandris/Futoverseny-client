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
			var message = new EmailMessage
			{
				Subject = "Versenyeredmény",
				Body = data.Mailify(),
				To = { "pinter.laszlo@crnl.hu" },
				BodyFormat = EmailBodyFormat.PlainText
				
			};
			string uri = $@"mailto:pinter.laszlo@crnl.hu?subject=Versenyeredmény&body={data.Mailify()}";

			Email.ComposeAsync(message);
			//Launcher.OpenAsync(new Uri(uri));
			//Device.OpenUri(new Uri($@"mailto:pinter.laszlo@crnl.hu?subject=Versenyeredmény&body={data.Mailify()}"));
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
