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
			zXingScannerPage = new ZXingScannerPage(); //We will scan the barcodes with creating a new page with this
			raceClass = new RaceClass(nev, osztaly, DisplayPageAlert); // This object handles all the data 
			zXingScannerPage.OnScanResult += Onresult;
			
			Device.StartTimer(new TimeSpan(0, 0, 0, 0, 50), () =>
				  {
					  StopwatchLabel.Text = raceClass.GetTime();
					  return true;
				  });
		
			InitializeComponent();
		}
		/// <summary>
		/// This method cleans up the scanner, and calls the processing method of the raceclass
		/// </summary>
		/// <param name="result"></param>
		void Onresult(ZXing.Result result)
		{
			Navigation.PopModalAsync(true);
			raceClass.Process(result.Text);
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			if (Navigation.ModalStack.Count == 1) // The app crashes if the button is clicked twice
			{
				Navigation.PushModalAsync(zXingScannerPage, false);
			}

		}
		/// <summary>
		/// This method is here so that the <c>raceClass</c> can push an alert to the screen
		/// </summary>
		/// <param name="message"></param>
		/// <param name="failConditions"></param>
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