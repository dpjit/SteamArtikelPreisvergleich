using Supremes;
using Supremes.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamArtikelPreisvergleich
{
    class SteamMarktSuche
    {           
        public List<SteamGegenstand> Suchergebnis(string suchstring)
        {
            string SteamSuchURL = "https://steamcommunity.com/market/search?q=";
            Document doc = Dcsoup.Parse(new Uri(SteamSuchURL + suchstring), 3000);

            string searchResultsRowsHTML = doc.Select("div#searchResultsRows").ToString();
            Elements Ergebnis = Dcsoup.Parse(searchResultsRowsHTML).Select("a.market_listing_row_link");
            List<SteamGegenstand> Suchergebnis = new List<SteamGegenstand>();
            Suchergebnis.Clear();
            //string  erg = "";
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Element Artikel = Ergebnis.ElementAt(i);

                    string Name = Artikel.Select("span.market_listing_item_name").Text.ToString(); //market_listing_item_name
                    string Spiel = Artikel.Select("span.market_listing_game_name").Text.ToString(); //market_listing_game_name
                    string SteamURL = Artikel.Attr("href"); //market_listing_row_link
                    string BildURL = Artikel.Select("img").Attr("src");
                    BildURL = BildURL.Substring(0, BildURL.LastIndexOf("/"));

                    Suchergebnis.Add(new SteamGegenstand(Name, Spiel, SteamURL, BildURL));
                    //erg += Name + "\n" + Spiel + "\n" + SteamURL + "\n" + BildURL + "\n\n";
                }
                catch { break; }
            }
            return Suchergebnis;
        }


    }
}
