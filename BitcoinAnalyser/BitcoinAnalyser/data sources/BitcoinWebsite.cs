using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Windows.Forms;

namespace BitcoinAnalyser
{
    
    class BitcoinWebsite
    {
        System.Windows.Forms.WebBrowser _browser;

        string url = "https://www.bitcoin.de/de/btceur/market";

        public bool Init(System.Windows.Forms.WebBrowser browser)
        {
            bool res = false;

            _browser = browser;
            
            browser.Navigate(url);

            return res;
        }
        
        Record ReadRecord(HtmlElement tr)
        {
            string minString = "", maxString = "", priceString = "";

            HtmlElementCollection tdList = tr.GetElementsByTagName("td");

            int i = 0;

            Record rec = new Record();
            
            /*
            <tr class="dnone" id="trade_id_7781837" data-trade-id="7781837" data-critical-price-formatted="7.430,18&nbsp;" data-critical-price="7430.18"data-amount="0.12723682" data-trade-type="order">
	            <td data-cell="amount">0,12723682 (0,0081)</td>
	            <td class="aright" data-cell="price">7.430,18&nbsp;€</td>
	            <td class="aright" data-cell="volume">945,39&nbsp;€</td>
	            <td class="hidden-xs" data-cell="payment_methods">
		            <div class="prelative">
			            <img title="Sitz der Bank: Deutschland" alt="Sitz der Bank: Deutschland" src="/images/currency_icon/de.png">
		            </div>
	            </td>
	            <td class="p0 hidden-xs" data-cell="not_used"></td>
	            <td class="w60 hidden-xs" data-cell="additional_info">
		            <img title="Express-Handel" class="mbm2" alt="Express-Handel" src="/images/icons/fidor.png" data-express="express">&nbsp;
		            <img title="Vollständig identifizierter User" class="mbm2" alt="Vollständig identifizierter User" src="/images/icons/verified.png">
	            </td>
	            <td data-cell="buy_sell_action">
		            <a class="c6" href="/de/btceur/sellOrder?trade_id=7781837">VERKAUFEN</a>
                </td>
            </tr>
            */

            switch (tr.GetAttribute("data-trade-type"))
            {
                case "offer":
                    rec.buyOffer = true;
                    break;
                case "order":
                    rec.buyOffer = false;
                    break;
            }

            foreach(HtmlElement td in tdList)
            {
                if(td.GetAttribute("data-cell") == "additional_info")
                {
                    var imgList = td.GetElementsByTagName("img");
                    foreach(HtmlElement img in imgList)
                    {
                        switch(img.GetAttribute("title"))
                        {
                            case "Express-Handel oder Standard-Überweisung":
                                rec.express = true;
                                rec.sepa = true;
                                break;
                            case "Express-Handel":
                                rec.express = true;
                                rec.sepa = false;
                                break;
                            case "Vollständig identifizierter User":
                                rec.fullyIdentified = true;
                                break;
                        }
                    }
                }
            }


            string line = tdList[0].InnerText;


            for(i = 0; i < line.Length && line[i] != ' '; i++)
            {
                minString += line[i];    
            }
            for(i += 2; i < line.Length && line[i] != ')'; i++)
            {
                maxString += line[i];
            }
            priceString = tr.GetAttribute("data-critical-price");

            float max, min, price;

            float.TryParse(minString, out max);
            float.TryParse(maxString, out min);

            priceString = priceString.Replace('.', ',');

            float.TryParse(priceString, out price);

            rec.SetData(price, min, max);

            return rec;
        }

		public bool GetCurrentPage(out OfferPage page)
		{
			page = new OfferPage();

			try
			{


				var tableList = _browser.Document.Body.Parent.GetElementsByTagName("table");

				page.timeStamp = System.DateTime.Now;

				string html = _browser.Document.Body.Parent.OuterHtml;

				/*
				<table class="list bg1 wp100 brd1t text-right">            
					<tbody class="fs11" id="trade_offer_results_table_body">
						<tr id="trade_id_7749755" data-trade-type="offer" data-amount="0.20000000" data-critical-price="7070.00000000" data-critical-price-formatted="7.070,00&nbsp;" data-trade-id="7749755">
							<td> 0,2 (0,01) </td>
							<td class="aright">7.070,00&nbsp;€</td>
							<td class="aright">1.414,00&nbsp;€</td>
							<td class="hidden-xs"><div class="prelative"> <img title="Sitz der Bank: Deutschland" alt="Sitz der Bank: Deutschland" src="/images/currency_icon/de.png"> </div> </td>
							<td class="hidden-xs p0">&nbsp;</td>
							<td class="hidden-xs w60" data-cell="additional_info"><img title="Vollständig identifizierter User" class="mbm2" alt="Vollständig identifizierter User" src="/images/icons/verified.png"></td>
							<td> <a class="c6" href="/de/btceur/buyOffer?trade_id=7749755">KAUFEN</a> </td> 
						</tr>
             
						<tr class="dnone" id="trade_id_7781837" data-trade-id="7781837" data-critical-price-formatted="7.430,18&nbsp;" data-critical-price="7430.18"data-amount="0.12723682" data-trade-type="order">
							<td data-cell="amount">0,12723682 (0,0081)</td>
							<td class="aright" data-cell="price">7.430,18&nbsp;€</td>
							<td class="aright" data-cell="volume">945,39&nbsp;€</td>
							<td class="hidden-xs" data-cell="payment_methods">
								<div class="prelative">
									<img title="Sitz der Bank: Deutschland" alt="Sitz der Bank: Deutschland" src="/images/currency_icon/de.png">
								</div>
							</td>
							<td class="p0 hidden-xs" data-cell="not_used"></td>
							<td class="w60 hidden-xs" data-cell="additional_info">
								<img title="Express-Handel" class="mbm2" alt="Express-Handel" src="/images/icons/fidor.png" data-express="express">&nbsp;
								<img title="Vollständig identifizierter User" class="mbm2" alt="Vollständig identifizierter User" src="/images/icons/verified.png">
							</td>
							<td data-cell="buy_sell_action">
								<a class="c6" href="/de/btceur/sellOrder?trade_id=7781837">VERKAUFEN</a>
							</td>
						</tr>
              
             
				 */

				foreach (HtmlElement table in tableList)
				{
					if (table.GetAttribute("classname") == "list bg1 wp100 brd1t text-right")
					{
						var tableBody = table.GetElementsByTagName("tbody");

						foreach (HtmlElement body in tableBody)
						{
							if (body.GetAttribute("classname") == "fs11")
							{
								foreach (HtmlElement element in body.GetElementsByTagName("tr"))
								{
									var rec = ReadRecord(element);
									
									if (rec.buyOffer)
									{
										rec.id = page.buyOffers.Count;

										page.buyOffers.Add(rec);
									}
									else
									{
										rec.id = page.sellOffers.Count;

										page.sellOffers.Add(rec);
									}
								}
							}
						}
					}
				}
				return true;
			}
			catch(Exception ex)
			{
				//MessageBox.Show(ex.ToString());
				return false;
			}
		}
    }
}
