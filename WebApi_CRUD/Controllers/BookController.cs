using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi_CRUD.Models;

namespace WebApi_CRUD.Controllers
{
    public class BookController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg = null;
            Book book = new Book();
            using (var context = new WebAPIDbContext())
            {
                book = (from b in context.Books
                        where b.Id == id
                       select b).FirstOrDefault();
            }

            if (book == null)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book not found!");

            }
            else
            {
                msg = Request.CreateResponse<Book>(HttpStatusCode.OK, book);
            }
            return msg;
        }

        public List<Book> Get()
        {
            List<Book> books = new List<Book>();
            using (var context = new WebAPIDbContext())
            {
                books = (from b in context.Books
                         orderby b.Title ascending
                         select b).ToList();
            }
            return books;
        }

        public void Put(int id, [FromBody]Book book)
        {
            using (var context = new WebAPIDbContext())
            {
                Book book_2 = (from b in context.Books
                             where b.Id == id
                             select b).FirstOrDefault();
                book_2.Title = book.Title;
                book_2.Author = book.Author;

                context.SaveChanges();
            }
        }

        public HttpResponseMessage Post([FromBody]Book c)
        {
            HttpResponseMessage msg;
            Book book = new Book() { Author = c.Author, Title = c.Title, TypeOfBook = c.TypeOfBook };
            using (var context = new WebAPIDbContext())
            {

                context.Books.Add(book);
                context.SaveChanges();
            }
            msg = Request.CreateResponse(HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + book.Id.ToString());

            return msg;
        }

        public void Delete(int id)
        {
            using (var context = new WebAPIDbContext())
            {
                Book book = (from b in context.Books
                               where b.Id == id
                               select b).FirstOrDefault();
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
        }

    }
}
