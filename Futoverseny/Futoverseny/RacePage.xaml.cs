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
using Newtonsoft.Json;


namespace Futoverseny
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	
	public partial class RacePage : ContentPage
	{
		ZXing.Net.Mobile.Forms.ZXingScannerPage zXingScannerPage;
		RaceClass raceClass;
		Data finalData;
		string RaceResults;
		public RacePage(string nev, string osztaly)
		{
			zXingScannerPage = new ZXingScannerPage();
			raceClass = new RaceClass(nev, osztaly);
			zXingScannerPage.OnScanResult += (result) =>
			{
				Navigation.PopModalAsync(true);
				if (raceClass.Process(result.Text, out finalData))
				{
					
					
				} 

			};
			InitializeComponent();
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			
			await Navigation.PushModalAsync(zXingScannerPage);
			
			
		}

		
		
		
		
	}
}