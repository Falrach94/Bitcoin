using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;

namespace BitcoinAnalyser
{
	//[1503853200000, date
	//	3754, open
	//	3754, high
	//	3630.2, low
	//	3717.88, close

	//	0.91087484, volume btc
	//	3363.06,  volume €
 
	//	3660.53, 3h av
	//	3659.58, 12h av
	//	3657.89] weighted av
	public enum TradeDataType
	{
		OPEN, HIGH, LOW, CLOSE, VOLUME_BTC, VOLUME_EUR, AVG_3H, AVG_12H, AVG_DAILY
	}


	public class TradeData
	{
		public DateTime date;

		public float[] data = new float[9];
		/*
		public float open, high, low, close;
		public float volumeBtc, volumeEur;
		public float avg3h, avg12h, avgDaily;*/
	}

	public class PeakInfo
	{
		public bool max;
		public float value;
		public int offset;
		public DateTime time;
	}

	class CourseAnalysator
	{
		/*
		DateTime _nextStamp = DateTime.Now;

		public int _anaTime_min = 100;

		public static CourseAnalysator that = new CourseAnalysator();

		private CourseAnalysator()
		{

		}

		float Avg(List<Record> list, int num)
		{
			if(num > list.Count)
			{
				num = list.Count;
			}
			if(num == 0)
			{
				return 0;
			}

			float sum = 0;
			for(int i = 0; i < num; i++)
			{
				sum += list[i].price;
			}
			return sum / (float)num;
		}
				
		public void GetMinMax(out float min, out float max, DateTime earliest, DateTime latest)
		{
			min = _data[_data.Count - 1 - from].data[(int)TradeDataType.HIGH];
			max = min;

			for (int i = _data.Count - 1 - from; i >= _data.Count - to; i--)
			{
				if (_data[i].data[(int)TradeDataType.HIGH] > max)
				{
					max = _data[i].data[(int)TradeDataType.HIGH];
				}
				if (_data[i].data[(int)TradeDataType.LOW] < min)
				{
					min = _data[i].data[(int)TradeDataType.LOW];
				}
			}
		}

		public void GetMinMax(ref float min, ref float max, DateTime earliest, DateTime latest, int type)
		{
			if (_data.Count == 0) return;

			//search for first data point
			int i = _data.Count - 1;

			for (; i >= 0; i-- )
			{
				if(_data[i].)
			}

			if (min == -1)
			{
				min = _data[_data.Count - 1 - from].data[type];
			}
			if (max == -1)
			{
				max = _data[_data.Count - 1 - from].data[type];
			}
			for (int i = _data.Count - 1 - from; i >= _data.Count - to; i--)
			{
				if (_data[i].data[type] > max)
				{
					max = _data[i].data[type];
				}
				if (_data[i].data[type] < min)
				{
					min = _data[i].data[type];
				}
			}
		}

		*/
		/*
		public List<PeakInfo> GetPeaks(int num, float minSlope = 5)
		{
			List<PeakInfo> list = new List<PeakInfo>();

			int type = (int)TradeDataType.AVG_3H;

			bool stagn = false;

			float lastVal = _data[_data.Count - 2].data[type];
			float lastSlope = _data[_data.Count - 1].data[type] - lastVal;
			for (int i = _data.Count - 3; i >= 0 && num > 0; i--)
			{
				float slope = lastVal - _data[i].data[type];

				PeakInfo info = new PeakInfo();
				info.value = _data[i + 1].data[type];
				info.offset = _data.Count - 2 - i;
				info.time = _data[i + 1].date;

				if (lastSlope > 0 && slope < 0)
				{
					info.max = false;
					list.Add(info);
					num--;
				}
				else if (lastSlope < 0 && slope > 0)
				{
					info.max = true;
					list.Add(info);
					num--;
				}


				if (lastSlope < minSlope)
				{
					stagn = true;
				}

				lastVal = info.value;
				lastSlope = slope;
			}

			return list;
		}

		public bool Rise(TradeDataType type)
		{
			TradeData last = _data[_data.Count-1];
			TradeData preLast = _data[_data.Count-2];
			
			return (last.data[(int)type] - preLast.data[(int)type])>= 0;
		}

		public bool Under12(int offset)
		{
			return _data[_data.Count - 1 - offset].data[(int)TradeDataType.AVG_3H] < _data[_data.Count - 1 - offset].data[(int)TradeDataType.AVG_12H];
		}

		public bool ConstLowHigh()
		{
			int num = 12 * 4;

			bool below = Under12(0);

			for(int i = 1; i < num; i++)
			{
				if(below != Under12(i))
				{
					return false;
				}
			}
			return true;
		}
				
		public float CurrentVal
		{
			get
			{
				return _data[_data.Count - 1].data[(int)TradeDataType.AVG_3H];
			}
		}
		*/

	}
}
