using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Text.RegularExpressions;


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
			Navigation.PushModalAsync(new RacePage(NameEntry.Text, ClassEntry.Text));
		}

		private void ZXingScannerView_OnScanResult(ZXing.Result result)
		{
			

			/*Device.BeginInvokeOnMainThread(() =>
			{
				DisplayAlert("QR", result.Text, "OK");
			});*/
			
		}
	}
}
