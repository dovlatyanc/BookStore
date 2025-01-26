using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        private ApplicationContext context;
        private Book selectedBook;

        public Window1()
        {
            InitializeComponent();
            context = new ApplicationContext();
            LoadBooks();
        }

        // Загрузка книг из базы данных
        private void LoadBooks() => BooksListBox.ItemsSource = context.Books.ToList();

        // Выбор книги
        private void BooksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBook = BooksListBox.SelectedItem as Book;
            if (selectedBook != null)
            {
                TitleTextBox.Text = selectedBook.Title;
                AuthorTextBox.Text = selectedBook.Author;
                PublisherTextBox.Text = selectedBook.Publisher;
                PageCountTextBox.Text = selectedBook.PageCount.ToString();
                GenreTextBox.Text = selectedBook.Genre;
                YearTextBox.Text = selectedBook.Year.ToString();
                CostPriceTextBox.Text = selectedBook.CostPrice.ToString();
                SellingPriceTextBox.Text = selectedBook.SellingPrice.ToString();
                IsPartOfSeriesTextBox.Text = selectedBook.IsPartOfSeries.ToString();
                BrokenCountTextBox.Text = selectedBook.BrokenCount.ToString();
                SoldCountTextBox.Text = selectedBook.SoldCount.ToString();
                DiscountTextBox.Text = selectedBook.Discount.ToString();
                StockTextBox.Text = selectedBook.Stock.ToString();
                InfoSeriesTextBox.Text = selectedBook.SeriesInfo;
                ReleaseDateTextBox.Text = selectedBook.ReleaseDate.ToString();

            }
        }

        // Добавление книги
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text,
                    Author = AuthorTextBox.Text,
                    Publisher = PublisherTextBox.Text,
                    PageCount = int.Parse(PageCountTextBox.Text),
                    Genre = GenreTextBox.Text,
                    Year = int.Parse(YearTextBox.Text),
                    CostPrice = decimal.Parse(CostPriceTextBox.Text),
                    SellingPrice = decimal.Parse(SellingPriceTextBox.Text),
                    IsPartOfSeries = bool.Parse(IsPartOfSeriesTextBox.Text),
                    SeriesInfo = InfoSeriesTextBox.Text,
                    Stock = int.Parse(StockTextBox.Text),
                    SoldCount = int.Parse(SoldCountTextBox.Text),
                    Discount = decimal.Parse(DiscountTextBox.Text),
                    BrokenCount = int.Parse(BrokenCountTextBox.Text),
                    ReleaseDate = DateTime.Parse(ReleaseDateTextBox.Text)
                };

                context.Books.Add(book);
                Save();
                ClearInputs(); // Очистка полей после добавления
            }
            else
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }


        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                context.Books.Remove(selectedBook);
                Save();
            }
        }


        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null && ValidateInputs())
            {
                selectedBook.Title = TitleTextBox.Text;
                selectedBook.Author = AuthorTextBox.Text;
                selectedBook.Publisher = PublisherTextBox.Text;
                selectedBook.PageCount = int.Parse(PageCountTextBox.Text);
                selectedBook.Genre = GenreTextBox.Text;
                selectedBook.Year = int.Parse(YearTextBox.Text);
                selectedBook.CostPrice = decimal.Parse(CostPriceTextBox.Text);
                selectedBook.SellingPrice = decimal.Parse(SellingPriceTextBox.Text);
                selectedBook.IsPartOfSeries = bool.Parse(IsPartOfSeriesTextBox.Text);
                selectedBook.BrokenCount = int.Parse(BrokenCountTextBox.Text);
                selectedBook.SoldCount = int.Parse(SoldCountTextBox.Text);
                selectedBook.Discount = decimal.Parse(DiscountTextBox.Text);
                selectedBook.Stock = int.Parse(StockTextBox.Text);
                selectedBook.SeriesInfo = InfoSeriesTextBox.Text;
                selectedBook.ReleaseDate = DateTime.Parse(ReleaseDateTextBox.Text);

                Save();
            }
            else
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }


        private void Save()
        {
            try
            {
                context.SaveChanges();
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }


        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(TitleTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(AuthorTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(PublisherTextBox.Text) &&
                   int.TryParse(PageCountTextBox.Text, out _) &&
                   !string.IsNullOrWhiteSpace(GenreTextBox.Text) &&
                   int.TryParse(YearTextBox.Text, out _) &&
                   decimal.TryParse(CostPriceTextBox.Text, out _) &&
                   decimal.TryParse(SellingPriceTextBox.Text, out _) &&
                   bool.TryParse(IsPartOfSeriesTextBox.Text, out _) &&
                   int.TryParse(StockTextBox.Text, out _) &&
                   int.TryParse(SoldCountTextBox.Text, out _) &&
                   decimal.TryParse(DiscountTextBox.Text, out _) &&
                   DateTime.TryParse(ReleaseDateTextBox.Text, out _) &&
                   int.TryParse(BrokenCountTextBox.Text, out _);



        }


        private void ClearInputs()
        {
            TitleTextBox.Clear();
            AuthorTextBox.Clear();
            PublisherTextBox.Clear();
            PageCountTextBox.Clear();
            GenreTextBox.Clear();
            YearTextBox.Clear();
            CostPriceTextBox.Clear();
            SellingPriceTextBox.Clear();
            IsPartOfSeriesTextBox.Clear();
            BrokenCountTextBox.Clear();
            SoldCountTextBox.Clear();
            DiscountTextBox.Clear();
            StockTextBox.Clear();
            InfoSeriesTextBox.Clear();
        }

        private void SellBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                if (selectedBook.Stock > 0)
                {
                    selectedBook.Stock--; // Уменьшаем количество на складе
                    selectedBook.SoldCount++; // Увеличиваем количество проданных книг

                    Save();

                    MessageBox.Show($"Книга '{selectedBook.Title}'  успешно продана! " +
                        $"На складе осталось {selectedBook.Stock} экземпляров", "Информация");
                }
                else
                {
                    MessageBox.Show("Нет книг на складе для продажи!");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для продажи!");
            }
        }

        private void WriteOffBookButton_Click(object sender, RoutedEventArgs e)
        {

            if (selectedBook != null)
            {
                if (selectedBook.Stock > 0)
                {
                    selectedBook.Stock--;
                    selectedBook.BrokenCount++;
                    Save();
                    MessageBox.Show($"Книга '{selectedBook.Title}'  успешно списана! " +
                        $"На складе осталось {selectedBook.Stock} экземпляров", "Информация");
                }
                else
                {
                    MessageBox.Show("Нет книг на складе для списания!");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для списания!");
            }
        }

        private void ShowNewReleases_Click(object sender, RoutedEventArgs e)//список новинок
        {
            var newReleases = context.Books
         .Where(b => b.ReleaseDate >= DateTime.Now.AddDays(-30))
         .OrderByDescending(b => b.ReleaseDate)
         .Select(b => $"{b.Title} ({b.Author})")
         .ToList();

            ResultsListBox.ItemsSource = newReleases;
        }
        private void ShowBestSellers_Click(object sender, RoutedEventArgs e)//самые продаваемые книги
        {
            DateTime startDate = GetStartDate();

            var bestSellers = context.Books
                .Where(b => b.SoldCount > 0)
                .OrderByDescending(b => b.SoldCount)
                .Select(b => $"{b.Title} ({b.Author}) - Продано: {b.SoldCount}")
                .ToList();

            ResultsListBox.ItemsSource = bestSellers;
        }
        private void ShowPopularAuthors_Click(object sender, RoutedEventArgs e)//популярные авторы
        {
            DateTime startDate = GetStartDate();

            var popularAuthors = context.Books
                .GroupBy(b => b.Author)
                .Select(g => new
                {
                    Author = g.Key,
                    TotalSold = g.Sum(b => b.SoldCount)
                })
                .OrderByDescending(g => g.TotalSold)
                .Select(g => $"{g.Author} - Продано: {g.TotalSold}")
                .ToList();

            ResultsListBox.ItemsSource = popularAuthors;
        }
        private void ShowPopularGenres_Click(object sender, RoutedEventArgs e)//популярные жанры
        {
            DateTime startDate = GetStartDate();

            var popularGenres = context.Books
                .GroupBy(b => b.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    TotalSold = g.Sum(b => b.SoldCount)
                })
                .OrderByDescending(g => g.TotalSold)
                .Select(g => $"{g.Genre} - Продано: {g.TotalSold}")
                .ToList();

            ResultsListBox.ItemsSource = popularGenres;
        }
        private DateTime GetStartDate()
        {
            var selectedPeriod = (PeriodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            return selectedPeriod switch
            {
                "За день" => DateTime.Now.AddDays(-1),
                "За неделю" => DateTime.Now.AddDays(-7),
                "За месяц" => DateTime.Now.AddMonths(-1),
                "За год" => DateTime.Now.AddYears(-1),
                _ => DateTime.MinValue
            };
        }




        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text.ToLower(); // Получаем поисковый запрос

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                // Ищем книги по названию, автору и жанру
                var searchResults = context.Books
                    .Where(b => b.Title.ToLower().Contains(searchQuery) ||
                                b.Author.ToLower().Contains(searchQuery) ||
                                b.Genre.ToLower().Contains(searchQuery))
                    .ToList();

                // Отображаем результаты поиска
                if (searchResults.Count > 0)
                    SearchResultsListBox.ItemsSource = searchResults;
                else
                {
                    MessageBox.Show("Книги не найдены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите поисковый запрос!");
            }
        }

        private void AddDiscountBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                if (selectedBook.Stock > 0)
                {
                    decimal oldPrice = selectedBook.SellingPrice;

                    decimal discountPercentage = 10; //10 percent
                    decimal discountAmount = oldPrice * (discountPercentage / 100);
                    selectedBook.SellingPrice = oldPrice - discountAmount;
                    selectedBook.Discount = 0.1M;


                    // Убедимся, что цена не стала отрицательной
                    if (selectedBook.SellingPrice < 0)
                    {
                        selectedBook.SellingPrice = 0;
                    }

                    Save();
                    ClearInputs();
                    MessageBox.Show($"Книга '{selectedBook.Title}' успешно добавлена в акцию! Применена скидка 10 % \n" +
                             $"Старая цена: {oldPrice} рублей\n" +
                             $"Новая цена: {selectedBook.SellingPrice} рублей",
                             "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Нет книг на складе для добавления в акцию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для добавления в акцию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}