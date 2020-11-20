using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;


namespace Futoverseny
{
	/// <summary>
	/// This Class encapsulates Data regarding the race, the methods required to process said codes,
	/// and sends it to a remote server
	/// </summary>
	class RaceClass
	{
		enum FailConditions
		{
			InvalidCode,
			WrongOrder
		}
		Stopwatch stopwatch;
		Dictionary<int, Action<string>> Actions;
		int StopCount, Currentstop;
		Action<string> action;
		Data data;
		bool Done = false;
		public RaceClass(string nev, string osztaly)
		{
			Actions.Add(0, StartRace); //Start Code
			Actions.Add(1, InRace); // Checks for route violation
			Actions.Add(2, EndRace); // Handles the sending of data
			data = new Data(nev, osztaly);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"> The Scan's result</param>
		/// <param name="OutData"> The Data object to be serialized</param>
		/// <returns>Returns wether the Race is done</returns>
		public bool Process(string code, out Data OutData)
		{
			Actions.TryGetValue(int.Parse(code.Substring(0, 1)), out action);
			action.Invoke(code);
			OutData = data;
			return Done;
		}
		
		void StartRace(string code)
		{
			if(!int.TryParse(code.Substring(1, 2), out StopCount))
			{
				FailureHandle(code, FailConditions.InvalidCode);
			}
			else
			{
				Currentstop = 1; 
			}
		}
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
						
				}
			}
		}
		void EndRace(string code)
		{

		}
		void FailureHandle(string code, FailConditions fail)
		{

		}
	}
}
