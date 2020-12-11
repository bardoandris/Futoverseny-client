using System;
using Xamarin.Forms;


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
			if (Navigation.ModalStack.Count == 0) // This so the user can't click the button twice, leading to a crash 
			{
				Navigation.PushModalAsync(new RacePage(NameEntry.Text, ClassEntry.Text), false);
			} 
		}

		
	}
}
