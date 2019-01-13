using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Bogus;
using FluentAssertions;
using EbookShop.Models;
using EbookShop.Services;
using EbookShop.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;
namespace EbookShop.Tests.Unit.Services.EbookService
{
   public class EbookSearchServiceTests
    {
        private readonly ITestOutputHelper output;

        public EbookSearchServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task GetEbooksByTitle_ValidCount()
        {
            #region Arrange
            var options = new DbContextOptionsBuilder<EbookShopContext>()
              .UseInMemoryDatabase(databaseName: "inMemoryTestsDb")
              .Options;
            var books = new List<Ebook>();
            var ebooksFaked = new Faker<Ebook>()
                .RuleFor(o => o.Title, f => f.Lorem.Word())
                .Generate(100);

            var searchString = ebooksFaked[50].Title;
            int expectedCount = ebooksFaked.Where(x => x.Title.Contains(searchString)).Count();
            

            using (var context = new EbookShopContext(options))
            {
                foreach(var ebook in ebooksFaked)
                {
                    context.Ebooks.Add(ebook); 
                }
               await context.SaveChangesAsync(); 
            }
            #endregion

            // Act
            int resultCount = -1;
            using (var context = new EbookShopContext(options))
            {
                var service = new SearchingService(context);
                resultCount = service.GetEbooksByTitle(searchString).Count(); 
            }

            // Assert

            resultCount.Should().Be(expectedCount);
            output.WriteLine("Expected: {0}, Current: {1}", expectedCount, resultCount); 
        }
    }
}
