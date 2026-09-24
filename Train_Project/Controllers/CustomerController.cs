using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Train_Project.Data;
using Train_Project.DTOs.Customers;
using Train_Project.Filters;

namespace Train_Project.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [ServiceFilter(typeof(RequestTimingFilter))]
    [ServiceFilter(typeof(ModelValidationFilter))]
    [ServiceFilter(typeof(DateRangeFilter))]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyCustomer()
        {
            var userIdClaim = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var customer = await _context.Customers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (customer == null)
                return NotFound("Customer not found.");

            return Ok(new
            {
                customer.Id,
                customer.Name,
                customer.Location,
                customer.Email,
                customer.Phone,
                customer.UserId,
                Username = customer.User.Username
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var userIdClaim = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var customer = await _context.Customers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            if (customer.UserId != userId)
                return Forbid();

            return Ok(new
            {
                customer.Id,
                customer.Name,
                customer.Location,
                customer.Email,
                customer.Phone,
                customer.UserId,
                Username = customer.User.Username
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(
            int id,
            UpdateCustomerDto dto)
        {
            var userIdClaim = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var customer = await _context.Customers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            if (customer.UserId != userId)
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
                customer.Location,
                customer.Email,
                customer.Phone,
                customer.UserId,
                Username = customer.User.Username
            });
        }
    }
}