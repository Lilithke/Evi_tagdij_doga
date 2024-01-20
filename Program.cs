using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Evi_tagdij_doga
{
    internal class Program
    {
        static List<Ugyfel>UgyfelList = new List<Ugyfel>();
        static MySqlConnection connection = null;
        static MySqlCommand command = null;
        static void Main(string[] args)
        {
            beolvas();
            TagokListazas();
            ujTagFelvetele();
            ujTagTorles();
            Console.WriteLine("Vége!");
            Console.ReadLine();
        }

        private static void ujTagTorles()
        {
            Ugyfel ugyfel = new Ugyfel(1014, "Bátony Teri", 1999, 3070, "H");
            command.CommandText = "DELETE FROM `ugyfel` WHERE 0";
            command.Parameters.Clear();

        }

        private static void ujTagFelvetele()
        {
            Ugyfel ugyfel = new Ugyfel(1014,"Bátony Teri", 1999, 3070, "H");
            command.CommandText = "INSERT INTO `ugyfel`(`azon`, `nev`, `szulev`, `irszam`, `orsz`) VALUES (@azon,@nev,@szulev,@irszam,@orsz)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@azon", ugyfel.azon);
            command.Parameters.AddWithValue("@nev", ugyfel.nev);
            command.Parameters.AddWithValue("@szulev", ugyfel.szulev);
            command.Parameters.AddWithValue("@irsz", ugyfel.irszam);
            command.Parameters.AddWithValue("@orsz", ugyfel.orsz);
            try 
            { 
                if(connection.State != System.Data.ConnectionState.Open) 
                { 
                    connection.Open();
                }
                command.ExecuteNonQuery();
                connection.Close();
            } 
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            
           
        }

        private static void TagokListazas()
        {
            foreach (Ugyfel item in UgyfelList)
            {
                Console.WriteLine(item);
            }
        }

        private static void beolvas()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "tagdij";
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.ConnectionString);
            command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM `ugyfel`";
                using(MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Ugyfel uj = new Ugyfel(dr.GetInt32("azon"), dr.GetString("nev"), dr.GetInt32("szulev"), dr.GetInt32("irszam"), dr.GetString("orsz"));
                        UgyfelList.Add(uj);
                    }
                }
                connection.Close();

            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
