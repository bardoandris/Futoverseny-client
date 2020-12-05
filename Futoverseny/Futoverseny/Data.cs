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

		public string Mailify()
		{
			return "Név:" + "%20" + Nev.Replace(" ", "%20") + "%0D%0A" +
			"Osztály:" + "%20" + Osztaly.Replace(" ","%20") + "%0D%0A" +
			"Idő:" + "%20" + Ido.ToString(@"mm\:ss\:ff");
		}

	}
}
