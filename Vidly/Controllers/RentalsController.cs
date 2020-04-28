using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Rentals
        //Use this to return a view to the client.
        public ActionResult New()
        {
            return View();
        }

        public ViewResult Index()
        {
            return View();
        }

        //public ActionResult Edit(int id)
        //{
        //    var rental = _context.Rentals.SingleOrDefault(r => r.Id == id);

        //    if (rental == null)
        //        return HttpNotFound();

        //    var viewModel = new RentalFormViewModel
        //    {
        //        Rental = rental,
        //        Movie = _context.Movies.ToList(),
        //        Customer = _context.Customers.ToList()
        //    };

        //    return View("Index");
        //}
    }
}