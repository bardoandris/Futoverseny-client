using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Xamarin.Forms;
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
			HttpResponseMessage httpResponse;
			client.BaseAddress = new Uri("https://bardodev.ddns.net/futoverseny/futoadatapi.php");
			var stringcontent = new StringContent(JsonConvert.SerializeObject(data), encoding: System.Text.Encoding.UTF8, "application/json");
			
			httpResponse = client.PostAsync(client.BaseAddress, stringcontent).Result;
			try
			{
				Debug.WriteLine((httpResponse = client.PostAsync(client.BaseAddress.ToString(),
															stringcontent).Result)
															.Content.ReadAsStringAsync().Result);
				httpResponse.EnsureSuccessStatusCode();
			}catch(HttpRequestException httpEx)
			{
				Device.StartTimer(new TimeSpan(0, 3, 0), () =>
				{
					try
					{
#if (DEBUG)
						Debug.WriteLine((httpResponse = client.PostAsync(client.BaseAddress.ToString(),
															stringcontent).Result)
															.Content.ReadAsStringAsync().Result);
#endif
#if (RELEASE)
						httpResponse = client.PostAsync(client.BaseAddress.ToString(),
															stringcontent).Result;
#endif
						httpResponse.EnsureSuccessStatusCode();
					}
					catch
					{
						return true;
					}
					if (httpResponse.Content.ToString() != "Data-OK")
					{
						return true;
					}
					notificationManager.ScheduleNotification("Sikeres Beküldés!",
						" Az eredményeid fel lettek töltve a szerverre!, " +
						"most már letörölheted az applikációt, ha szeretnéd!");

					return false;
				});
			}
			
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
