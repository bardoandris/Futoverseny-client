using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace Futoverseny
{
	/// <summary>
	/// This Class encapsulates Data regarding the race, the methods required to process said codes,
	/// and sends it to a remote server
	/// </summary>
	class RaceClass
	{
		public enum FailConditions
		{
			InvalidCode,
			WrongOrder
		}
		Stopwatch stopwatch = new Stopwatch();
		
		int Currentstop = 1;
		Action<string, FailConditions?> Alert;
		Data data;
		
		public RaceClass(string nev, string osztaly, Action<string, FailConditions?> PageAction)
		{
			Alert = PageAction;
			data = new Data(nev, osztaly);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"> The Scan's result</param>
		/// <returns>Returns wether the Race is done</returns>
		public void Process(string code)
		{
			if (code.Contains("Rajt"))
			{
				StartRace(code);
			}
			else if (code.Contains("Cél"))
			{
				FinishRace(code);
			}
			else if (!code.Contains("Rajt") && !code.Contains("Cél"))
			{
				InRace(code);
			}	
		}

		/// <summary>
		/// This method sets initial race parameters
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void StartRace(string code)
		{
			stopwatch.Start();
		}
		/// <summary>
		/// This method advances the race forward, checks for order
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void InRace(string code)
		{
			int temp;
			if (int.TryParse(code.Substring(0, 1), out temp))
			{
				if (temp == Currentstop)
				{
					Currentstop++;

				}
				else
				{
					FailureHandle(code, FailConditions.WrongOrder);
				}
				
			}
			else
			{
				FailureHandle(code, FailConditions.InvalidCode);
			}
		}
		public string GetTime()
		{
			return stopwatch.Elapsed.ToString(@"mm\:ss\:ff");
		}
		/// <summary>
		/// This method Finalizes the time, and starts the method that will attempt to upload the result
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void FinishRace(string code)
		{
			if (int.Parse(code.Substring(0, 1)) == Currentstop)
			{
				data.Ido = stopwatch.Elapsed;
				stopwatch.Stop();
				App.TrySend(data);
				

			}
			else
			{

			}
		}
		/// <summary>
		/// This method invokes the parent page's Display method, so the app can display an error message
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		/// <param name="fail">The Type of failure</param>
		void FailureHandle(string code, FailConditions? fail)
		{
			if (fail == FailConditions.InvalidCode)
			{
				Alert("A kód nem része a futóversenynek, kérlek győződj meg róla, " +
					"hogy jó kódot olvastál-e be!", FailConditions.InvalidCode);
			}
			else
			{
				Alert("Ez a kód nem helyes sorrenben lett beolvasva, " +
					"a verseny meg van szakítva", FailConditions.WrongOrder);
			}
		}
	}
}
