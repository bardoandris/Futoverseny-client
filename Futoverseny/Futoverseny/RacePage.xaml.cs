using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;


namespace Futoverseny
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	
	public partial class RacePage : ContentPage
	{
		ZXing.Net.Mobile.Forms.ZXingScannerPage zXingScannerPage;
		Stopwatch stopwatch = new Stopwatch();
		public RacePage()
		{
			InitializeComponent();
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			zXingScannerPage = new ZXingScannerPage();
			await Navigation.PushModalAsync(zXingScannerPage);
			zXingScannerPage.OnScanResult += ProcessResult;
			
		}

		async void ProcessResult(ZXing.Result result)
		{
			Device.BeginInvokeOnMainThread(delegate { DisplayAlert("QR", result.Text, "ok"); });
			await Navigation.PopModalAsync(true);		
		}
		
		
	}
}