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
        [HttpPost]
        public IHttpActionResult DeleteRental(int id, RentalDto newRental)
        {
            var rentedMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            rentedMovie.NumberAvailable++;

            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            if (customer.Id == newRental.CustomerId)
            {
                var rental = new Rental
                {
                    DateReturned = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}