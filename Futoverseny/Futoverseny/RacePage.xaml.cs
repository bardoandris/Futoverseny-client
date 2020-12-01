using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;




namespace Futoverseny
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class RacePage : ContentPage
	{
		ZXing.Net.Mobile.Forms.ZXingScannerPage zXingScannerPage;
		RaceClass raceClass;
		public RacePage(string nev, string osztaly)
		{
			zXingScannerPage = new ZXingScannerPage();
			raceClass = new RaceClass(nev, osztaly, DisplayPageAlert);
			zXingScannerPage.OnScanResult += Onresult;
			InitializeComponent();
		}

		void Onresult(ZXing.Result result)
		{
			Navigation.PopModalAsync(true);
			raceClass.Process(result.Text);
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			if (Navigation.ModalStack.Count == 1)
			{
				await Navigation.PushModalAsync(zXingScannerPage, false);
			}

		}

		void DisplayPageAlert(string message, RaceClass.FailConditions? failConditions)
		{

			if (failConditions != null)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					DisplayAlert("Nem érvényes kód", message, "OK");
				});
				if (failConditions == RaceClass.FailConditions.WrongOrder)
				{
					Navigation.PopModalAsync();
				}
			}
		}




	}
}