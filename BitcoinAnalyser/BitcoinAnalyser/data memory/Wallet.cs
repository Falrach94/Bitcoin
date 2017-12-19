using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace BitcoinAnalyser
{
	public enum VolumeOwner
	{
		RESERVE_BTC, RESERVE_EUR, SPECULATION, OTHER, ALL, COUNT
	}

	public class TradeAnalysation
	{
		float _price, _amount, _fee;

		//fee in %
		public TradeAnalysation(float price, float amount, float fee = 0.08f)
		{
			_price = price;
			_amount = amount;
			_fee = fee/100;
		}

		public float Amount { get { return _amount * (1 - _fee); } }
		public float BuyAmount { get { return _amount; } }

		public float BuyPrice { get { return _price; } }

		public float FeePrice { get { return _amount * _price * _fee / 2.0f; } }

		public float PaidPrice { get { return _amount * (1 - _fee/2) * _price; } }

		public float GetFeePrice(bool buy)
		{
			return _price * (buy ? (1 + _fee / 2) : (1 - _fee / 2));
		}

		public float GetSellPriceForPercentageGain(float per)
		{
			return (1 + per / 100) * _price / (1 - _fee);
		}
		
		public float GetGain(float price)
		{
			return GetValue(price) - PaidPrice;
		}
		public float GetPercentageGain(float currentSellPrice)
		{
			float paid = PaidPrice;
			float gain = GetGain(currentSellPrice);
			return (gain + paid) / paid;
		}
		public float GetValue(float price)
		{
			return price * Amount * (1 - _fee / 2.0f);
		}

		public void Save(XmlElement el)
		{
			el.SetAttribute("price", _price.ToString());
			el.SetAttribute("amount", _amount.ToString());
			el.SetAttribute("fee", _fee.ToString());
		}
		static public TradeAnalysation Load(XmlElement el)
		{
			float price, amount, fee;
			float.TryParse(el.GetAttribute("price"), out price);
			float.TryParse(el.GetAttribute("amount"), out amount);
			float.TryParse(el.GetAttribute("fee"), out fee);
			return new TradeAnalysation(price, amount, fee * 100);
		}
	}


	class WalletItem
	{
		public DateTime date;
		public TradeAnalysation data;
		public float[] share = new float[(int)VolumeOwner.COUNT];
	}
	class MoneyWallet
	{
		public float startKapital = 1000;
		float kapital = 1000;
		float[] spendings = new float[(int)VolumeOwner.COUNT];
		public float[] share = new float[(int)VolumeOwner.COUNT];

		public float Kapital
		{
			get
			{
				return kapital;
			}
		}

		public MoneyWallet()
		{
			share[(int)VolumeOwner.RESERVE_BTC] = 0.2f;
			share[(int)VolumeOwner.RESERVE_EUR] = 0.2f;
			share[(int)VolumeOwner.SPECULATION] = 0.6f;
		}

		public float RemainingMoney(VolumeOwner owner, float maxPerc = 1.0f)
		{
			if (owner == VolumeOwner.ALL)
			{
				float res = 0;
				foreach (var s in spendings)
				{
					res += s;
				}
				return kapital - res;
			}
			else if ((int)owner > 3)
			{
				return 0;
			}
			float result = kapital * share[(int)owner] - spendings[(int)owner];
			float max = kapital * share[(int)owner] * maxPerc;
			if(result > max)
			{
				result = max;
			}

			return result;
		}
		public float SpentMoney(VolumeOwner owner)
		{
			if (owner == VolumeOwner.ALL)
			{
				float res = 0;
				foreach (var s in spendings)
				{
					res += s;
				}
				return res;
			}
			else if ((int)owner > 3)
			{
				return 0;
			}
			return spendings[(int)owner];
		}


		public void AddMoney(VolumeOwner owner, float amount)
		{
			spendings[(int)owner] -= amount;

			Logger.Log("Wallet", "Booked " + amount.ToString("N2") + " € for " + owner.ToString() +"!");
			
			float dif = -1.0f*spendings[(int)owner];

			if(dif > 0)
			{
				float res = spendings[(int)VolumeOwner.RESERVE_EUR];
				if(res != 0)
				{
					if(res > dif)
					{
						spendings[(int)VolumeOwner.RESERVE_EUR]-= dif;
						dif = 0;
					}
					else
					{
						spendings[(int)VolumeOwner.RESERVE_EUR] = 0;
						dif -= res;
					}
				}
				spendings[(int)owner] = 0;
				kapital += dif;
			}

		}
		public bool Spend(VolumeOwner owner, float amount)
		{
			if(RemainingMoney(owner) < amount)
			{
				return false;
			}

			spendings[(int)owner] += amount;

			return true;
		}

		public void Save(XmlDocument doc, XmlElement root)
		{
			XmlElement eur = doc.CreateElement("eur");
			root.AppendChild(eur);

			eur.SetAttribute("kapital", Kapital.ToString());

			int i = 0;
			foreach (var spending in spendings)
			{
				XmlElement xmlItem = doc.CreateElement("spending");
				eur.AppendChild(xmlItem);

				xmlItem.SetAttribute("type", (i).ToString());
				xmlItem.SetAttribute("value", spending.ToString());
				xmlItem.SetAttribute("share", share[i].ToString());
				i++;
			}
		}
		public void Load(XmlElement root)
		{
			XmlElement eur = (XmlElement)root.GetElementsByTagName("eur")[0];

			float.TryParse(eur.GetAttribute("kapital"), out kapital);

			foreach (XmlElement xmlItem in eur.GetElementsByTagName("spending"))
			{
				int type;
				int.TryParse(xmlItem.GetAttribute("type"), out type);

				float.TryParse(xmlItem.GetAttribute("value"), out spendings[type]);
				float.TryParse(xmlItem.GetAttribute("share"), out share[type]);
			}
		}
	}

	class Wallet
	{
		public List<WalletItem> _content = new List<WalletItem>();

		public MoneyWallet _money = new MoneyWallet();

		public static Wallet that = new Wallet();

		MoneyWallet Money
		{
			get
			{
				return _money;
			}
		}

		const string URL = "C:/bitcoins/wallet.xml";

		private Wallet() { }

		public void Save()
		{
			XmlDocument doc = new XmlDocument();

			XmlElement root = doc.CreateElement("wallet");
			doc.AppendChild(root);

			XmlElement btc = doc.CreateElement("btc");
			root.AppendChild(btc);
						
			foreach(var item in _content)
			{
				XmlElement xmlItem = doc.CreateElement("item");
				btc.AppendChild(xmlItem);

				xmlItem.SetAttribute("date", item.date.ToString());
				item.data.Save(xmlItem);
				//xmlItem.SetAttribute("buy", item.buy.ToString());
				xmlItem.SetAttribute("share_res", item.share[(int)VolumeOwner.RESERVE_BTC].ToString());
				xmlItem.SetAttribute("share_spec", item.share[(int)VolumeOwner.SPECULATION].ToString());
			}

			_money.Save(doc, root);

			Logger.Log("Saved Wallet!", true);

			doc.Save(URL);
		}
		public void Load()
		{
			XmlDocument doc = new XmlDocument();

			try
			{
				doc.Load(URL);

				_content.Clear();

				XmlElement root = doc.DocumentElement;

				foreach (XmlElement xmlItem in root.GetElementsByTagName("item"))
				{
					WalletItem item = new WalletItem();

					DateTime.TryParse(xmlItem.GetAttribute("date"), out item.date);
					item.data = TradeAnalysation.Load(xmlItem);
					//bool.TryParse(xmlItem.GetAttribute("buy"), out item.buy);
					float.TryParse(xmlItem.GetAttribute("share_res"), out item.share[(int)VolumeOwner.RESERVE_BTC]);
					float.TryParse(xmlItem.GetAttribute("share_spec"), out item.share[(int)VolumeOwner.SPECULATION]);

					_content.Add(item);
				}

				_money.Load(root);
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Failed to load wallet!");
				Save();
			}


		}


		public bool BuyItem(WalletItem item)
		{
			for(int i = 0; i < item.share.Length; i++)
			{
				float price = item.share[i] * item.data.PaidPrice;
				float remaining = _money.RemainingMoney((VolumeOwner)i);
				if(remaining < price)
				{
					Logger.Log("Wallet", "Not enough money in reserve '" + ((VolumeOwner)i).ToString() + "' to buy item!");
					Logger.LogVal("available", remaining, LogType.EUR);
					Logger.LogVal("item price", item.data.PaidPrice, LogType.EUR);
					Logger.LogVal("share", item.share[i], LogType.PERC);
					Logger.LogVal("price share", price, LogType.EUR);
					return false;
				}
			}
			for (int i = 0; i < item.share.Length; i++)
			{
				_money.Spend((VolumeOwner)i, item.share[i] * item.data.PaidPrice);
			}

			_content.Add(item);

			Logger.Log("Wallet", "Successfully added item to wallet!");
			Logger.LogLineSub();
			Logger.LogVal("cost", item.data.BuyPrice, LogType.EUR);
			Logger.LogVal("cost (fee)", item.data.GetFeePrice(true), LogType.EUR);
			Logger.LogVal("amount", item.data.Amount, LogType.BTC);
			Logger.LogVal("price", item.data.PaidPrice, LogType.EUR);

			for (int i = 0; i < (int)VolumeOwner.COUNT; i++)
			{
				if(item.share[i] != 0)
				{
					Logger.LogVal("share (" + ((VolumeOwner)i).ToString() + ")", item.share[i], LogType.PERC);
				}
			}
			Logger.Log("");

			GraphForm.that.AddMarker(item.date, item.data, false);

			Save();

			return true;
		}

		public float GetBitcoins(VolumeOwner owner)
		{
			float result = 0;
			foreach (var item in _content)
			{
				result += item.share[(int)owner] * item.data.Amount;
			}

			return result;
		}

		public float GetBitcoins(VolumeOwner owner, float price_fee, float minGainPercentage)
		{
			float result = 0;
			foreach (var item in _content)
			{
				if (price_fee / item.data.GetFeePrice(true) >= minGainPercentage)
				{
					result += item.share[(int)owner] * item.data.Amount;
				}
			}

			return result;
		}

		List<WalletItem> GetOrderedList(VolumeOwner owner)
		{
			List<WalletItem> list = new List<WalletItem>();
			
			while(true)
			{
				float min = 9999999999;
				bool br = true;
				int i = 0, iM = 0;
				foreach(var item in _content)
				{
					bool newIt = true;
					foreach(var it in list)
					{
						if(it == item)
						{//item is already in ordered list
							newIt = false;
							break;
						}
					}
					if(newIt && (item.share[(int)owner] > 0))
					{
						//item is new and belongs in parts to requested owner
						if(min > item.data.BuyPrice)
						{
							min = item.data.BuyPrice;
							br = false;
							iM = i;
						}
					}
					i++;
				}
				if(br) break;

				list.Add(_content[iM]);
			}

			return list;
		}

		public bool SellItem(float amount, VolumeOwner owner, float cost_fee)
		{
			if (GetBitcoins(owner) < amount)
			{
				Logger.Log("Wallet", "Not enough bitcoins to sell in wallet!");
				Logger.LogVal("has", GetBitcoins(owner), LogType.BTC);
				Logger.LogVal("need", amount, LogType.BTC);
				Logger.Log("");
				return false;
			}
			
			var list = GetOrderedList(owner);

			for (int i = 0; i < list.Count && amount > 0; i++)
			{
				float share = list[i].share[(int)owner] * list[i].data.Amount;

				if (share < amount)
				{
					amount -= share;
					list[i].share[(int)owner] = 0;
				}
				else
				{
					amount = 0;
					list[i].share[(int)owner] -= amount / list[i].data.Amount;
					break;
				}
			}

			Logger.Log("Wallet", "Sold item from wallet!");

			_money.AddMoney(owner, amount * cost_fee);
			return true;
		}
		public bool SellItem(ref float targetAmount, VolumeOwner owner, float cost_fee)
		{
			float amount = GetBitcoins(owner);

			if(amount >= targetAmount)
			{
				amount = targetAmount;
				targetAmount = 0;
			}
			else 
			{
				targetAmount -= amount;
			}

			return SellItem(amount - targetAmount, owner, cost_fee);
		}

	}
}
