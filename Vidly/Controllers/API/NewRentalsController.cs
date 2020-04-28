using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetRentals(string query = null)
        {
            var rentalsQuery = _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie);

            //if (!String.IsNullOrWhiteSpace(query))
            //    rentalsQuery = rentalsQuery.Where(r => r.Id.Contains(query));

            var rentalDtos = rentalsQuery
                .ToList()
                .Select(Mapper.Map<Rental, RentalDto>);

            return Ok(rentalDtos);
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(RentalDto newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public IHttpActionResult ReturnRental(int id)
        {
            var rentals = _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .Where(r => r.Id == id);

            var movies = rentals.Select(m => m.Movie).ToList();

            if (rentals == null)
                return NotFound();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable >= movie.NumberInStock)
                        return BadRequest("Movie has already been returned");

                    var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.NumberAvailable++;
            }

            foreach (var rental in rentals)
            {
                var rentalInDb = _context.Rentals.Single(r => r.Id == rental.Id);
                rentalInDb.DateReturned = DateTime.Now;
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}