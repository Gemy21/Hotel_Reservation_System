using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train_Project.Data;
using Train_Project.DTOs.Customers;

namespace Train_Project.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Username,
                    x.Location,
                    x.Email,
                    x.Phone
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound();

            var currentUsername = User.Identity?.Name;

            if (customer.Username != currentUsername)
                return Forbid();

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            var currentUsername = User.Identity?.Name;

            if (customer.Username != currentUsername)
                return Forbid();

            customer.Name = dto.Name;
            customer.Location = dto.Location;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                customer.Id,
                customer.Name,
                customer.Username,
                customer.Location,
                customer.Email,
                customer.Phone
            });
        }
    }
}