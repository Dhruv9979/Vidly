using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Runtime.Caching;

//To give authorization to all the actions in cutomers page we need to use[Authorize] globally.
//We can do this in App_Start/FilterConfig.

/*4 ways to increase performance in the application tier:
 * 1. Output Caching
 * 2. Data Caching
 * 3. Release Builds: We can apply this without performance tests.
 * 4. Disabling Session: We can apply this without performance tests.
 */
/*Ways to increase performance in the client tier:
* 1. Dto should be light weight. Use bundles for loading JS and CSS. Render them at the end of body. 
*       So markup can load first for the user.
* 2. Data Caching
* 3. Release Builds: We can apply this without performance tests.
* 4. Disabling Session: We can apply this without performance tests.
*/
namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
        //Anti Forgery tokens are used to avoid Cross-Site Request Forgery
        //To avoid this we need to follow 2 steps.
        /*
         * 1. Use @Html.AntiForgeryToken in CustomerForm to create a token which is like a secret code,
         *      and then put it as a hidden field in this form and also as a cookie in the user's computer.
         * 2. Use [ValidateAntiForgeryToken] before the save method.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //ModelState is used to get access to the data
            //IsValid is Used to change the application flow
            /* 3 steps to add server-side validation
             * 1. Add annotation in the model class
             * 2. Use ModelState and ModeState.IsValid and return View("Form", viewModel) in Controller Class
             * 3. Add Validation messages to the form
             * For Client-side validation go to CustomerForm.cshtml @section.
             */
            
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                //Using auto Mapper
                //Mapper.Map(customer, customerInDb);
                //We don't use this because of security issues.
                //But in the case of updating only a specific values instead of all of them.
                //You can create a new class UpdateCustomerDto and use as the parameter here.
                //Dto stands for Data Transfer Object.
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        /*Data Caching
        *  Use this after performance testing, as we can get duplicate data in the database or exceptions thrown
        *  by ASP.NET Entity Framework. As a work around you have to attach those object sto DbConext, that 
        *  may also cause other issues. 
        *  Also limit this to displaying data not modifying it.
        */
        public ViewResult Index()
        {
            //if(MemoryCache.Default["Genres"] == null)
            //{
            //    MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            //var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}