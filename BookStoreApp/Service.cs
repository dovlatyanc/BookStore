using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStoreApp
{
    internal class Service
    {
    }
   
public partial class Service : ObservableObject
    {
        private readonly ApplicationContext context;

        public Service(AppDbContext context)
        {
            this.context = context;
            LoadBooks();
        }

        [ObservableProperty]
        private ObservableCollection<Book> books;

        [ObservableProperty]
        private Book selectedBook;

        [ObservableProperty]
        private ObservableCollection<Book> searchResults;

        [ObservableProperty]
        private string searchQuery;

        // Поиск книг по названию, автору и жанру
        [RelayCommand]
        private void SearchBooks()
        {
            var query = context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = query.Where(b => b.Title.Contains(SearchQuery) ||
                                         b.Author.Contains(SearchQuery) ||
                                         b.Genre.Contains(SearchQuery));
            }

            SearchResults = new ObservableCollection<Book>(query.ToList());
        }

        // Добавление книги
        [RelayCommand]
        private void AddBook(string title, string author, string publisher, int pageCount, string genre, int year, decimal costPrice, decimal sellingPrice, bool isPartOfSeries, string seriesInfo, int stock)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                Publisher = publisher,
                PageCount = pageCount,
                Genre = genre,
                Year = year,
                CostPrice = costPrice,
                SellingPrice = sellingPrice,
                IsPartOfSeries = isPartOfSeries,
                SeriesInfo = seriesInfo,
                Stock = stock,
                ReleaseDate = DateTime.Now
            };
            context.Books.Add(book);
            context.SaveChanges();
            LoadBooks();
        }

        // Удаление книги
        [RelayCommand]
        private void DeleteBook()
        {
            if (SelectedBook != null)
            {
                context.Books.Remove(SelectedBook);
                context.SaveChanges();
                LoadBooks();
            }
        }

        // Редактирование книги
        [RelayCommand]
        private void EditBook(string title, string author, string publisher, int pageCount, string genre, int year, decimal costPrice, decimal sellingPrice, bool isPartOfSeries, string seriesInfo, int stock)
        {
            if (SelectedBook != null)
            {
                SelectedBook.Title = title;
                SelectedBook.Author = author;
                SelectedBook.Publisher = publisher;
                SelectedBook.PageCount = pageCount;
                SelectedBook.Genre = genre;
                SelectedBook.Year = year;
                SelectedBook.CostPrice = costPrice;
                SelectedBook.SellingPrice = sellingPrice;
                SelectedBook.IsPartOfSeries = isPartOfSeries;
                SelectedBook.SeriesInfo = seriesInfo;
                SelectedBook.Stock = stock;
                context.SaveChanges();
                LoadBooks();
            }
        }

        // Продажа книги
        [RelayCommand]
        private void SellBook(int quantity)
        {
            if (SelectedBook != null && SelectedBook.Stock >= quantity)
            {
                SelectedBook.Stock -= quantity;
                SelectedBook.SoldCount += quantity;

                var sale = new Sale
                {
                    BookId = SelectedBook.Id,
                    SaleDate = DateTime.Now,
                    Quantity = quantity
                };
                context.Sales.Add(sale);

                context.SaveChanges();
                LoadBooks();
            }
        }

        // Загрузка книг
        private void LoadBooks()
        {
            Books = new ObservableCollection<Book>(context.Books.ToList());
        }
    }
}
