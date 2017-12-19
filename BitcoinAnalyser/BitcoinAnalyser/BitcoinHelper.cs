using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace BitcoinAnalyser
{
    public static class BitcoinHelper
    {
        static public float AdjustedPrice(float price, bool buy)
        {
            return price * (buy ? 1.005f : 0.995f);
        }
        static float CorrespondingPrice(float price, bool buy, float percentage)
        {

            return 0;//info.Price * (buy ? )
        }

        public static string FloatToCurrency(float v)
        {
            return v.ToString("N2") + " €";
        }

        static string RecordToString(Record rec)
        {
            return '\t' + FloatToCurrency(rec.info.BuyPrice) + '\t' + FloatToCurrency(rec.info.GetFeePrice(rec.buyOffer)) + '\t' + rec.min.ToString("N4") + '\t' + rec.max.ToString("N4") + Environment.NewLine;
        }
        static public void SavePage(string path, OfferPage page)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement("record");
            doc.AppendChild(root);

            XmlElement date = doc.CreateElement("info");
            root.AppendChild(date);
            date.SetAttribute("date", System.DateTime.Now.ToString());

            XmlElement table = doc.CreateElement("table");
            root.AppendChild(table);
            table.SetAttribute("offer-type", "buy");

            foreach (var rec in page.buyOffers)
            {
                XmlElement record = doc.CreateElement("sheet");
                table.AppendChild(record);

                record.SetAttribute("info.Price", rec.info.BuyPrice.ToString());
                record.SetAttribute("price_fee", rec.info.GetFeePrice(rec.buyOffer).ToString());
                record.SetAttribute("min", rec.min.ToString());
                record.SetAttribute("max", rec.max.ToString());
                record.SetAttribute("sepa", rec.sepa.ToString());
                record.SetAttribute("express", rec.express.ToString());
                record.SetAttribute("req_ident", rec.fullyIdentified.ToString());
            }

            table = doc.CreateElement("table");
            root.AppendChild(table);
            table.SetAttribute("offer-type", "sell");

            foreach (var rec in page.sellOffers)
            {
                XmlElement record = doc.CreateElement("sheet");
                table.AppendChild(record);

                record.SetAttribute("price", rec.info.BuyPrice.ToString());
                record.SetAttribute("price_fee", rec.info.GetFeePrice(rec.buyOffer).ToString());
                record.SetAttribute("min", rec.min.ToString());
                record.SetAttribute("max", rec.max.ToString());
                record.SetAttribute("sepa", rec.sepa.ToString());
                record.SetAttribute("express", rec.express.ToString());
                record.SetAttribute("req_ident", rec.fullyIdentified.ToString());
            }



            System.IO.Directory.CreateDirectory(path);

            doc.Save(path + page.timeStamp.ToString().Replace(':', '_') + ".xml");

        }
		
		static public bool LoadPage(string file, out OfferPage page)
        {
			/*
            XmlDocument doc = new XmlDocument();

            page = new OfferPage();
            
            try
            {
                doc.Load(file);
            }
            catch (Exception ex)
            {
                return false;
            }


            XmlElement root = doc.DocumentElement;

            var infoList = root.GetElementsByTagName("info");

            if(infoList.Count == 0)
            {
                return false;
            }

            XmlAttribute date = infoList[0].Attributes["date"]; ;
            page.timeStamp = DateTime.Parse(date.InnerText);

            var tables = root.GetElementsByTagName("table");

            if(tables.Count != 2)
            {
                return false;
            }
			int turn = 0;
            foreach (XmlElement table in tables)
            {
                bool buy = table.GetAttribute("offer-type") == "buy";

				turn++;

                var recs = table.GetElementsByTagName("sheet");
                foreach (XmlElement xmlRec in recs)
                {
                    Record record = new Record();

                    record.buyOffer = buy;

                    if (!float.TryParse(xmlRec.GetAttribute("price"), out record.info.Price)) return false;
                    if (!float.TryParse(xmlRec.GetAttribute("price_fee"), out record.info.GetFeePrice(buy)) return false;
                    if (!float.TryParse(xmlRec.GetAttribute("min"), out record.min)) return false;
                    if (!float.TryParse(xmlRec.GetAttribute("max"), out record.max)) return false;
                    if (!bool.TryParse(xmlRec.GetAttribute("sepa"), out record.sepa)) return false;
                    if (!bool.TryParse(xmlRec.GetAttribute("express"), out record.express)) return false;
                    if (!bool.TryParse(xmlRec.GetAttribute("req_ident"), out record.fullyIdentified)) return false;

					if (buy) page.buyOffers.Add(record);
					else page.sellOffers.Add(record);
                }
            }

			if (turn != 2) return false;
			*/
			page = new OfferPage();
            return true;          
        }
    }
}
