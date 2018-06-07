using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamArtikelPreisvergleich
{
    class Favoriten
    {
        public List<SteamGegenstand> FavoritenListe = new List<SteamGegenstand>();

        public Favoriten()
        {
            laden();
        }

        private void erstelleDatenbank()
        {
            try
            {
                SQLiteConnection.CreateFile("Favoriten.sqlite");
                SQLiteConnection dbConnection = new SQLiteConnection("Data Source = Favoriten.sqlite; Version = 3;");
                dbConnection.Open();
                string sql = "CREATE TABLE Favoritenliste(Name TEXT, Spiel TEXT,SteamURL TEXT,BildURL TEXT,SteamPreis DECIMAL(10,5),OPSkinsPreis DECIMAL(10,5) )";
                SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
                Command.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.Message);
            }


        }

        public void laden()
        {
            FavoritenListe.Clear();
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source = Favoriten.sqlite; Version = 3;");
            dbConnection.Open();

            string sql = "SELECT * FROM Favoritenliste";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                FavoritenListe.Add(new SteamGegenstand(reader["Name"].ToString(), reader["Spiel"].ToString(), reader["SteamURL"].ToString(), reader["BildURL"].ToString(), Convert.ToDouble(reader["SteamPreis"]), Convert.ToDouble(reader["OPSkinsPreis"])));
            dbConnection.Close();
        }

        public void hinzufuegen(SteamGegenstand Artikel)
        {
            if (!bereitsInDatenbank(Artikel))
            {
                SQLiteConnection dbConnection = new SQLiteConnection("Data Source = Favoriten.sqlite; Version = 3;");
                dbConnection.Open();
                string sql = "INSERT INTO Favoritenliste(Name, Spiel,SteamURL, BildURL,SteamPreis,OPSkinsPreis) VALUES(@name, @spiel,@SteamURL,@BildURL,@SteamPreis,@OPSkinsPreis)";
                SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
                Command.Parameters.Add("@name", System.Data.DbType.String).Value = Artikel.Name;
                Command.Parameters.Add("@spiel", System.Data.DbType.String).Value = Artikel.Spiel;
                Command.Parameters.Add("@SteamURL", System.Data.DbType.String).Value = Artikel.SteamURL;
                Command.Parameters.Add("@BildURL", System.Data.DbType.String).Value = Artikel.BildURL;
                Command.Parameters.Add("@SteamPreis", System.Data.DbType.String).Value = Artikel.SteamPreis;
                Command.Parameters.Add("@OPSkinsPreis", System.Data.DbType.String).Value = Artikel.OPSkinsPreis;
                Command.ExecuteNonQuery();
                Command.Parameters.Clear();
                dbConnection.Close();
                laden();
            }
        }

        public void loeschen(SteamGegenstand Artikel)
        {
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source = Favoriten.sqlite; Version = 3;");
            dbConnection.Open();
            string sql = "DELETE FROM Favoritenliste WHERE Name = '" + Artikel.Name + "';";
            SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
            Command.ExecuteNonQuery();
            dbConnection.Close();
            laden();
        }

        private bool bereitsInDatenbank(SteamGegenstand Artikel)
        {
            bool Ergebnis = false;
            foreach (SteamGegenstand sg in FavoritenListe) { if (sg.Name == Artikel.Name) Ergebnis = true; }
            return Ergebnis;
        }

        public void preiseAktualisieren(SteamGegenstand Artikel, int Shop)
        {
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source = Favoriten.sqlite; Version = 3;");
            dbConnection.Open(); string sql;
            if (Shop == 0) sql = "UPDATE Favoritenliste SET SteamPreis = @SteamPreis WHERE Name  = '";
            else sql = "UPDATE Favoritenliste SET OPSkinsPreis = @OPSkinsPreis WHERE Name  = '";
            sql += Artikel.Name + "';";
            SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
            if (Shop == 0) Command.Parameters.Add("@SteamPreis", System.Data.DbType.String).Value = Artikel.SteamPreis;
            else Command.Parameters.Add("@OPSkinsPreis", System.Data.DbType.String).Value = Artikel.OPSkinsPreis;
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            dbConnection.Close();
            laden();
        }
    }

}
