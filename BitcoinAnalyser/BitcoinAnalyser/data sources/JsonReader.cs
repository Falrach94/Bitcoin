using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalyser
{
	class JsonReader
	{
		List<TradeData> _data = new List<TradeData>();

		public static JsonReader that = new JsonReader();

		private JsonReader() { }

		bool ReadNext(string txt, int i_in, out int i, out string t)
		{
			t = "";

			for (i = i_in; i < txt.Length; i++)
			{
				if (txt[i] == '[' || txt[i] == ']' || txt[i] == ',')
				{
					continue;
				}
				else
				{
					break;
				}
			}

			if (txt.Length == i)
			{
				return false;
			}

			for (; i < txt.Length; i++)
			{
				if (txt[i] == ']' || txt[i] == ',')
				{
					break;
				}
				else
				{
					if (txt[i] == '.')
					{
						t += ",";
					}
					else
					{
						t += txt[i];
					}
				}
			}

			return true;
		}
		bool ReadNextFloat(string txt, int i_in, out int i, out float val)
		{
			string t;
			ReadNext(txt, i_in, out i, out t);

			val = -1;
			return float.TryParse(t, out val);
		}

		void LoadJsonFile(string file)
		{
			string data = System.IO.File.ReadAllText(file);

			LoadJson(data);
		}
		void LoadJson(string data)
		{
			int i = 0;

			_data.Clear();

			while (i < data.Length)
			{
				TradeData newSheet = new TradeData();
				float val;
				string t;
				if (!ReadNext(data, i, out i, out t))
				{
					break;
				}

				long time_js = 0;
				long.TryParse(t, out time_js);

				newSheet.date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time_js).ToLocalTime();

				for (int j = 0; j < 9; j++)
				{
					ReadNextFloat(data, i, out i, out newSheet.data[j]);
				}

				_data.Add(newSheet);
			}
		}


		public void Reload3MonthJson()
		{
			System.Net.WebRequest request = System.Net.WebRequest.Create("https://www.bitcoin.de/json/chart/stats_hourly_btceur_statistics_3month.json");
			System.Net.WebResponse response = request.GetResponse();
			var stream = response.GetResponseStream();

			System.IO.StreamReader reader = new System.IO.StreamReader(stream);

			LoadJson(reader.ReadToEnd());
		}
		
		public bool GetData(DateTime earliest, DateTime latest, out List<TradeData> list)
		{
			list = new List<TradeData>();

			if(_data.Count == 0)
			{
				Reload3MonthJson();
			}

			int i = _data.Count - 1;

			while(i >= 0 && _data[i].date > latest.AddMinutes(15))
			{
				i--;
			}
			while (i >= 0 && _data[i].date >= earliest.AddMinutes(-15))
			{
				list.Add(_data[i--]);
			}

			return list.Count != 0;
		}
				
		public void GetMinMax(ref float min, ref float max, DateTime earliest, DateTime latest, int type)
		{
			if (_data.Count == 0) return;

			List<TradeData> list;
			if(GetData(earliest, latest, out list))
			{
				foreach(var v in list)
				{
					if(min == -1) min = v.data[type];
					if(max == -1) max = v.data[type];
				
					if (v.data[type] > max)
					{
						max = v.data[type];
					}
					if (v.data[type] < min)
					{
						min = v.data[type];
					}
				}
			}
		}
	
		public float GetCurrentValue(int type)
		{
			if(_data.Count == 0)
			{
				Reload3MonthJson();
			}

			return _data[_data.Count - 1].data[type];
		}
	}
}
