using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SoundSenseiContext _context;

        public UserController(SoundSenseiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            // Clear existing lists before adding new ones
            _context.HasProducts.RemoveRange(_context.HasProducts.Where(hp => hp.UserId == id));
            _context.WantProducts.RemoveRange(_context.WantProducts.Where(wp => wp.UserId == id));

            // Add new HasProducts to the database
            foreach (var hasProduct in user.HasProducts)
            {
                _context.HasProducts.Add(new HasProduct { UserId = id, ProductId = hasProduct.Id });
            }

            // Add new WantProducts to the database
            foreach (var wantProduct in user.WantProducts)
            {
                _context.WantProducts.Add(new WantProduct { UserId = id, ProductId = wantProduct.Id });
            }

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            // Remove user's HasProducts and WantProducts from the database
            _context.HasProducts.RemoveRange(_context.HasProducts.Where(hp => hp.UserId == id));
            _context.WantProducts.RemoveRange(_context.WantProducts.Where(wp => wp.UserId == id));

            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}
