using System;

namespace Futoverseny
{
	public class Data
	{
		public Data(string nev, string osztaly)
		{
			Nev = nev;
			Osztaly = osztaly;
		}
		public string Nev {  get; set; }
		public string Osztaly {  get; set; }
		public TimeSpan Ido { get; set; }

		public override string ToString()
		{
			return Nev + "\n" + Osztaly + "\n" + Ido.ToString("mm:ss:.ff");
		}

	}
}
