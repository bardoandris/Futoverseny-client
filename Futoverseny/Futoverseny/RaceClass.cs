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
		Dictionary<int, Action<string>> Actions;
		int StopCount;
		int Currentstop = 1;
		Action<string> action;
		Action<string, FailConditions?> Alert;
		Data data;
		
		public RaceClass(string nev, string osztaly, Action<string, FailConditions?> PageAction)
		{
			Actions = new Dictionary<int, Action<string>>();
			Actions.Add(0, StartRace); //Start Code
			Actions.Add(1, InRace); // Checks for route violation
			Actions.Add(2, Finishrace); // Handles the sending of data
			Alert = PageAction;
			data = new Data(nev, osztaly);
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"> The Scan's result</param>
		/// <param name="OutData"> The Data object to be serialized</param>
		/// <returns>Returns wether the Race is done</returns>
		public void Process(string code)
		{
			Actions.TryGetValue(int.Parse(code.Substring(0, 1)), out action);
			action.Invoke(code);


		}
		public void InitializeData(string nev, string osztaly)
		{

		}

		/// <summary>
		/// This method sets initial race parameters
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void StartRace(string code)
		{
			if (!int.TryParse(code.Substring(1, 2), out StopCount))
			{
				FailureHandle(code, FailConditions.InvalidCode);
			}
			else
			{
				stopwatch.Start();
			
				int.TryParse(code.Substring(1, 2), out StopCount);
			}
		}
		/// <summary>
		/// This method advances the race forward, checks for order
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void InRace(string code)
		{
			int temp;
			if (!int.TryParse(code.Substring(1, 2), out temp))
			{
				FailureHandle(code, FailConditions.InvalidCode);
			}
			else
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
		}
		public string GetTime()
		{
			return stopwatch.Elapsed.ToString(@"mm\:ss\:ff");
		}
		/// <summary>
		/// This method Finalizes the time, and starts the method that will attempt to upload the result
		/// </summary>
		/// <param name="code">The scanned Qr code</param>
		void Finishrace(string code)
		{
			if (Currentstop == StopCount & int.Parse(code.Substring(1, 2)) == Currentstop)
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
