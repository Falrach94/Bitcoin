using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BitcoinAnalyser
{
	enum DataPointType
	{
		PEAK, PEAK_SMALL, AVERAGE, START, END, EX1, EX2, EX3, EX4,
	}
	enum DataPointProcType
	{
		DIF, GRAD_BUY, GRAD_SELL, REL_CHANGE, TREND_SCORE, BUY_SCORE, SELL_SCORE, MIXED_SCORE
	}

	enum DataType
	{
		BUY, SELL, PROC
	}

	public class DataPoint
	{
		public float[] data = new float[typeof(DataPointType).GetEnumValues().Length];
	}

	public class DataSet
	{
		public DataPoint[] setData = { new DataPoint(), new DataPoint(), new DataPoint()};

		public DateTime time;
	}

	class OfferRecorder
	{
		const float MAX_TIME_DIF_MIN = 5f;

		const int ANA_NUM = 10; // how many buy/sell offers will be counted for a data point
		const float ANA_LIMIT = 0.01f;

		public static OfferRecorder that = new OfferRecorder();

		private OfferRecorder() { }

		const string PATH = "C:/bitcoins/OfferLog/";

		List<DataSet> _data = new List<DataSet>();

		DataSet _currentRecord = null;
		List<DataPoint> _recordSetBuy = new List<DataPoint>();
		List<DataPoint> _recordSetSell = new List<DataPoint>();
				
		void LoadRecordFile(string url)
		{
			XmlDocument doc = new XmlDocument();

			doc.Load(url);

			XmlElement root = doc.DocumentElement;

			foreach (XmlElement e in root.GetElementsByTagName("record"))
			{
				DataSet set = new DataSet();
				set.time = DateTime.Parse(e.GetAttribute("time"));

				XmlElement buy = (XmlElement)e.GetElementsByTagName("buy")[0];
				for (int i = 0; i < typeof(DataPointType).GetEnumValues().Length; i++)
				{
					float.TryParse(buy.GetAttribute(((DataPointType)i).ToString()), out set.setData[0].data[i]);
				}

				XmlElement sell = (XmlElement)e.GetElementsByTagName("sell")[0];
				for (int i = 0; i < typeof(DataPointType).GetEnumValues().Length; i++)
				{
					float.TryParse(sell.GetAttribute(((DataPointType)i).ToString()), out set.setData[1].data[i]);
				}

				AddData(set);
			}

		}

		public void Load(DateTime till)
		{
			_data.Clear();

			System.IO.Directory.CreateDirectory(PATH);

			var files = System.IO.Directory.GetFiles(PATH);

			if (files == null || till > DateTime.Now)
			{
				return;
			}

			DateTime time = new DateTime(till.Ticks);

			do
			{
				string fileName = PATH + time.ToShortDateString().Replace('.', '_') + "_hour_" + time.Hour.ToString() + ".xml";

				foreach (var f in files)
				{
					if (f == fileName)
					{
						LoadRecordFile(fileName);
						break;
					}
				}
				time = time.AddHours(1);
			} while (time.Date.AddHours(time.Hour) < DateTime.Now);

			GraphForm.that.RefreshGraph();
		}
		void Save()
		{
			System.IO.Directory.CreateDirectory(PATH);

			XmlDocument doc = new XmlDocument();

			XmlElement root = null;

			string fileName = (_currentRecord.time.ToShortDateString().Replace('.', '_') + "_hour_" + _currentRecord.time.Hour + ".xml");


			_currentRecord.setData[2].data[(int)DataPointType.AVERAGE] = _currentRecord.setData[0].data[(int)DataPointType.AVERAGE] - _currentRecord.setData[1].data[(int)DataPointType.AVERAGE];

			AddData(_currentRecord);


			//search for existing file
			if (System.IO.File.Exists(PATH + fileName))
			{
				doc.Load(PATH + fileName);
				root = doc.DocumentElement;
			}
			else
			{
				//create new file
				root = doc.CreateElement("list");
				doc.AppendChild(root);
			}

			XmlElement rec = doc.CreateElement("record");
			root.AppendChild(rec);

			rec.SetAttribute("time", _currentRecord.time.ToString());

			XmlElement buy = doc.CreateElement("buy");
			rec.AppendChild(buy);

			for (int i = 0; i < typeof(DataPointType).GetEnumValues().Length; i++)
			{
				buy.SetAttribute(((DataPointType)i).ToString(), _currentRecord.setData[0].data[i].ToString());
			}

			XmlElement sell = doc.CreateElement("sell");
			rec.AppendChild(sell);

			for (int i = 0; i < typeof(DataPointType).GetEnumValues().Length; i++)
			{
				sell.SetAttribute(((DataPointType)i).ToString(), _currentRecord.setData[1].data[i].ToString());
			}


			doc.Save(PATH + fileName);
		}

		void AddData(DataSet set)
		{
			set.setData[(int)DataType.PROC].data[(int)DataPointProcType.DIF] = set.setData[0].data[(int)DataPointType.AVERAGE] / set.setData[1].data[(int)DataPointType.AVERAGE];

			if(_data.Count > 0)
			{
				if ((_data.Last().time - set.time).TotalMinutes > MAX_TIME_DIF_MIN)
				{
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.GRAD_BUY] = 0;
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.GRAD_SELL] = 0;
				}
				else
				{
					float buyOld = _data.Last().setData[(int)DataType.BUY].data[(int)DataPointType.AVERAGE];
					float sellOld = _data.Last().setData[(int)DataType.SELL].data[(int)DataPointType.AVERAGE];
					float buyNew = set.setData[(int)DataType.BUY].data[(int)DataPointType.AVERAGE];
					float sellNew = set.setData[(int)DataType.SELL].data[(int)DataPointType.AVERAGE];

					float buyDif = buyNew - buyOld;
					float sellDif = sellNew - sellOld;

					float relBuy = buyNew / buyOld -1;
					float relSell = sellNew / sellOld -1;

					float relChange = (relBuy +1) / (relSell +1);


					const float LIN_AMP = 1f;
					const float QUAD_AMP = 0.01f;
					
					const float DECAY = 0.05f;

					const float BOOST = 20;

					const float EXP_FLAT = 100;

					//----------- trendScore

					//(0)
					float scoreOld = _data.Last().setData[(int)DataType.PROC].data[(int)DataPointProcType.TREND_SCORE];

					//(2)
					float trendScore = scoreOld - scoreOld * DECAY;

					//(1)
					trendScore += LIN_AMP * (relBuy);
					trendScore += LIN_AMP * (relSell );

					//(3)
					if(relBuy < 0)
						trendScore += (float)(QUAD_AMP * Math.Sign(relBuy) * Math.Pow(1 + Math.Abs(relBuy), 2) * Math.Exp(-buyDif/(buyNew*EXP_FLAT)));
				
					if(relSell > 0)
						trendScore += (float)(QUAD_AMP * Math.Sign(relSell) * Math.Pow(1 + Math.Abs(relSell), 2) * Math.Exp(-sellDif / (sellNew*EXP_FLAT)));

					//------------ buy/sell score

					const float BUY_SELL_QUAD_P = 0.01f;
					const float BUY_SELL_QUAD_M = 0.009f;
					const float BUY_SELL_LIN = 0;

					const float BUY_SELL_DECAY = 0.09f;

					float buyScore, sellScore;
					float buyScoreOld = _data.Last().setData[(int)DataType.PROC].data[(int)DataPointProcType.BUY_SCORE];
					float sellScoreOld = _data.Last().setData[(int)DataType.PROC].data[(int)DataPointProcType.SELL_SCORE];

					buyScore = buyScoreOld;
					sellScore = sellScoreOld;

					if(relBuy < 0)
						buyScore += BUY_SELL_QUAD_P * (float)(Math.Sign(relBuy) * Math.Pow(1 + Math.Abs(relBuy), 2));
					else
						buyScore += BUY_SELL_QUAD_M * (float)(Math.Sign(relBuy) * Math.Pow(1 + Math.Abs(relBuy), 2));

					if(relSell > 0)
						sellScore += BUY_SELL_QUAD_P * (float)(Math.Sign(relSell) * Math.Pow(1 + Math.Abs(relSell), 2));
					else
						sellScore += BUY_SELL_QUAD_M * (float)(Math.Sign(relSell) * Math.Pow(1 + Math.Abs(relSell), 2));

					buyScore += BUY_SELL_LIN * relBuy;
					sellScore += BUY_SELL_LIN * relSell;

					buyScore -= buyScoreOld * BUY_SELL_DECAY;
					sellScore -= sellScoreOld * BUY_SELL_DECAY;

					//------------ mixed score

					float mixedScore;
					//float mixedScoreOld = _data.Last().setData[(int)DataType.PROC].data[(int)DataPointProcType.MIXED_SCORE];

					const float AMP = 100.1f;
					const float STRETCH = 0.01f;

					double bs = Math.Exp(-buyScore * STRETCH);
					double ss = Math.Exp(-sellScore * STRETCH);
					bs = buyScore;
					ss = sellScore;

					double ts = Math.Sign(trendScore)*(Math.Atan(trendScore) + Math.PI/2)/(Math.PI);

					mixedScore = -AMP * (float)( bs* ss * ts);

					if (trendScore < 0)
					{
						int t = 0;
					}

					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.GRAD_BUY] = relBuy +1;
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.GRAD_SELL] = relSell +1;

					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.REL_CHANGE] = relChange;

					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.TREND_SCORE] = trendScore;
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.BUY_SCORE] = buyScore;
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.SELL_SCORE] = sellScore;
					set.setData[(int)DataType.PROC].data[(int)DataPointProcType.MIXED_SCORE] = mixedScore;
				}
			}
			_data.Add(set);
		}

		public void RecordData()
		{
			DataPoint newPointBuy = new DataPoint();
			DataPoint newPointSell = new DataPoint();

			newPointBuy.data[(int)DataPointType.AVERAGE] = OfferBook.that.Average(true, ANA_NUM);
			newPointBuy.data[(int)DataPointType.PEAK] = OfferBook.that.Best(true, 0);
			newPointBuy.data[(int)DataPointType.PEAK_SMALL] = OfferBook.that.Best(true, ANA_LIMIT);

			newPointSell.data[(int)DataPointType.AVERAGE] = OfferBook.that.Average(false, ANA_NUM);
			newPointSell.data[(int)DataPointType.PEAK] = OfferBook.that.Best(false, 0);
			newPointSell.data[(int)DataPointType.PEAK_SMALL] = OfferBook.that.Best(false, ANA_LIMIT);

			if (newPointBuy.data[(int)DataPointType.AVERAGE] == -1 || newPointSell.data[(int)DataPointType.AVERAGE] == -1)
			{
				return;
			}

			if ((_currentRecord != null) && (_currentRecord.time.AddMinutes(1) <= DateTime.Now))
			{
				float sum = 0;

				float minMaxB = 999999999;
				float minMaxS = 999999999;

				foreach (var e in _recordSetBuy)
				{
					sum += e.data[(int)DataPointType.AVERAGE];
					if (minMaxB > e.data[(int)DataPointType.PEAK]) minMaxB = e.data[(int)DataPointType.PEAK];
					if (minMaxS > e.data[(int)DataPointType.PEAK_SMALL]) minMaxS = e.data[(int)DataPointType.PEAK_SMALL];
				}
				_currentRecord.setData[0].data[(int)DataPointType.AVERAGE] = sum / _recordSetBuy.Count;
				_currentRecord.setData[0].data[(int)DataPointType.PEAK] = minMaxB;
				_currentRecord.setData[0].data[(int)DataPointType.PEAK_SMALL] = minMaxS;

				minMaxB = 0;
				minMaxS = 0;
				sum = 0;

				foreach (var e in _recordSetSell)
				{
					sum += e.data[(int)DataPointType.AVERAGE];
					if (minMaxB < e.data[(int)DataPointType.PEAK]) minMaxB = e.data[(int)DataPointType.PEAK];
					if (minMaxS < e.data[(int)DataPointType.PEAK_SMALL]) minMaxS = e.data[(int)DataPointType.PEAK_SMALL];
				}
				_currentRecord.setData[1].data[(int)DataPointType.AVERAGE] = sum / _recordSetBuy.Count;
				_currentRecord.setData[1].data[(int)DataPointType.PEAK] = minMaxB;
				_currentRecord.setData[1].data[(int)DataPointType.PEAK_SMALL] = minMaxS;

				_currentRecord.setData[0].data[(int)DataPointType.END] = _recordSetBuy.Last().data[(int)DataPointType.AVERAGE];
				_currentRecord.setData[1].data[(int)DataPointType.END] = _recordSetSell.Last().data[(int)DataPointType.AVERAGE];

				if (GraphForm.that.RecordOfferData)
				{
					Save();
				}
				GraphForm.that.RefreshGraph();

				_currentRecord = null;
			}

			if (_currentRecord == null)
			{
				_currentRecord = new DataSet();

				_currentRecord.time = DateTime.Now;

				_currentRecord.setData[0].data[(int)DataPointType.START] = newPointBuy.data[(int)DataPointType.AVERAGE];
				_currentRecord.setData[1].data[(int)DataPointType.START] = newPointSell.data[(int)DataPointType.AVERAGE];

				_recordSetBuy.Clear();
				_recordSetSell.Clear();
			}


			_recordSetBuy.Add(newPointBuy);
			_recordSetSell.Add(newPointSell);

		}

		public bool GetData(DateTime earliest, DateTime latest, out List<DataSet> list)
		{
			list = new List<DataSet>();

			if (_data.Count == 0)
			{
				Load(DateTime.Now.AddDays(-7));
			}

			int i = _data.Count - 1;

			while (i >= 0 && _data[i].time > latest)
			{
				i--;
			}
			while (i >= 0 && _data[i].time >= earliest)
			{
				list.Add(_data[i--]);
			}

			return list.Count != 0;
		}

		public void GetMinMax(ref float min, ref float max, DateTime earliest, DateTime latest, int buy, int type)
		{
			if (_data.Count == 0) return;

			List<DataSet> list;
			if (GetData(earliest, latest, out list))
			{
				foreach (var v in list)
				{
					if (min == -1) min = v.setData[buy].data[type];
					if (max == -1) max = v.setData[buy].data[type];

					if (v.setData[buy].data[type] > max)
					{
						max = v.setData[buy].data[type];
					}
					if (v.setData[buy].data[type] < min)
					{
						min = v.setData[buy].data[type];
					}
				}
			}
		}
	

	}
}
