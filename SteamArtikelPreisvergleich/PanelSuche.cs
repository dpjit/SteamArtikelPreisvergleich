using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{   

    class PanelSuche:Panel
    {
        List<SteamGegenstand> Suchergebnis = new List<SteamGegenstand>();
        List<PanelSuchergebnis> Panels = new List<PanelSuchergebnis>();
        Label KeineErgebnisse = new Label();
        Button Suche = new Button();
        TextBox Suchfeld = new TextBox();
        Form1 f;

        public PanelSuche(Form1 f1)
        {
            this.AutoScroll = true;            
            this.Size = new Size(f1.Width, f1.Height-40);
            this.Location = new Point(0, 0);                     

            Suche.Parent = this;
            Suche.Text = "Suche";
            Suche.Location = new Point(150, 34);
            Suche.AutoSize = true;
            Suche.Click += buttonSuche_Click;
            
            Suchfeld.Parent = this;
            Suchfeld.Location = new Point(25,35);
            Suchfeld.AutoSize = true;

            f = f1;
        }

        private void buttonSuche_Click(object sender, EventArgs e)
        {
            KeineErgebnisse.Dispose();
            SteamMarktSuche NeueSuche = new SteamMarktSuche();
            Suchergebnis = NeueSuche.Suchergebnis(Suchfeld.Text);
            
            foreach (PanelSuchergebnis pa in Panels) pa.Visible = false;
            Panels.Clear();

            int PanelPosition = 100;
            int PanelNummer = 0;
            PanelSuchergebnis p;
            foreach (SteamGegenstand i in Suchergebnis)
            {
                if (i.Spiel == "PLAYERUNKNOWN'S BATTLEGROUNDS" || i.Spiel == "Counter-Strike: Global Offensive")
                {
                    p = new PanelSuchergebnis(10, PanelPosition, i, PanelNummer);
                    string PanelName = "myPanel" + PanelNummer.ToString();
                    p.Name = PanelName;
                    p.Parent = this;
                    p.Click += p_Click;
                    Panels.Add(p);

                    PanelPosition += 140;
                    PanelNummer++;
                }
            }
            if (Panels.Count < 1)
            {                
                KeineErgebnisse.Parent = this;
                KeineErgebnisse.Location = new Point(100,100);
                KeineErgebnisse.Text = "Suche ergab keine Ergebnisse. Bitte verfeinern";
                KeineErgebnisse.AutoSize = true;
            }

        }

        private void p_Click(object sender, EventArgs e)
        {
            PanelSuchergebnis p = (PanelSuchergebnis)sender;
            //Artikelseite öffnen Panel verstecken
            this.Visible = false;
            PanelArtikelseite ArtikelPanel = new PanelArtikelseite(Suchergebnis[Convert.ToInt32(p.Name.Substring(7))], this.Height);
            ArtikelPanel.Parent = f;
            ArtikelPanel.Zurueck.Click += show;            
            ArtikelPanel.BringToFront();
        }
        
        private void artikelseiteZurueck_Click(object sender, EventArgs e)
        {
            this.Visible = true;
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
