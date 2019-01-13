using EbookShop.Models;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EbookShop.Services
{
    public interface IEbookSearchService
    {
       IEnumerable<Ebook> GetEbooksByTextInAnyProperty(string text);
       IEnumerable<Ebook> GetEbooksByTitle(string title);
       IEnumerable<Ebook> GetEbooksByCategory(Category category);
       IEnumerable<Ebook> GetEbooksByAuthor(Author author);
       HtmlString GetEbooksByTextInAnyProperty_HtmlString(string text);
        string GetEbooksByTextInAnyProperty_JSON(string text);
    }
}
