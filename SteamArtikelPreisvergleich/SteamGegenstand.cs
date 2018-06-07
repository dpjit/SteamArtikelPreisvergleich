using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SteamArtikelPreisvergleich
{
    class SteamGegenstand
    {
        public string Name;
        public string Spiel;
        public string SteamURL;
        public string BildURL;
        public double SteamPreis = new double();
        public double OPSkinsPreis = new double();

        public SteamGegenstand() { }
        public SteamGegenstand(string name, string spiel, string steamURL, string bildURL)
        {
            Name = name;
            Spiel = spiel;
            SteamURL = steamURL;
            BildURL = bildURL;

        }

        public SteamGegenstand(string name, string spiel, string steamURL, string bildURL,double steampreis,double opskinspreis)
        {
            Name = name;
            Spiel = spiel;
            SteamURL = steamURL;
            BildURL = bildURL;
            SteamPreis = steampreis;
            OPSkinsPreis = opskinspreis;
        }

        public void getSteamPreis()
        {
                string URL = "http://steamcommunity.com/market/priceoverview/?currency=1&appid=" + getAppID() + "&market_hash_name=";
                System.Threading.Thread.Sleep(3000);
                string json = new System.Net.WebClient().DownloadString(URL + this.Name);
                JObject o = JObject.Parse(json);
                string PreisString = (string)o["lowest_price"];
                this.SteamPreis = Convert.ToDouble(PreisString.Replace(".", ",").Substring(1));
                Favoriten f = new Favoriten();f.preiseAktualisieren(this,0);          
           
        }

        private int getAppID()
        {
            int AppID = new int();
            switch (this.Spiel)
            {
                case "PLAYERUNKNOWN'S BATTLEGROUNDS": AppID = 578080; break;
                case "Counter-Strike: Global Offensive": AppID = 730; break;
            }            
            return AppID;           
        }

        public void getOPSkinsPreis()
        {
            string URL = "https://api.opskins.com/IPricing/GetAllLowestListPrices/v1/?appid="+getAppID();
            string json = new System.Net.WebClient().DownloadString(URL);
            JObject o = JObject.Parse(json);           
        
            string PreisString = (string)o["response"][this.Name]["price"];
            OPSkinsPreis = Convert.ToDouble(PreisString.Substring(0, PreisString.Length - 2)+"," +PreisString.Substring(PreisString.Length-2));
            Favoriten f = new Favoriten(); f.preiseAktualisieren(this, 1);
        }

        public string getShopLink(int ShopID)
        {
            string ShopLink = string.Empty;
            switch (ShopID)
            {
                case 0: ShopLink = "https://steamcommunity.com/market/listings/"+ getAppID() + "/" + this.Name; break;
                case 1: ShopLink = "https://opskins.com/?loc=shop_search&app="+ getAppID() + "_2&search_item=" + this.Name; break;
            }
            return ShopLink;
        }
    }
}

