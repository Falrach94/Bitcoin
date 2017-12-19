using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinAnalyser
{
	class AnalysationFilter
	{
		public float maxPrice;
		public int maxNum;
		public bool onlyExpress, combineTrades, showAll, onlyPossible;
	}

	class Analysation
	{
		public List<Record> buyOffers = new List<Record>();
		public List<Record> sellOffers = new List<Record>();

		public bool express = true;

		public float min, max;
		public bool possible;

		public float GetInvestment(float amount)
		{
			return amount * buyOffers[0].info.GetFeePrice(true);
		}
		public float GetNecBitcoins(float amount)
		{
			return amount * 1.005f;
		}
		public float GetGain(float amount)
		{
			return amount * (sellOffers[0].info.GetFeePrice(false) - buyOffers[0].info.GetFeePrice(false));
		}

	}

    class Analysator
    {
        float _lastBuy, _lastSell;
        public float _sum = 0;
		
		public Form1 _form;

        public List<OfferPage> _pageList = new List<OfferPage>();

        public void AnalyseNewPage(OfferPage page)
        {
			/*
            if(page.buyOffers.Count > 0 && page.sellOffers.Count > 0)
            {
                float buyOffer = page.buyOffers[0].info.GetFeePrice(true);
                float sellOffer = page.sellOffers[0].info.GetFeePrice(false);
                if(_lastBuy !=  buyOffer && _lastSell != sellOffer)
                {
                    if (buyOffer < sellOffer && false)
                    {
                        BitcoinHelper.Save ("C:/bitcoins/", page);
                        _sum += (sellOffer - buyOffer) * page.sellOffers[0].max;

                        _lastBuy = buyOffer;
                        _lastSell = sellOffer;

                        _pageList.Add(page);
                    }
                }
            }*/
        }

		public Analysation Analyse(OfferPage page)
		{
			Analysation ana = new Analysation();

			ana.buyOffers.Add(page.buyOffers[0]);
			ana.sellOffers.Add(page.sellOffers[0]);


			//buy: 0.2 - 0.4
			//sell: 0.1 - 0.5
			// -> 0.2 - 0.3
			//   s_min <= b_min <= s_max 
			//|| b_min <= s_min <= b_max

			Record buy = page.buyOffers[0];
			Record sell = page.sellOffers[0];

			//dows buy or sell offer limit?
			bool lowerBuy = sell.min <= buy.min && buy.min <= sell.max;
			bool lowerSell = buy.min <= sell.min && sell.min <= buy.max;

			bool upperBuy = sell.min <= buy.max && buy.max <= sell.max;
			bool upperSell = buy.min <= sell.max && sell.max <= buy.max;

			ana.possible = lowerBuy || lowerSell;

			float lowerLimit = lowerBuy ? buy.min : sell.min;
			float upperLimit = upperBuy ? buy.max : sell.max;

			ana.possible = page.buyOffers[0].min <= page.sellOffers[0].max && page.buyOffers[0].max >= page.sellOffers[0].min;
			ana.min = lowerLimit;
			ana.max = upperLimit;

			foreach (var rec in ana.buyOffers)
			{
				ana.express &= rec.express;
			}
			foreach (var rec in ana.sellOffers)
			{
				ana.express &= rec.express;
			}

			return ana;
		}

        public void LoadFiles()
        {/*
            string path = "C:/bitcoins";

			_pageList.Clear();

            var files = System.IO.Directory.GetFiles(path, "*.xml");

            foreach(var file in files)
            {
                OfferPage page;
				if(BitcoinHelper.LoadPage(file, out page))
				{
					_pageList.Add(page);
				}
            }

			_form.RefreshAnalysatorList();*/
		}

		public void LoadPage(OfferPage page)
		{
			Analysation ana = Analyse(page);
		}
		
    }
}
