using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepo<Book> BookRepository;

        private readonly IBookStoreRepo<Author> AuthorRepository;
        private IWebHostEnvironment hosting;
        public BookController(IBookStoreRepo<Book> bookRepo ,
            IBookStoreRepo<Author> authorRepo,
            IWebHostEnvironment hosting)
        {
            BookRepository = bookRepo;
            AuthorRepository = authorRepo;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = BookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = BookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try
            {
                String filename = String.Empty;
                if(model.File != null)
                {
                    String upload = Path.Combine(hosting.WebRootPath, "uploads");
                    filename = model.File.FileName;
                    String FullName = Path.Combine(upload , filename);
                    model.File.CopyTo(new FileStream(FullName, FileMode.Create));
                }

                if(model.AuthorId == -1)
                {
                    ViewBag.Messege = "Please Select An Author !!";
                    var viewmodel = new BookAuthorViewModel
                    {
                        Authors = FillSelectList()
                    };
                    return View(viewmodel);
                }
                Book book = new Book()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = AuthorRepository.Find(model.AuthorId),
                    ImgURL = filename
                };
                BookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = BookRepository.Find(id);
            var autherId = book.Author == null ? book.Author.AuthorId = 0 : book.Author.AuthorId;

            var viewMode = new BookAuthorViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId =  book.Author.AuthorId,
                Authors = AuthorRepository.List().ToList(),
                ImgURL = book.ImgURL
            };
            return View(viewMode);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel viewModel)
        {
            try
            {
                String filename = String.Empty;
                if (viewModel.File != null)
                {
                    String upload = Path.Combine(hosting.WebRootPath, "uploads");
                    filename = viewModel.File.FileName;
                    String FullName = Path.Combine(upload, filename);

                    //Old File Name
                    String oldFileName = BookRepository.Find(viewModel.BookId).ImgURL;
                    String FullOldName = Path.Combine(upload, oldFileName);

                    if(FullName != FullOldName)
                    {
                        //Delete Old Photo
                        if(oldFileName != "default.jpg")
                            System.IO.File.Delete(FullOldName);
                        //Save New Photo
                        viewModel.File.CopyTo(new FileStream(FullName, FileMode.Create));
                    }

                }

                var book = new Book()
                {
                    BookId= viewModel.BookId,
                    Title = viewModel.Title,
                    Description=viewModel.Description,
                    Author = AuthorRepository.Find(viewModel.AuthorId),
                    ImgURL = filename
                };
                BookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = BookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                BookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = AuthorRepository.List().ToList();
            authors.Insert(0, new Author { AuthorId = -1, FullName = "--- Please Select An Author ---" });
            return authors;
        }
    }
}
