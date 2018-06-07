using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{
    class PanelFavoriten:Panel
    {
        Form1 f;
        Button NeuLaden = new Button();

        List<PanelSuchergebnis> Panels = new List<PanelSuchergebnis>();
        Favoriten fav = new Favoriten();

        public PanelFavoriten(Form1 f1)
        {            
            this.AutoScroll = true;
            this.Size = new Size(f1.Width, f1.Height-40);
            this.Location = new Point(0, 0);

            NeuLaden.Parent = this;
            NeuLaden.Text = "NeuLaden";
            NeuLaden.Location = new Point(250, 50);
            NeuLaden.Click += refresh;            

            panelsNeuLaden();
            f = f1;
        }

            
        public void refresh(object sender, EventArgs e)
        {
            panelsNeuLaden();
        }

        private void panelsNeuLaden()
        {
            fav.laden();
            foreach (PanelSuchergebnis pa in Panels) pa.Visible = false;
            Panels.Clear();

            int PanelPosition = 100;
            int PanelNummer = 0;
            PanelSuchergebnis p;
            foreach (SteamGegenstand i in fav.FavoritenListe)
            {
                p = new PanelSuchergebnis(10, PanelPosition, i, PanelNummer);
                string PanelName = "Favorit" + PanelNummer.ToString();

                p.Name = PanelName;
                p.Parent = this;
                p.Click += p_Click;
                Label SteamPreis = new Label();
                Label OPSkinsPreis = new Label();
                SteamPreis.Parent = p;
                i.getSteamPreis();
                SteamPreis.Text = "$ " + i.SteamPreis.ToString();
                SteamPreis.Location = new Point(450, 100);
                OPSkinsPreis.Parent = p;
                i.getOPSkinsPreis();
                OPSkinsPreis.Text = "$ " + i.OPSkinsPreis.ToString();
                OPSkinsPreis.Location = new Point(550, 100);
                Panels.Add(p);
                p.Favorit.Click += refresh;

                PanelPosition += 140;
                PanelNummer++;
            }
        }

        private void p_Click(object sender, EventArgs e)
        {
            PanelSuchergebnis p = (PanelSuchergebnis)sender;
            //Artikelseite öffnen Panel verstecken
            this.Visible = false;
            PanelArtikelseite ArtikelPanel = new PanelArtikelseite(fav.FavoritenListe[Convert.ToInt32(p.Name.Substring(7))], this.Height);
            ArtikelPanel.Parent = f;
            ArtikelPanel.Zurueck.Click += show;
            ArtikelPanel.BringToFront();
        }        
          

        public void show(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        public void hide(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }
}
