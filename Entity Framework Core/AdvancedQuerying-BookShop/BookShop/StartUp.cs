namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            Console.WriteLine(GetMostRecentBooks(db));

        }

        // Problem 1. Age Restriction.
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            var titles = context
                .Books
                .Where(book => book.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .ToList()
                .OrderBy(title => title);

            var result = String.Join(Environment.NewLine, titles);

            return result;
        }

        // Problem 2. Golden Books.
        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();
            var editionType = Enum.Parse<EditionType>("Gold");
            var titles = context
                .Books
                .Where(book => book.EditionType == editionType)
                .Where(book => book.Copies < 5000)
                .Select(book => new { book.BookId, book.Title })
                .OrderBy(book => book.BookId)
                .ToList();

            foreach (var title in titles)
            {
                sb.AppendLine(title.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 3. Book by Prce.
        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();
            var titles = context.Books.Where(b => b.Price > 40).Select(b => new { b.Price, b.Title }).OrderByDescending(b => b.Price).ToList();
            foreach (var title in titles)
            {
                sb.AppendLine($"{title.Title} - ${title.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 4. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var bookTitles = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title).ToList();

            var result = String.Join(Environment.NewLine, bookTitles);

            return result;
        }

        // Problem 5. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();
            var bookTitles = context
                .BooksCategories
                .Where(category => categories.Contains(category.Category.Name.ToLower()))
                .Select(x => x.Book.Title)
                .OrderBy(x => x)
                .ToList();

            var result = String.Join(Environment.NewLine, bookTitles);

            return result;
        }

        // Problem 6. Released Before Date

        public static string GetBooksReleasedBefore(BookShopContext context, string input)
        {
            var sb = new StringBuilder();
            var books = context
                .Books
                .Where(book => book.ReleaseDate < DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                 .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.Price,
                    b.EditionType,
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 7. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();
            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                })
                .OrderBy(a => a.FullName)
                .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            input = input.ToLower();
            var bookTitles = context.Books.Where(book => book.Title.ToLower().Contains(input))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();


            var result = String.Join(Environment.NewLine, bookTitles);

            return result;
        }

        // Problem 9. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();
            input = input.ToLower();
            var titlesAuthors = context
                .Books
                .Where(book => book.Author.LastName.ToLower().StartsWith(input))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName,
                })
                .ToList();

            foreach (var titleAuthor in titlesAuthors)
            {
                sb.AppendLine($"{titleAuthor.Title} ({titleAuthor.FirstName + " " + titleAuthor.LastName})");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books.Where(book => book.Title.Length > lengthCheck).Count();
            return booksCount;
        }

        // Problem 11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();
            var authorsCopies = context.Authors.OrderByDescending(a => a.Books.Sum(b => b.Copies)).Select(ac => new
            {
                ac.FirstName,
                ac.LastName,
                Copies = ac.Books.Sum(b => b.Copies),
            })
                .ToList();

            foreach (var author in authorsCopies)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName} - {author.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        //  Problem 12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();
            var categoriesProfits = context.Categories.Select(c => new
            {
                c.Name,
                BookProfit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
            })
                 .ToList()
                 .OrderByDescending(x => x.BookProfit)
                 .ThenBy(x => x.Name);

            foreach (var category in categoriesProfits)
            {
                sb.AppendLine($"{category.Name} ${category.BookProfit:F2}");
            }

            return sb.ToString().TrimEnd();

        }

        // Problem 13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();
            var mostRecentBooksByCategory = context.Categories.OrderBy(c => c.Name).Select(c => new
            {
                c.Name,
                Books = c.CategoryBooks.OrderByDescending(b => b.Book.ReleaseDate).Select(b => new
                {
                    b.Book.Title,
                    Year = b.Book.ReleaseDate.Value.Year,
                })
                .Take(3)
                .OrderByDescending(x => x.Year)
                .ToList()
            });

            foreach (var category in mostRecentBooksByCategory)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }


        //Problem 14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var bookstoUpdate = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in bookstoUpdate)
            {
                book.Price += 5;
            }

            context.SaveChanges();

        }

        //Problem 15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();
            int count = books.Count();
            context.Books.RemoveRange(books);
            context.SaveChanges();

            return books.Count();
        }
    }
}

