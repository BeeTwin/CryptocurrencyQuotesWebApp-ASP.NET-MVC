using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using CryptocurrencyQuotesLibrary.Data.Contexts;
using CryptocurrencyQuotesWebApp.Models;
using CryptocurrencyQuotesLibrary.Utils;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using CryptocurrencyQuotesLibrary.Models.DbModels;
using CryptocurrencyQuotesLibrary.Utils.Extentions;

namespace CryptocurrencyQuotesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CryptocurrencyDbContext _dbContext;

        public HomeController(CryptocurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Flags]
        public enum Sorting
        {
            Name = 1,
            Price = 2,
            PercentChange1h = 4,
            PercentChange24h = 8,
            MarketCap = 16,
            Descending = 32
        }


        private const int PAGE_SIZE = 7;
        private const int DECIMALS = 2;
        [Authorize]
        public async Task<IActionResult> CryptocurrencyQuotes(
            string filter,
            string searchString,
            int pageNumber = 1,
            Sorting sortOrder = Sorting.Name)

        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSorting"] = sortOrder == Sorting.Name
                                                            ? Sorting.Name | Sorting.Descending
                                                            : Sorting.Name;
            ViewData["PriceSorting"] = sortOrder == Sorting.Price
                                                            ? Sorting.Price | Sorting.Descending
                                                            : Sorting.Price;
            ViewData["PercentChange1hSorting"] = sortOrder == Sorting.PercentChange1h
                                                            ? Sorting.PercentChange1h | Sorting.Descending
                                                            : Sorting.PercentChange1h;
            ViewData["PercentChange24hSorting"] = sortOrder == Sorting.PercentChange24h
                                                            ? Sorting.PercentChange24h | Sorting.Descending
                                                            : Sorting.PercentChange24h;
            ViewData["MarketCapSorting"] = sortOrder == Sorting.MarketCap
                                                            ? Sorting.MarketCap | Sorting.Descending
                                                            : Sorting.MarketCap;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = filter;
            }

            ViewData["Filter"] = searchString;

            var crypts = await _dbContext.Cryptocurrency.ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                crypts = crypts.Where(x =>
                        x.Name.Contains(searchString) ||
                        x.Symbol.Contains(searchString))
                    .ToList();
            }


            var viewList = new List<CryptocurrencyViewModel>();
            crypts = await SortContext(crypts, sortOrder);
            foreach (var crypto in crypts)
            {
                viewList.Add(new CryptocurrencyViewModel
                {
                    Name =             crypto.Name,
                    Symbol =           crypto.Symbol,
                    Logo =             ImageUrl.GetImageUrl(crypto.Id),
                    Price =            DecimalToViewString(crypto.Price, false),
                    PercentChange1h =  DecimalToViewString(crypto.PercentChange1h),
                    PercentChange24h = DecimalToViewString(crypto.PercentChange24h),
                    MarketCap =        DecimalToViewString(crypto.Price, false),
                    LastUpdated =      crypto.LastUpdated
                });
            }

            var paginatedList = new PaginatedList<CryptocurrencyViewModel>(viewList, pageNumber, PAGE_SIZE);

            return View(paginatedList);
        }

        private async Task<List<Cryptocurrency>> SortContext(IEnumerable<Cryptocurrency> list, Sorting sorting)
        {
            return await Task.Run(() =>
            {
                var isDescending = false;
                if ((isDescending = sorting.HasFlag(Sorting.Descending)))
                {
                    sorting -= Sorting.Descending;
                }

                list = sorting switch
                {
                    Sorting.Name =>             list.OrderBy(isDescending, x => x.Name),
                    Sorting.Price =>            list.OrderBy(isDescending, x => x.Price),
                    Sorting.PercentChange1h =>  list.OrderBy(isDescending, x => x.PercentChange1h),
                    Sorting.PercentChange24h => list.OrderBy(isDescending, x => x.PercentChange24h),
                    Sorting.MarketCap =>        list.OrderBy(isDescending, x => x.MarketCap),
                    _ =>                        throw new NotImplementedException()
                };

                return list.ToList();
            });
        }

        private string DecimalToViewString(decimal dec, bool showSign = true)
        {
            var result = "";
            if (showSign && dec > 0)
            {
                result = "+";
            }
            result += Math.Round(dec, DECIMALS);
            return result;
        }

        /*
        private string PreparePercentChange(decimal percentChange, decimal price)
        {
            var oldPrice = 100 * price / (100 + percentChange);

            var difference = price - oldPrice;

            return DecimalToViewString(difference, true);
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
