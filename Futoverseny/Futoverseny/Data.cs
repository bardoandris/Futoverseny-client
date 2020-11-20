using System;
using System.Collections.Generic;
using System.Text;

namespace Futoverseny
{
	class Data
	{
		public Data(string nev, string osztaly)
		{
			Nev = nev;
			Osztaly = osztaly;
		}
		public string Nev { private get; set; }
		public string Osztaly { private get; set; }
		public DateTime Ido { private get; set; }

	}
}
