using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsPartOfSeries { get; set; }
        public string SeriesInfo { get; set; }
        public int Stock { get; set; }
        public int SoldCount { get; set; }
        public decimal Discount { get; set; }
        public int BrokenCount { get; set; }
        public DateTime ReleaseDate { get;  set; }
    }
}
