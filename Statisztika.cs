using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztaliKonzolos
{
    internal class Statisztika
    {
        static MySqlConnection connection = null;
        static MySqlCommand command = null;
        private List<Book> books = new List<Book>();

        public Statisztika()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Clear();
            sb.Server = "localhost";
            //sb.Port = 3306;
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "book";
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.ConnectionString);
            command = connection.CreateCommand();
            try
            {
                kapcsolatNyit();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Nem sikerült a kapcsolatot létesíteni az adatbázissal!" + ex.Message);
                Environment.Exit(0);
            }
            finally
            {
                kapcsolatZar();
            }
        }

        private void kapcsolatZar()
        {
            if (connection.State != System.Data.ConnectionState.Closed) connection.Close();
        }
        private void kapcsolatNyit()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();

        }

        public List<Book> Books { get => books; set => books = value; }

        public void getAllBook()
        {
            command.CommandText = "SELECT * FROM `books`";
            try
            {
                kapcsolatNyit();
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //Book books = new Book(dr.GetInt32("id"), dr.GetString("title"), dr.GetString("author"), dr.GetInt32("publish_year"), dr.GetInt32("page_count"));
                        int id = dr.GetInt32("id");
                        string title = dr.GetString("title");
                        string author = dr.GetString("author");
                        int publish_year = dr.GetInt32("publish_year");
                        int page_count = dr.GetInt32("page_count");
                        books.Add(new Book(id, title, author, publish_year, page_count));
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            finally
            {
                kapcsolatZar();
            }
        }

        // Határozza meg az 500 oldalnál hosszabb könyvek számát
        public void CountBooksOver500Pages()
        {
            int db =0;
            foreach (Book b in books)
            {
                if (b.Page_count > 500)
                {
                    db++;
                }
            }
            Console.WriteLine($"500 oldalnál hosszabb könyvek száma: {db}");
        }

        // Döntse el, hogy szerepel-e az adatok között 1950-nél régebbi könyv
        public void CheckOlderThan1950()
        {
            int db = 0;
            foreach(Book b in books)
            {
                if(b.Publish_year < 1950)
                {
                    db++;
                }
            }
            if (db >= 1) {
                Console.WriteLine($"{db} darab 1950-nél régebbi könyv van a könyvtárban.");
            }
            else
            {
                Console.WriteLine("Az adatok között nincs 1950-nél régebbi könyv.");
            }
        }
        // Határozza meg és írja ki a leghosszabb könyv adatait
        public void FindLongestBook()
        {
            int longest = 0;
            foreach (Book b in books)
            {
                if (b.Page_count > longest)
                {
                    longest = b.Page_count;
                }
            }
            foreach (Book b in books)
            {
                if (b.Page_count == longest)
                {
                    Console.WriteLine($"A leghosszabb könyv:");
                    Console.WriteLine($"\tCím: {b.Title}");
                    Console.WriteLine($"\tSzerző: {b.Author}");
                    Console.WriteLine($"\tKiadás éve: {b.Publish_year}");
                    Console.WriteLine($"\tOldalszám: {b.Page_count}");
                }
            }
        }

        // Határozza meg és írja ki a legtöbb könyvvel rendelkező szerzőt
        public void FindMostActiveAuthor()
        {
            Dictionary<string, int> authors = new Dictionary<string, int>();
            foreach (Book b in books)
            {
                if (authors.ContainsKey(b.Author))
                {
                    authors[b.Author]++;
                }
                else
                {
                    authors.Add(b.Author, 1);
                }
            }
            int max = 0;
            string author = "Hiba történt!";
            foreach (KeyValuePair<string, int> pair in authors)
            {
                if(pair.Value > max)
                {
                    max = pair.Value;
                    author = pair.Key;
                }
            }
            Console.WriteLine($"A legtöbb könyvvel rendelkező szerző: {author}");
        }

        // Kérjen be a konzolról egy könyv címet és határozza meg az adott könyv szerzőjét
        public void FindAuthorByTitle()
        {
            Console.WriteLine("Kérlek add meg a keresett könyv címét:");
            string title = Console.ReadLine();
            foreach (Book b in books)
            {
                if(title == b.Title)
                {
                    Console.WriteLine($"A megadott könyv szerzője: {b.Author}");
                }
            }
        }
    }
}

