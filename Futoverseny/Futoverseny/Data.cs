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
			return "Név: " + Nev.ToType() + "\n" +
			"Osztály: " + Osztaly.ToType() + "\n" +
			"Idő: " + Ido.ToString(@"mm\:ss\:ff");
		}

	}
}
public static class MyStringExtension
{
	public static string ToType(this string str) => str == null ? new String("".ToCharArray()) : str;
}
