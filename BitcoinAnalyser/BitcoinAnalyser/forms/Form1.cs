using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitcoinAnalyser
{

    public partial class Form1 : Form
    {

		static public Form1 that;

        public class OfferFilter
        {
            public float min = -1, max = -1;
            public int num = -1;

            public bool sepa = true, express = true;
        }

		bool _refreshGraph = true;

        OfferFilter filter = new OfferFilter();
		AnalysationFilter anaFilter = new AnalysationFilter();

        BitcoinWebsite website;

        Analysator analysator = new Analysator();
		

		bool refreshWallet = false;

        public Form1()
        {
			that = this;

            InitializeComponent();
			
	   }

		private void Form1_Load(object sender, EventArgs e)
		{

			analysator._form = this;

			Logger.Init();

			website = new BitcoinWebsite();
			website.Init(browser);

			LoadAnalysatorFilter();

			JsonReader.that.Reload3MonthJson();

			TradeManager.that.Init();

			OfferBook.that.Init(marketplaceBrowser);
		}

		//---------------- price developement
		
        private void button1_Click(object sender, EventArgs e)
        {
           // float.TryParse()
            OfferPage page;
            website.GetCurrentPage(out page);
             int t = 0;
           // browser.Document.
        }

		// filter ----

        void ReadFilterData()
        {
            string num = FilterNumBox.Text.Replace('.', ',');
            string max = FilterMaxBox.Text.Replace('.', ',');
            string min = FilterMinBox.Text.Replace('.', ',');

            if (!int.TryParse(num, out filter.num))
            {
                filter.num = -1;
            }
            if (!float.TryParse(max, out filter.max))
            {
                filter.max = -1;
            }
            if (!float.TryParse(min, out filter.min))
            {
                filter.min = -1;
            }

            filter.sepa = SepaBox.Checked;
            filter.express = ExpressBox.Checked;

            RefreshTable();
        }

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			ReadFilterData();
		}

		private void SepaBox_CheckedChanged(object sender, EventArgs e)
		{
			ReadFilterData();
		}

		//------------

		private void CalcPriceBox_TextChanged(object sender, EventArgs e)
		{
			RefreshCalculator();
		}

        void RefreshCalculator()
        {
            string priceString = CalcPriceBox.Text;
            string amountString = CalcAmountBox.Text;

            priceString = priceString.Replace('.', ',');
            amountString = amountString.Replace('.', ',');

            float price = 0, amount = 0;

            if (float.TryParse(priceString, out price))
            {
                BuyFeeLabel.Text = BitcoinHelper.AdjustedPrice(price, true).ToString("N2") + " €";
                SellFeeLabel.Text = BitcoinHelper.AdjustedPrice(price, false).ToString("N2") + " €";
            }
            if (float.TryParse(amountString, out amount))
            {
                BuyAmountLabel.Text = (amount * 0.995).ToString("N4") + " BTC";
                SellAmountLabel.Text = amount.ToString("N4") + " BTC";

                BuyValueLabel.Text = (amount * price).ToString("N2") + " €";
                SellValueLabel.Text = (amount * BitcoinHelper.AdjustedPrice(price, false)).ToString("N2") + " €";

            }

        }

        void RefreshView(List<Record> list, ListView view, bool buy)
        {
            int num = 0;
            foreach (var entry in list)
            {
                if(!((entry.sepa && filter.sepa) || (entry.express && filter.express)))
                {
                    continue;
                }

                if (filter.num != -1)
                {
                    if (num++ == filter.num) break;
                }
                if (filter.max != -1)
                {
                    if (filter.max > entry.max) continue;
                }
                if (filter.min != -1)
                {
                    if (filter.min < entry.min) continue;
                }

                float val = buy ? 1.005f : 0.995f;
                float ch = buy ? 1.01f : 0.99f;

                ListViewItem item = new ListViewItem(entry.info.BuyPrice.ToString("N2") + " €"); //price

                //item.SubItems.Add(entry.info.GetFeePrice(buy).ToString("N2") + " €"); //price_fee
				if(buy)
	                item.SubItems.Add(entry.info.GetSellPriceForPercentageGain(0).ToString("N2") + " €"); //price_0%
                item.SubItems.Add(entry.min.ToString());
                item.SubItems.Add(entry.max.ToString());
				if(buy)
					item.SubItems.Add(entry.info.GetSellPriceForPercentageGain(1).ToString("N2") + " €"); //price_1%
                item.SubItems.Add(entry.express ? "x" : " ");
                item.SubItems.Add(entry.sepa ? "x" : " ");
                item.SubItems.Add(entry.fullyIdentified ? "x" : " ");

                view.Items.Add(item);
            }
        }
		
        void RefreshTable()
        {
			int selB = -1, selS = -1;

			if (buyView.SelectedIndices.Count > 0)
			{
				selB = buyView.SelectedIndices[0];
			}
			if (sellView.SelectedIndices.Count > 0)
			{
				selS = sellView.SelectedIndices[0];
			}

            buyView.Items.Clear();
            sellView.Items.Clear();

            RefreshView(OfferBook.that._currentPage.buyOffers, buyView, true);
            RefreshView(OfferBook.that._currentPage.sellOffers, sellView, false);

			if (selB != -1 && selB < buyView.Items.Count)
			{
				buyView.SelectedIndices.Add(selB);
			}
			if (selS != -1 && selS < buyView.Items.Count)
			{
				buyView.SelectedIndices.Add(selS);
			}

            analysator.AnalyseNewPage(OfferBook.that._currentPage);

            GainLabel.Text = analysator._sum.ToString("N2") + " €";
        }



		//----------------- marketplace browser

		private void RefreshBrowserTimer_Tick(object sender, EventArgs e)
		{
			marketplaceBrowser.Refresh();
		}

		private void ReEvalTimer_Tick(object sender, EventArgs e)
		{
			if (OfferBook.that.Update())
			{
				RefreshTable();
				//CourseAnalysator.that.AddPage(OfferBook.that._currentPage);

				RefreshBuyPanel();

				TradeManager.that.Refresh();
			}
		}

		//----------------- analysator

		public void RefreshAnalysatorList()
		{
			string selection = (string)AutoRecordList.SelectedItem;

			AutoRecordList.Items.Clear();
			
			foreach(var page in analysator._pageList)
			{
				var ana = analysator.Analyse(page);

				if (anaFilter.onlyPossible && !ana.possible) continue;
				if (anaFilter.onlyExpress && !ana.express) continue;

				AutoRecordList.Items.Add(page.timeStamp.ToString());
			}

			AutoRecordList.SelectedItem = selection;
			/*
			if(selection != null)
			{
				foreach(string item in AutoRecordList.Items)
				{
					if(item == selection)
					{
					}
				}
			}*/
		}
		
		private void button3_Click(object sender, EventArgs e)
		{
			analysator.LoadFiles();
		}

		void RefreshAutoView(List<Record> buyList, List<Record> sellList, int max)
		{
			AutoBuyView.Items.Clear();
			AutoSellView.Items.Clear();

			int num = max;

			foreach (var entry in buyList)
			{
				ListViewItem item = new ListViewItem(entry.info.BuyPrice.ToString("N2") + " €");

				item.SubItems.Add((entry.info.GetFeePrice(true)).ToString("N2") + " €");
				item.SubItems.Add(entry.min.ToString() + " BTC");
				item.SubItems.Add(entry.max.ToString() + " BTC");
				item.SubItems.Add(entry.express ? "x" : " ");

				AutoBuyView.Items.Add(item);

				if (--num == 0) break;
			}
			num = max;
			foreach (var entry in sellList)
			{
				ListViewItem item = new ListViewItem(entry.info.BuyPrice.ToString("N2") + " €");

				item.SubItems.Add((entry.info.GetFeePrice(false)).ToString("N2") + " €");
				item.SubItems.Add(entry.min.ToString() + " BTC");
				item.SubItems.Add(entry.max.ToString() + " BTC");
				item.SubItems.Add(entry.express ? "x" : " ");

				AutoSellView.Items.Add(item);

				if (--num == 0) break;
			}
		}

		void LoadAnalysatorRecord(int i)
		{
			if (i >= 0 && i < analysator._pageList.Count)
			{
				OfferPage page = analysator._pageList[i];

				var ana = analysator.Analyse(page);

				if (anaFilter.showAll)
				{
					RefreshAutoView(page.buyOffers, page.sellOffers, anaFilter.maxNum);
				}
				else
				{
					RefreshAutoView(ana.buyOffers, ana.sellOffers, -1);
				}

				float amount = -1;
				float.TryParse(AutoAmount.Text, out amount);

				AutoAmountMin.Text = ana.min.ToString("N4") + " BTC";
				AutoAmountMax.Text = ana.max.ToString("N4") + " BTC";

				AutoInvestMin.Text = ana.GetInvestment(ana.min).ToString("N2") + " €";
				AutoInvestMax.Text = ana.GetInvestment(ana.max).ToString("N2") + " €";

				AutoNecMin.Text = ana.GetNecBitcoins(ana.min).ToString("N4") + " BTC";
				AutoNecMax.Text = ana.GetNecBitcoins(ana.max).ToString("N4") + " BTC";

				AutoGainMin.Text = ana.GetGain(ana.min).ToString("N2") + " €";
				AutoGainMax.Text = ana.GetGain(ana.max).ToString("N2") + " €";

				if (amount != -1)
				{
					AutoInvest.Text = ana.GetInvestment(amount).ToString("N2") + " €";

					AutoNec.Text = ana.GetNecBitcoins(amount).ToString("N4") + " BTC";

					AutoGain.Text = ana.GetGain(amount).ToString("N2") + " €";
				}
			}
		}

		void LoadAnalysatorFilter()
		{
			if(!float.TryParse(AnaMaxPriceBox.Text, out anaFilter.maxPrice))
			{
				anaFilter.maxPrice = -1;
			}
			if(!int.TryParse(AutoMaxNum.Text, out anaFilter.maxNum))
			{
				anaFilter.maxNum = -1;
			}

			anaFilter.onlyExpress = AutoExpressBox.Checked;
			anaFilter.onlyPossible = AutoPossibleBox.Checked;
			anaFilter.combineTrades = AutoCombineBox.Checked;
			anaFilter.showAll = AutoAllBox.Checked;

			RefreshAnalysatorList();
		}

		private void AutoRecordList_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadAnalysatorRecord(AutoRecordList.SelectedIndex);
		}

		private void AutoAmount_TextChanged(object sender, EventArgs e)
		{
			LoadAnalysatorRecord(AutoRecordList.SelectedIndex);
		}

		private void AutoPossibleBox_CheckedChanged(object sender, EventArgs e)
		{
			LoadAnalysatorFilter();
		}

		//------------------ development graph


		//----------------- Wallet

		public void RefreshBitcoinWallet()
		{
			WalletListView.Items.Clear();
			
			float currentPrice = OfferBook.that.Average(false, 10);

			if(currentPrice == 0)
			{
				refreshWallet = true;
				return;
			}

			foreach(var item in Wallet.that._content)
			{
				ListViewItem it = new ListViewItem(item.data.BuyPrice.ToString("N2") + " €");//cost
				it.SubItems.Add(item.data.GetFeePrice(true).ToString("N2") + " €"); //cost_fee
				//it.SubItems.Add((item.cost * 1.005f).ToString("N2") + " €"); //
				it.SubItems.Add(item.data.Amount.ToString("N5") + " BTC"); //amount

				it.SubItems.Add(currentPrice.ToString("N2") + " €"); //current price
				it.SubItems.Add(item.data.PaidPrice.ToString("N2") + " €"); //buy price
				it.SubItems.Add(item.data.GetGain(currentPrice).ToString("N2") + " €");//gain €
				it.SubItems.Add(item.data.GetPercentageGain(currentPrice).ToString("N1") + " %");//gain %
				it.SubItems.Add((item.share[0] * 100).ToString("N1") + " %");
				it.SubItems.Add((item.share[1] * 100).ToString("N1") + " %");
				it.SubItems.Add((item.share[2] * 100).ToString("N1") + " %");

				WalletListView.Items.Add(it);
			}
		}

		public void RefreshWallet()
		{
			AvailableMoneyLabel.Text =Wallet.that._money.RemainingMoney(VolumeOwner.ALL).ToString("N2") + " €";
			InvestedMoneyLabel.Text =Wallet.that._money.SpentMoney(VolumeOwner.ALL).ToString("N2") + " €";
			KapitalSumLabel.Text =Wallet.that._money.Kapital.ToString("N2") + " €";

			StartKapitalLabel.Text =Wallet.that._money.startKapital.ToString("N2") + " €";
			KapitalGainLabel.Text = (Wallet.that._money.Kapital-Wallet.that._money.startKapital).ToString("N2") + " €";

			AvailableResBtc.Text =Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_BTC).ToString("N2") + " €";
			AvailableResEur.Text =Wallet.that._money.RemainingMoney(VolumeOwner.RESERVE_EUR).ToString("N2") + " €";
			AvailableSpec.Text =Wallet.that._money.RemainingMoney(VolumeOwner.SPECULATION).ToString("N2") + " €";

			InvestedBtc.Text =Wallet.that._money.SpentMoney(VolumeOwner.RESERVE_BTC).ToString("N2") + " €";
			InvestedEur.Text =Wallet.that._money.SpentMoney(VolumeOwner.RESERVE_EUR).ToString("N2") + " €";
			InvestedSpec.Text =Wallet.that._money.SpentMoney(VolumeOwner.SPECULATION).ToString("N2") + " €";

			BtcShare.Text = (Wallet.that._money.share[(int)VolumeOwner.RESERVE_BTC]*100).ToString("N1") + " %";
			EurShare.Text = (Wallet.that._money.share[(int)VolumeOwner.RESERVE_EUR]*100).ToString("N1") + " %";
			SpecShare.Text = (Wallet.that._money.share[(int)VolumeOwner.SPECULATION]*100).ToString("N1") + " %";

			float price_fee = OfferBook.that.AvgFee(false, 10);

			VolResBtc.Text =Wallet.that.GetBitcoins(VolumeOwner.RESERVE_BTC).ToString("N4") + " BTC";
			VolResEur.Text =Wallet.that.GetBitcoins(VolumeOwner.RESERVE_EUR).ToString("N4") + " BTC";
			VolSpec.Text =Wallet.that.GetBitcoins(VolumeOwner.SPECULATION).ToString("N4") + " BTC";

			ValResBtc.Text = (Wallet.that.GetBitcoins(VolumeOwner.RESERVE_BTC) * price_fee).ToString("N2") + " €";
			ValResEur.Text = (Wallet.that.GetBitcoins(VolumeOwner.RESERVE_EUR) * price_fee).ToString("N2") + " €";
			ValResSpec.Text = (Wallet.that.GetBitcoins(VolumeOwner.SPECULATION) * price_fee).ToString("N2") + " €";
		
			RefreshBitcoinWallet();
		}

		private void WalletRefresher_Tick(object sender, EventArgs e)
		{
			RefreshWallet();
		}


		//----------------- Buy Panel

		public float RefreshBuyPanel()
		{
			if(buyView.SelectedIndices.Count == 0)
			{
				return 0;
			}

			int sel = buyView.SelectedIndices[0];

			BuyPriceLabel.Text = OfferBook.that.BuyOffers[sel].info.GetFeePrice(true).ToString("N2") + " €";
			float amount;
			float.TryParse(BuyAmountText.Text.Replace('.',','), out amount);

			BuyAmount.Text = (amount * 0.995).ToString("N4") + " BTC";
			BuyCostLabel.Text = (OfferBook.that.BuyOffers[sel].info.GetFeePrice(true) * amount).ToString("N2") + " €";

			return amount;
		}

		private void textBox1_TextChanged_1(object sender, EventArgs e)
		{
			RefreshBuyPanel();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			float amount = RefreshBuyPanel();

			if (amount != 0)
			{
				int sel = buyView.SelectedIndices[0];
				if(OfferBook.that.BuyOffers[sel].min <= amount && OfferBook.that.BuyOffers[sel].max >= amount)
				{
					float[] share = new float[(int)VolumeOwner.COUNT];

					share[0] = 1;

					TradeManager.that.Buy(sel, amount, share);
				}
			}
		}

		private void buyView_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshBuyPanel();
		}

		private void RefreshWalletTimer_Tick(object sender, EventArgs e)
		{
			if(refreshWallet)
			{
				refreshWallet = false;
				RefreshWallet();
			}
		}

		private void RefreshCourseInfo_Tick(object sender, EventArgs e)
		{
			/*
			if(CourseAnalysator.that.Rise(TradeDataType.AVG_3H))
			{
				MonotonyLabel.Text = "Rising";
			}
			else
			{
				MonotonyLabel.Text = "Falling";
			}

			AboveLabel.Text = CourseAnalysator.that.Under12(0) ? "below 12h mark" : "above 12h mark";

			MoreThanLabel.Text = CourseAnalysator.that.ConstLowHigh().ToString();

			string output = "waiting";
			
			if(trader.OpenBuyRequest(RequestOwner.RESERVE))
			{
				output = "buying";
			}
			else if(trader.OpenSellRequest(RequestOwner.RESERVE))
			{
				output = "selling";
			}
			ResStateLabel.Text = output;
			output = "waiting";

			if(trader.OpenBuyRequest(RequestOwner.SPEC))
			{
				output = "buying";
			}
			else if (trader.OpenSellRequest(RequestOwner.SPEC))
			{
				output = "selling";
			}

			SpecStateLabel.Text = output;*/
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Logger.Log("Application shut down!", true);
		}



		private void tb_price_TextChanged(object sender, EventArgs e)
		{
			float price, amount, fee, paid, minus_fee, zero_sell;
			
			if (!float.TryParse(tb_price.Text.Replace('.', ','), out price)) return;
			if (!float.TryParse(tb_amount.Text.Replace('.', ','), out amount)) return;
			if (!float.TryParse(tb_fee.Text.Replace('.', ','), out fee)) return;
			
			minus_fee = 1 - fee / 100;
			paid = (1 - fee / 200) * amount * price;

			zero_sell = price /( 1 - fee / 100);

			label_sellAmount.Text = (minus_fee * amount).ToString("N5") + " BTC";
			label_paid.Text = paid.ToString("N2") + " €";
			label_sellPrice.Text = zero_sell.ToString("N2") + " €";

		}

		private void button6_Click(object sender, EventArgs e)
		{
			float price, amount, fee, paid, minus_fee, zero_sell;

			if (!float.TryParse(tb_price.Text.Replace('.', ','), out price)) return;
			if (!float.TryParse(tb_amount.Text.Replace('.', ','), out amount)) return;
			if (!float.TryParse(tb_fee.Text.Replace('.', ','), out fee)) return;

			minus_fee = 1 - fee / 100;
			paid = (1 - fee / 200) * amount * price;

			zero_sell = price / (1 - fee / 100);

			WalletItem item = new WalletItem();

			item.data = new TradeAnalysation(price, amount, 0.8f);
			item.date = DateTime.Now;

			Wallet.that.BuyItem(item);
		}

		private void sellView_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			new GraphForm();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			RefreshWallet();
		}


						
		//----------------- 
    }
}
