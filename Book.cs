using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztaliKonzolos
{
    internal class Book
    {
        int id;
        string title;
        string author;
        int publish_year;
        int page_count;
        public int Id { get { return id; } set { id = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Author { get { return author; } set { author = value; } }
        public int Publish_year { get { return publish_year; } set { publish_year = value; } }
        public int Page_count { get { return page_count; } set { page_count = value; } }

        public Book(int id, string title, string author, int publish_year, int page_count)
        {
            Id = id;
            Title = title;
            Author = author;
            Publish_year = publish_year;
            Page_count = page_count;
        }
        public Book()
        { }

        public override string ToString()
        {
            return $"{this.id} {this.title}{this.author}{this.publish_year}{this.page_count}";
        }
    }
}
