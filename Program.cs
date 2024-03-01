using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace KonyvtarAsztaliKonzolos
{
    internal class Program
    {
        public static List<Book> book = new List<Book>();

        static void Main(string[] args)
        {
            Statisztika stat = new Statisztika();
            try
            {
                stat.getAllBook();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Environment.Exit(1);
            }
            stat.CountBooksOver500Pages();
            stat.CheckOlderThan1950();
            stat.FindLongestBook();
            stat.FindMostActiveAuthor();
            stat.FindAuthorByTitle();
            Console.ReadKey();
        }
    }
}
