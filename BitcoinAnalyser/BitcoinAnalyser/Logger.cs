using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalyser
{
	public enum LogType
	{
		EUR, BTC, PERC
	}

	static class Logger
	{
		static string path = "C:/bitcoins/Log/";
		static string fileName;

		static public void Init()
		{
			System.IO.Directory.CreateDirectory(path);

			fileName = "log_" + DateTime.Now.Date.ToShortDateString().Replace('.', '_') + ".txt";


			Log("Application started!", true);
		}

		static public void Log(string txt, bool time = false)
		{
			string output = (time ? (DateTime.Now.ToString() + ": ") : (""+'\t')) + txt;
			System.IO.StreamWriter writer = System.IO.File.AppendText(path + fileName);

			writer.WriteLine(output);

			writer.Close();

		}
		static public void Log(string subtitle, string txt, bool time = true)
		{
			Log(subtitle + ": " + txt, time);
		}
		static public void LogVal(string name, float val, LogType type)
		{
			string value = "";
			switch (type)
			{
				case LogType.BTC:
					{
						value = val.ToString("N5") + " BTC";
						break;
					}
				case LogType.EUR:
					{
						value = val.ToString("N2") + " €";
						break;
					}
				case LogType.PERC:
					{
						value = (val * 100).ToString("N1") + " %";
						break;
					}
			}

			Log('\t' + name + ": " + value);
		}
		static public void LogVal(string name, bool val)
		{
			Log('\t' + name + ": " + val.ToString());
		}

		static public void LogLine(int linefeeds = 0)
		{
			Log("__________________________________________");
			for (int i = 0; i < linefeeds; i++)
			{
				Log("");
			}

		}
		static public void LogLineSub()
		{
			Log("------------------------------------------");
		}

		static public void LogOffer(Record rec)
		{
			LogVal("express", rec.express);
			LogVal("min", rec.min, LogType.BTC);
			LogVal("max", rec.max, LogType.BTC);
			LogVal("price", rec.info.BuyPrice, LogType.EUR);
			LogVal("min. price", rec.min * rec.info.BuyPrice, LogType.EUR);
		}
	}
}
