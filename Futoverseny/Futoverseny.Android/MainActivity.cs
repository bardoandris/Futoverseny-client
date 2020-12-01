using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Xamarin.Forms;

namespace Futoverseny.Droid
{
	[Activity(Label = "Futoverseny", LaunchMode = LaunchMode.SingleTop, Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			
			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			ZXing.Net.Mobile.Forms.Android.Platform.Init();
			LoadApplication(new App());
			CreateNotificationFromIntent(Intent);
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
		void CreateNotificationFromIntent(Intent intent)
		{
			if (intent?.Extras != null)
			{
				string title = intent.Extras.GetString(AndroidNotificationManager.TitleKey);
				string message = intent.Extras.GetString(AndroidNotificationManager.MessageKey);
				DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
			}
		}
		protected override void OnNewIntent(Intent intent)
		{
			CreateNotificationFromIntent(intent);
		}

	}
}