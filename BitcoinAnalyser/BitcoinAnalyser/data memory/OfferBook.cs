using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalyser
{
	class OfferBook
	{
		BitcoinWebsite _website;

		public OfferPage _currentPage;


		public List<Record> BuyOffers
		{
			get
			{
				return _currentPage.buyOffers;
			}
		}
		public List<Record> SellOffers
		{
			get
			{
				return _currentPage.sellOffers;
			}
		}

		public List<Record> _usedEntries = new List<Record>();


		public static OfferBook that = new OfferBook();

		private OfferBook() { }

		public void Init(System.Windows.Forms.WebBrowser browser)
		{
			_website = new BitcoinWebsite();
			_website.Init(browser);
		}

		public bool Update()
		{
			if(_website != null && _website.GetCurrentPage(out _currentPage))
			{
				UpdateUsedList(_currentPage.buyOffers);
				UpdateUsedList(_currentPage.sellOffers);

				OfferRecorder.that.RecordData();
				return true;
			}
			return false;
		}

		public float Best(bool buy, float volLimit)
		{
			const float MAX = 999999999;
			float res = -1;
			if (Ready())
			{
				var list = buy ? _currentPage.buyOffers : _currentPage.sellOffers;
				float minMax = buy ? MAX : 0;

				if (volLimit == 0) return list[0].info.BuyPrice;

				foreach (var e in list)
				{
					if (volLimit == 0 || e.min <= volLimit)
					{
						if ((buy && (minMax > e.info.BuyPrice)) || (!buy && (minMax < e.info.BuyPrice)))
						{
							minMax = e.info.BuyPrice;
						}
					}
				}
				if (minMax != MAX && minMax != 0)
				{
					res = minMax;
				}
			}
			return res;
		}

		public bool Ready()
		{
			if (_currentPage == null) return false;
			if (_currentPage.buyOffers == null || _currentPage.sellOffers == null) return false;

			if (_currentPage.buyOffers.Count == 0 || _currentPage.sellOffers.Count == 0) return false;

			return true;
		}

		public float Average(bool buy, int num)
		{
			float sum = 0;

			if (_currentPage == null) return -1;
			if (_currentPage.buyOffers == null || _currentPage.sellOffers == null) return -1;

			var list = buy ? _currentPage.buyOffers : _currentPage.sellOffers;

			if (list.Count < num) num = list.Count;

			if (num < 1) return -1;

			for (int i = 0; i < num; i++)
			{
				sum += list[i].info.BuyPrice;
			}
			return sum / (float)(num);
		}
		public float AvgFee(bool buy, int num)
		{
			float sum = 0;

			if (_currentPage == null) return 0;
			if (_currentPage.buyOffers == null || _currentPage.sellOffers == null) return 0;

			var list = buy ? _currentPage.buyOffers : _currentPage.sellOffers;

			if (list.Count < num) num = list.Count;

			if (num <= 1) return 0;

			for (int i = 1; i < num; i++)
			{
				sum += list[i].info.GetFeePrice(buy);
			}
			return sum / (float)(num - 1);
		}



		public List<Record> GetClearedList(bool buy)
		{
			List<Record> fullList = buy ? _currentPage.buyOffers : _currentPage.sellOffers;

			List<Record> res = new List<Record>();
			List<Record> temp = new List<Record>();
			foreach (var e in _usedEntries)
			{
				temp.Add(e);
			}

			foreach (var e in fullList)
			{
				int i = 0;
				bool used = false;
				foreach (var t in temp)
				{
					if (e.Equals(t))
					{
						used = true;
						break;
					}
					i++;
				}
				if (used)
				{
					temp.RemoveAt(i);
				}
				else
				{
					res.Add(e);
				}
			}
			return res;
		}

		void UpdateUsedList(List<Record> orig)
		{
			int i = 0;
			List<int> del = new List<int>();
			foreach (var r in _usedEntries)
			{
				bool present = false;
				foreach (var l in orig)
				{
					if (r.Equals(l))
					{
						present = true;
					}
				}
				if (!present)
				{
					del.Add(i);
				}
				i++;
			}
			for (int j = del.Count - 1; j >= 0; j--)
			{
				_usedEntries.RemoveAt(j);
			}
		}


		
		//amount is before fee
		public bool Trade(int i, float amount, bool buy)
		{
			var offer = buy ? _currentPage.buyOffers[i] : _currentPage.sellOffers[i]; 
			if(offer.min > amount || offer.max < amount)
			{
				Logger.Log("Trade", "Requested amount doesn't fit with offer!");
				Logger.LogVal("min", offer.min, LogType.BTC);
				Logger.LogVal("max", offer.max, LogType.BTC);
				Logger.LogVal("amount", amount, LogType.BTC);
				return false;
			}

			Logger.Log("Trade", "Successfully "+ (buy ? "bought " : "sold ") + amount.ToString("N5") +" BTC!");
			_usedEntries.Add(offer);

			return true;
		}

		//returns max possible buy amount after fee
		public bool GetMaxBuyAmount(float money, int iTrade, out float amount, bool minusFee, out float necMoney)
		{
			var trade = _currentPage.buyOffers[iTrade];

			float maxAmount = money / trade.info.BuyPrice;

			if (maxAmount > trade.max)
			{
				maxAmount = trade.max;
			}

			amount = maxAmount * (minusFee ? 0.995f : 1.0f);

			bool res = maxAmount >= trade.min;

			necMoney = (res ? maxAmount : trade.min)* trade.info.BuyPrice;

			return res;
		}
		//returns max possible buy amount after fee
		public bool GetMaxSellAmount(int iTrade, float amount, out float maxAmount, out float money)
		{
			var trade = _currentPage.sellOffers[iTrade];

			maxAmount = amount;

			if(maxAmount > trade.max)
			{
				maxAmount = trade.max;
			}

			money = maxAmount * trade.info.GetFeePrice(false);

			return trade.min <= amount;
		}

		public bool FindBuyOffer(float money, out float necMoney, List<float> amount, List<int> foundOffers)
		{
			var list = GetClearedList(true);
			
			necMoney = 0;

			foreach(var e in list)
			{
				float posAmountL, necMoneyL;

				//offer may cost max. 1.5 % more than data[(int)DataPointType.AVERAGE]
				if (e.info.GetFeePrice(true) <= AvgFee(true, 10) * 1.015f)
				{
					if (GetMaxBuyAmount(money, e.id, out posAmountL, true, out necMoneyL))
					{
						money -= necMoneyL;
						necMoney = necMoneyL;

						foundOffers.Add(e.id);
						amount.Add(posAmountL);

						if (money > 0) //search for additional offers
						{
							float extraMoney;
							if(FindBuyOffer(money, out extraMoney, amount, foundOffers))
							{
								necMoney += extraMoney;
							}
						}

						return true;
					}
				}
			}


			return false;
		}
    
	}
}
