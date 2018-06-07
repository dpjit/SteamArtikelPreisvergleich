using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{
    class PanelSuchergebnis:Panel
    {
        Label Bezeichnung = new Label();
        Label Spiel = new Label();
        Label PanleName = new Label();
        PictureBox Bild = new PictureBox();
        public Button Favorit = new Button();
        SteamGegenstand Artikel = new SteamGegenstand();
        Favoriten fav;

        public PanelSuchergebnis( int x, int y,SteamGegenstand a,int panelindex)
        {
            Artikel = a;
            this.Size = new Size(700, 120);
            this.Location = new Point(x, y);
            this.BorderStyle = BorderStyle.Fixed3D;
            //this.BackColor = Color.Red;

            Favorit.Parent = this;
            Favorit.Location = new Point(500, 60);
            Favorit.Name = panelindex.ToString();

            fav = new Favoriten();
            if (fav.FavoritenListe.Exists(z => z.Name == Artikel.Name))
            {
                Favorit.Text = "Favorit entfernen";
                Favorit.Click += favoritenLoeschen_Click;
            }

            else { Favorit.Text = "Favorit hinzufügen";
                Favorit.Click += favoritenHinzufuegen_Click;
            }
            Favorit.AutoSize = true;

            Bezeichnung.Parent = this;
            Bezeichnung.Location = new Point(150, 30);
            Bezeichnung.Text = Artikel.Name;
            Bezeichnung.AutoSize = true;            

            Spiel.Parent = this;
            Spiel.Location = new Point(150, 90);
            Spiel.Text = Artikel.Spiel;
            Spiel.AutoSize = true;

            Bild.Parent = this;
            Bild.Location = new Point(10, 10);
            Bild.Size = new Size(100, 100);
            Bild.SizeMode = PictureBoxSizeMode.Zoom;
            Bild.Load(Artikel.BildURL);           

        }

        private void favoritenHinzufuegen_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(this.Name);
            Favoriten f = new Favoriten();
            f.hinzufuegen(Artikel);
            //zu favoriten datenbank hinzufügen

        }

        private void favoritenLoeschen_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(this.Name);
            Favoriten f = new Favoriten();
            f.loeschen(Artikel);
            //zu favoriten datenbank hinzufügen

        }
       

    }
}
