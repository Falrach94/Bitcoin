using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace BitcoinAnalyser
{
    public class Record
    {
        public float min, max;
        public bool buyOffer;

        public bool express, sepa = true;
        public bool fullyIdentified;

		public TradeAnalysation info;

		public int id;

        //public float 

		public override bool Equals(object obj)
		{
			bool res = true;

			Record cObj = (Record)obj;

			res &= cObj.info.BuyPrice == info.BuyPrice;
			res &= cObj.info.GetFeePrice(true) == info.GetFeePrice(true);

			res &= cObj.min == min;
			res &= cObj.max == max;

			res &= cObj.buyOffer == buyOffer;

			res &= cObj.express == express;
			res &= cObj.sepa == sepa;
			res &= cObj.fullyIdentified == fullyIdentified;

			return res;
		}

        public void SetData(float p_price, float p_min, float p_max)
        {
            info = new TradeAnalysation(p_price, 0, 0.08f);
			
            min = p_min;
            max = p_max;
        }
    }

    public class OfferPage
    {
        public float buyPrice, sellPrice;
        public List<Record> sellOffers = new List<Record>(), buyOffers = new List<Record>();
        public DateTime timeStamp;
    }
}
