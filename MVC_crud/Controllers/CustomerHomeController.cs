using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_crud.Data;
using MVC_crud.Models.Entities;

namespace MVC_crud.Controllers
{
    public class CustomerHomeController : Controller
    {
        private readonly MVCDBContext _mvcDBContext;
        public CustomerHomeController(MVCDBContext mvcDBContext)
        {
            this._mvcDBContext = mvcDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> ListCust()
        {
            var customers = await _mvcDBContext.Customers.ToListAsync();
            return View(customers);
        }
        [HttpGet]
        public  async Task<IActionResult> EditCust(string custID)
        {
            var customers = await _mvcDBContext.Customers.FindAsync(custID);
            return View(customers);
        }
        [HttpPost]
        public async Task<IActionResult> EditCust(Customer viewModel)
        {
            var customers = await _mvcDBContext.Customers.FindAsync(viewModel.CustomerID);
            if(customers is not null)
            {
                customers.CompanyName = viewModel.CompanyName;
                customers.ContactName = viewModel.ContactName;
                customers.ContactTitle = viewModel.ContactTitle;
                customers.Address = viewModel.Address;
                customers.City = viewModel.City;
                customers.Region = viewModel.Region;
                customers.PostalCode = viewModel.PostalCode;
                customers.Country = viewModel.Country;
                customers.Phone = viewModel.Phone;
                customers.Fax = viewModel.Fax;

                await _mvcDBContext.SaveChangesAsync();
            }

            return RedirectToAction("ListCust", "CustomerHome");
        }
        [HttpPost]
        public async Task<IActionResult> DelCust(Customer viewModel)
        {
            var customers = await _mvcDBContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerID == viewModel.CustomerID);
            if(customers is not null)
            {
                _mvcDBContext.Customers.Remove(viewModel);
                await _mvcDBContext.SaveChangesAsync();
            }
            return RedirectToAction("ListCust", "CustomerHome");
        }
        [HttpPost]
        public async Task<IActionResult> AddCust(Customer viewModel)
        {
            var customers = new Customer
            {
                CustomerID = viewModel.CustomerID,
                CompanyName = viewModel.CompanyName,
                ContactName = viewModel.ContactName,
                ContactTitle = viewModel.ContactTitle,
                Address = viewModel.Address,
                City = viewModel.City,
                Region = viewModel.Region,
                PostalCode = viewModel.PostalCode,
                Country = viewModel.Country,
                Phone = viewModel.Phone,
                Fax = viewModel.Fax,
            };
            await _mvcDBContext.Customers.AddAsync(customers);
            await _mvcDBContext.SaveChangesAsync();
            return RedirectToAction("ListCust", "CustomerHome");
        }
    }
}
