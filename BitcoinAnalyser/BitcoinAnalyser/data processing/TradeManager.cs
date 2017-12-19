using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalyser
{
	public enum RequestOwner
	{
		SPEC, RESERVE
	}

	public class BuyRequest
	{
		public RequestOwner owner;
		public float value;

		public PeakInfo lastMax, lastMin;

		public DateTime time;
	}
	public class SellRequest
	{
		public RequestOwner owner;
		public DateTime time;
	}

	class TradeManager
	{
		List<BuyRequest> _requestListBuy = new List<BuyRequest>();
		List<SellRequest> _requestListSell = new List<SellRequest>();

		public static TradeManager that = new TradeManager();

		bool init = false;

		private TradeManager() { }

		public void Init()
		{
			Wallet.that.Load();
			
			Form1.that.RefreshWallet();
			Form1.that.RefreshBitcoinWallet();

			init = true;
		}

		Record lastBuy;
		Record lastSell;

		void FindSpecialOffer()
		{
			/*
			var buyList = Trade.GetClearedList(true);
			var sellList = OfferBook.that.GetClearedList(false);

			if (buyList.Count < 10) return;
			if (sellList.Count < 10) return;

			float av = OfferBook.that.AvgFee(true, 5);

			float rel = buyList[0].price_fee / av ;
			
			if(lastBuy == null ||  !lastBuy.Equals(buyList[0]))
			{
				if (rel < 0.99)
				{
					lastBuy = buyList[0];

					float money_btc = Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_BTC);
					float money_eur = Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_EUR);

					Logger.Log("Special Offer", "Found matching buy offer!");
					Logger.LogLine();
					Logger.LogVal("avg. price", av, LogType.EUR);
					Logger.LogVal("fraction of avg. price", rel, LogType.PERC);
					Logger.LogVal("btc money reserve", money_btc, LogType.EUR);
					Logger.LogVal("eur money reserve", money_eur, LogType.EUR);
					Logger.LogLineSub();
					Logger.LogOffer(buyList[0]);
					Logger.LogLineSub();

					float amount, necMoney;
					if (OfferBook.that.GetMaxBuyAmount(money_btc + money_eur, buyList[0].id, out amount, false, out necMoney))
					{
						Logger.LogVal("maximal amount", amount, LogType.BTC);
						Logger.LogVal("price", necMoney, LogType.EUR);
						Logger.LogLineSub();

						float eur_share = necMoney - money_btc;

						float[] share = new float[(int)VolumeOwner.COUNT];
						/*if (eur_share < 0)
						{
							eur_share *= -1.0f;
							//float share_btc = share[(int)VolumeOwner.RESERVE_BTC] = money_btc / necMoney;
							//float share_eur = share[(int)VolumeOwner.RESERVE_EUR] = eur_share / necMoney;

							Logger.LogVal("share btc reserve", share_btc, LogType.PERC);
							Logger.LogVal("share eur reserve", share_eur, LogType.PERC);
							Logger.LogLineSub();
						}
						else
						{
						}*/
					/*	share[(int)VolumeOwner.RESERVE_BTC] = 1.0f;

						Buy(buyList[0].id, amount, share);
					}
					else
					{
						Logger.Log("Not sufficient money in reserve!");
						Logger.LogVal("minimal price", necMoney, LogType.EUR);
					}

					Logger.LogLine(1);
				}

			}




			av = OfferBook.that.AvgFee(false, 5);
			rel = sellList[0].price_fee / av;
			if (lastSell == null || !lastSell.Equals(sellList[0]))
			{
				if (rel > 1.01)
				{
					lastSell = sellList[0];
					float amount = Wallet.that.GetBitcoins(VolumeOwner.RESERVE_BTC, sellList[0].price_fee, 1.002f);

					Logger.Log("Special Offer", "Found matching sell Offer!");
					Logger.LogLine();
					Logger.LogVal("avg. price", av, LogType.EUR);
					Logger.LogVal("fraction of avg. price", rel, LogType.PERC);

					Logger.LogLineSub();
					Logger.LogOffer(sellList[0]);
					Logger.LogLineSub();
					Logger.LogVal("available btc", amount, LogType.BTC);

					float maxAmount, gain;
					if (OfferBook.that.GetMaxSellAmount(sellList[0].id, amount, out maxAmount, out gain))
					{
						OfferBook.that.Trade(sellList[0].id, maxAmount, false);
						
						Wallet.that.SellItem(maxAmount, VolumeOwner.RESERVE_BTC, sellList[0].price_fee);						
					}
					else
					{
						Logger.Log("Not sufficient bitcoins!");
					}
					Logger.LogLine();
				}
			}*/
		}

		public void Refresh()
		{
			if (!init) return;

			FindSpecialOffer();

			CheckStartSpeculationBuy();
			CheckStopSpeculationBuy();

			FindBuyOffers();
		}

		public float PossibleBitcoins(VolumeOwner owner)
		{
			//float av = AvgFee(true, 5);

			//if (av == 0) return 0;

			return 0;
			//return Wallet.that._money.RemainingMoney(owner) / OfferBook.that.AvgFee(true, 5);
		}

		//amount before fee
		public void Buy(int id, float amount, float[] share)
		{
			OfferBook.that.Trade(id, amount, true);

			var offer = OfferBook.that.BuyOffers[id];

			WalletItem item = new WalletItem();
				item.date = DateTime.Now;

				item.data = new TradeAnalysation(offer.info.BuyPrice, amount, 0.08f);
				//item.buy = true;

				int i = 0;
				foreach (var s in share)
				{
					item.share[i] = share[i];
					i++;
				}
			Wallet.that.BuyItem(item);

			Form1.that.RefreshWallet();
		}
		void Sell()
		{

		}

		public bool OpenBuyRequest(RequestOwner owner)
		{
			return GetBuyRequest(owner) != null;
		}
		public bool OpenSellRequest(RequestOwner owner)
		{
			return GetSellRequest(owner) != null;
		}
		public BuyRequest GetBuyRequest(RequestOwner owner)
		{
			foreach (var o in _requestListBuy)
			{
				if (o.owner == owner)
				{
					return o;
				}
			}
			return null;
		}
		public SellRequest GetSellRequest(RequestOwner owner)
		{
			foreach (var o in _requestListSell)
			{
				if (o.owner == owner)
				{
					return o;
				}
			}
			return null;
		}

		void CheckStopSpeculationBuy()
		{
			/*
			BuyRequest req = GetBuyRequest(RequestOwner.SPEC);

			if(req != null)
			{
				bool stop = false;
				if (!_analysator.Rise(TradeDataType.AVG_3H))
				{
					Logger.Log("Speculation", "Price is falling!");
					stop = true;
				}
				if (_analysator.CurrentVal / req.value > 1.01f)
				{
					Logger.Log("Speculation", "Price has risen more than 1%!");
					Logger.LogVal("start value", req.value, LogType.EUR);
					Logger.LogVal("current value", _analysator.CurrentVal, LogType.EUR);
					Logger.LogVal("rise", _analysator.CurrentVal / req.value, LogType.PERC);
					stop = true;
				}

				float avg = OfferBook.that.AvgFee(true, 5);
				float money = Wallet.that._money.RemainingMoney((req.owner == RequestOwner.RESERVE) ? VolumeOwner.RESERVE_BTC : VolumeOwner.SPECULATION);
				//verfügbares geld reicht für mehr als 0.005 BTC
				float possibleAmount =  money / avg;

				if (possibleAmount > 0.005f)
				{
					Logger.Log("Speculation", "Remaining money doesn't allow to buy more bitcoins!");
					Logger.LogVal("cost (fee)", avg, LogType.EUR);
					Logger.LogVal("money", money, LogType.EUR);
					Logger.LogVal("possible amount", possibleAmount, LogType.BTC);

					stop = true;
				}

				if(stop)
				{
					Logger.Log("Specculation", "Buy request was removed!");
					_requestListBuy.Remove(req);
					_form.RefreshWallet();
				}

			//	if(_analysator.CurrentVal / )
			}*/
		}

		bool CheckStartSpeculationBuy()
		{
			/*
			if(_analysator.Rise(TradeDataType.AVG_3H) && !_analysator.ConstLowHigh() && !OpenBuyRequest(RequestOwner.SPEC))
			{
				List<PeakInfo> list = _analysator.GetMinMax(2);

				//wert liegt mehr als 0.5% unter letztem peak
				float dist = _analysator.CurrentVal / list[1].value ;
				if (dist < 0.995f)
				{
					float avg = OfferBook.that.AvgFee(true, 5);

					//verfügbares geld reicht für mehr als 0.005 BTC
					float possibleAmount = Wallet.that._money.RemainingMoney(VolumeOwner.SPECULATION) / avg;

					if (possibleAmount > 0.005f)
					{
						BuyRequest req = new BuyRequest();

						req.owner = RequestOwner.SPEC;
						req.lastMax = list[1];
						req.lastMin = list[0];
						req.value = _analysator.CurrentVal;

						_requestListBuy.Add(req);

						Logger.Log("Speculation", "Added new speculation buy request!");
						Logger.LogVal("relativ peak distance", dist, LogType.PERC);
						Logger.LogVal("absolute peak distance", list[1].value - _analysator.CurrentVal, LogType.EUR);
						Logger.LogVal("current cost (fee)", avg, LogType.EUR);
						Logger.LogVal("possible amount", possibleAmount, LogType.BTC);

					}
				}

			}
			*/
			return true;
		}

		void FindBuyOffers()
		{
			float[] share = new float[(int)VolumeOwner.COUNT];

			float[] money = new float[(int)VolumeOwner.COUNT];

			//---- determine available money

			foreach(var req in _requestListBuy)
			{
				switch(req.owner)
				{
					//all reserve buys (€ and btc) go to reserve_btc
					case RequestOwner.SPEC:
					{
						money[(int)VolumeOwner.RESERVE_BTC] += Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_EUR, 0.25f);
						money[(int)VolumeOwner.SPECULATION] += Wallet.that._money.RemainingMoney(VolumeOwner.SPECULATION);
						break;
					}
					case RequestOwner.RESERVE:
					{
						money[(int)VolumeOwner.RESERVE_BTC] += Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_EUR, 0.25f);
						money[(int)VolumeOwner.RESERVE_BTC] += Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_BTC);
						break;
					}
				}
			}

			float ges = 0;
			foreach(float v in money)
			{
				ges += v;
			}

			int i = 0;
			foreach (float v in money)
			{
				share[i] = money[i] / ges;

				i++;
			}

			//---- find offers
			float necMoney;
			List<int > ids = new List<int>();
			List<float> amount = new List<float>();

			if(OfferBook.that.FindBuyOffer(ges, out necMoney, amount, ids))
			{
				Logger.Log("Buy Offers", "Found " + ids.Count.ToString() + " matching offers!");

				int j = 0;
				foreach(var id in ids)
				{
					WalletItem item = new WalletItem();
					item.data = new TradeAnalysation(OfferBook.that.BuyOffers[id].info.BuyPrice,
													 amount[j], 0.08f);
					item.date = DateTime.Now;
					item.share = share;

					OfferBook.that.Trade(id, amount[j], true);

					Wallet.that.BuyItem(item);

					j++;
				}
				//priority order: spec, reserve_btc, reserve_eur 
				foreach(var r in _requestListBuy)
				{
					if(r.owner == RequestOwner.SPEC)
					{
						Logger.Log("Speculation", "Removed speculation buy request!");
					}
				}

			}

		}
	}
}
