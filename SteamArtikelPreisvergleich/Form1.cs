using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{
    public partial class Form1 : Form
    {
        PanelFavoriten Favorit;
        PanelSuche Suche;

        public Form1()
        {
            InitializeComponent();            
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            Favorit = new PanelFavoriten(this);
            Favorit.Parent = this;            

            Suche = new PanelSuche(this); Suche.Parent = this;
            Suche.Visible = true;
            Favorit.Visible = false;
        }

        private void favoritenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suche.Visible = false;
            Favorit.Visible = true;
            Favorit.refresh(sender,e);
        }

        private void sucheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suche.Visible = true;
            Favorit.Visible = false;
        }
    }
}
