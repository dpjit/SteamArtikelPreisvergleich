using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{
    class PanelArtikelseite:Panel
    {
        public Button Zurueck = new Button();
        public Button Favorit = new Button();
        PictureBox Bild = new PictureBox();
        Label Bezeichnung = new Label();
        Label Spiel = new Label();
        Label PreisSteam = new Label();
        Label PreisOPSkins = new Label();
        PictureBox SteamShopLink = new PictureBox();
        PictureBox OPSkinsShopLink = new PictureBox();
        Favoriten f = new Favoriten();
        SteamGegenstand Artikel = new SteamGegenstand();

        public PanelArtikelseite(SteamGegenstand a,int panelheight)
        {
            Artikel = a;
            this.Size = new Size(700, panelheight-180);
            this.Location = new Point(100, 140);
                        
            Favorit.Parent = this;
            Favorit.Name = "ButtonFavoritHinzufuegen";    
            if (f.FavoritenListe.Exists(x => x.Name == Artikel.Name))
            {
                Favorit.Text = "Aus Favoriten";
                Favorit.Click += FavoritLoeschen_Click;
            }
            else
            {
                Favorit.Text = "Zu Favoriten";
                Favorit.Click += FavoritHinzufuegen_Click;
            }
            Favorit.Location = new Point(this.Width - Favorit.Width, 0);
            Favorit.AutoSize = true;
            
            PreisSteam.Parent = this;
            PreisSteam.Location = new Point(300, 250);
            PreisSteam.Name = "PreisSteam";
            Artikel.getSteamPreis();
            PreisSteam.Text = "$ "+Artikel.SteamPreis.ToString();
            PreisSteam.AutoSize = true;

            string SteamShopLinkString = Artikel.getShopLink(0);
            SteamShopLink.Parent = this;
            SteamShopLink.Size = new Size(100, 100);
            SteamShopLink.Image = Image.FromFile("../../steam-icon.png");
            SteamShopLink.SizeMode = PictureBoxSizeMode.Zoom;
            SteamShopLink.Location = new Point(280, 150);
            //SteamShopLink.Text = Artikel.getShopLink(0).ToString();
            SteamShopLink.Click += steamShopLink_Click;

            PreisOPSkins.Parent = this;
            PreisOPSkins.Location = new Point(500, 250);
            PreisOPSkins.Name = "PreisOPSkins";
            Artikel.getOPSkinsPreis();
            PreisOPSkins.Text = "$ " + Artikel.OPSkinsPreis.ToString();
            PreisOPSkins.AutoSize = true;

            string OPSkinsShopLinkString = Artikel.getShopLink(1);
            OPSkinsShopLink.Parent = this;
            OPSkinsShopLink.Size = new Size(100, 100);
            OPSkinsShopLink.Image = Image.FromFile("../../opskins-icon.png");
            OPSkinsShopLink.SizeMode = PictureBoxSizeMode.Zoom;
            OPSkinsShopLink.Location = new Point(480, 150);
            
            //OPSkinsShopLink.Text = Artikel.getShopLink(1).ToString();
            OPSkinsShopLink.Click += opskinsShopLink_Click;

            Zurueck.Parent = this;       
            Zurueck.Name = "ButtonZurueck";
            Zurueck.Text = "Zurück";
            Zurueck.AutoSize = true;
            Zurueck.Click += buttonZurueck_Click;
            Zurueck.Location = new Point(this.Width - Zurueck.Width, this.Height - Zurueck.Height);

            Bezeichnung.Parent = this;
            Bezeichnung.Location = new Point(260, 30);
            Bezeichnung.Text = Artikel.Name;
            Bezeichnung.AutoSize = true;

            Spiel.Parent = this;
            Spiel.Location = new Point(260, 90);
            Spiel.Text = Artikel.Spiel;
            Spiel.AutoSize = true;

            Bild.Parent = this;
            Bild.Location = new Point(0, 0);
            Bild.Size = new Size(240, 240);
            Bild.SizeMode = PictureBoxSizeMode.Zoom;
            Bild.Load(Artikel.BildURL);

        }

        private void FavoritHinzufuegen_Click(object sender, EventArgs e)
        {
            Favorit.Text = "Hinzugefügt";
            f.hinzufuegen(Artikel);
        }

        private void FavoritLoeschen_Click(object sender, EventArgs e)
        {
            Favorit.Text = "Gelöscht";
            f.loeschen(Artikel);           
        }

        public void buttonZurueck_Click(object sender, EventArgs e)
        {
            this.Dispose();                      
        }

        private void steamShopLink_Click(object sender, EventArgs e)
        {            
            System.Diagnostics.Process.Start(Artikel.getShopLink(0));
        }
        private void opskinsShopLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Artikel.getShopLink(1));
        }
    }
}
