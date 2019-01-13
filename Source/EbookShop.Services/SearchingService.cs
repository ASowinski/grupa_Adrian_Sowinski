using EbookShop.DataAccess;
using EbookShop.Models;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EbookShop.Services
{
    public class SearchingService : IEbookSearchService
    {
        private EbookShopContext context;

        public SearchingService(EbookShopContext context)
        {
            this.context = context;
        }
        /// <summary>
        ///Zwraca tablicę ebooków, w których tytule/nazwie kategorii/nazwie autora zawiera się dana fraza.
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IEnumerable<Ebook> GetEbooksByTextInAnyProperty(string text)
        {
            List<Ebook> returnValue = new List<Ebook>();

            Ebook[] temp = GetEbooksByTitle(text).ToArray();
            returnValue.AddRange(temp);

            temp = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, text)
            ).ToArray();
            returnValue.AddRange(temp);

            temp = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, text)
            ).ToArray();
            returnValue.AddRange(temp);


            return returnValue;
        }


        public string GetEbooksByTextInAnyProperty_JSON(string text)
        {
            Ebook[] arr = GetEbooksByTextInAnyProperty(text).ToArray();
            return JsonConvert.SerializeObject(arr);
        }

        public HtmlString GetEbooksByTextInAnyProperty_HtmlString(string text)
        {
            Ebook[] arr = GetEbooksByTextInAnyProperty(text).ToArray();

            using (StringWriter stringWriter = new StringWriter())
            using (JsonTextWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                serializer.Serialize(jsonWriter, arr);

                return new HtmlString(stringWriter.ToString());
            }
        }
        /// <summary>
        ///Zwraca tablicę z ebookami, w których tytule zawiera się podana fraza
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IEnumerable<Ebook> GetEbooksByTitle(string title)
        {
            Ebook[] returnValue = context.Ebooks.Where(e => e.Title.Contains(title)).ToArray();

            return returnValue;
        }

        /// <summary>
        ///Zwraca tablicę ebooków danej kategorii
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<Ebook> GetEbooksByCategory(Category category)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, category)
            ).ToArray();

            return returnValue;
        }

        //Sprawdza czy ebook jest danej kategorii
        private bool DoesEbookContainCategory(Ebook e, Category category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category == category;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w nazwie kategorii ebooka
        private bool DoesEbookContainCategory(Ebook e, string category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category.CategoryName.Contains(category);

            return false;
        }
        ///<summary>
        ///Zwraca tablicę ebooków z danym autorem
        ///</summary>
        public IEnumerable<Ebook> GetEbooksByAuthor(Author author)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, author)
            ).ToArray();

            return returnValue;
        }
        ///<summary>
        ///Sprawdza czy dany autor jest autorem danego ebooka
        ///</summary>
        private bool DoesEbookContainAuthor(Ebook e, Author author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author == author;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w imieniu bądź nazwisku autora
        private bool DoesEbookContainAuthor(Ebook e, string author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author.FirstName.Contains(author) || ebookCategories.Author.LastName.Contains(author);

            return false;
        }
    }
}
