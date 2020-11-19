using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace Futoverseny
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			Scanner.IsScanning = true;
			Navigation.PushModalAsync(new RacePage());
		}

		private void ZXingScannerView_OnScanResult(ZXing.Result result)
		{
			Scanner.IsScanning = false;

			/*Device.BeginInvokeOnMainThread(() =>
			{
				DisplayAlert("QR", result.Text, "OK");
			});*/
			
		}
	}
}
