using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopForms
{
    public class Book
    {
        public string title;
        public string genre;
        public int pages;

        public string author;
        public string country;

        public int year;

        public Book() { }

        public Book(string title, string genre, int pages, string author, string country, int year)
        {
            this.title = title;
            this.genre = genre;
            this.pages = pages;
            this.author = author;
            this.country = country;
            this.year = year;
        }
    }
}