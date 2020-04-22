using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    //You can also use API for camel Notation. Go to App_Start/WebApiConfig. Use config.Formatters.JsonFormatter
    //API should never recirve or return domain objects. In this case the Customer Objects.
    /* By using DTO's we can simply exclude the properties that not be changed. This helps in keeping the
    application secure from hackers.*/
    //We use AutoMapper to map Customer objects to Dtos. So open Package Manager Console from tools.
    /*1. Create a mapping profile which determines how objects of different types can be mapped to each other. 
     * So, in App_Start add a new class called MappingProfile.
     */
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customers, this convention is build in ASP.NET API.
        //This is for all customers
        //Use Include here to include MembershipType in Datatables.
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }
        //GET /api/customers/1, this convention is build in ASP.NET API.
        //This is for single customers
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //POST /api/customers
        [HttpPost] //apply this expilicitly, otherwise if you use PostCustomer instead of CreateCustomer.
        //It may break the code, if you consider to rename the method.
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //New id for new Customer
            customerDto.Id = customer.Id;
            //We need to return URI(unified return indentifier)
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        //PUT /api/customers/1 (PUT HTTP request is for updating the customers)
        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);
            /*I didn't use 2 param before because, we already have an existing object(CustomerInDb) so we can pass it.
            It is also loaded into our _context, so DbContext can track changes into our object.*/
            //Delete /api/customers/1
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
