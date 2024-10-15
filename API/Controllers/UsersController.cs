using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet] //This  method returns the entire list of users stored inside the DB
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            //Where we are doing here is making the method call Async. What is does is that once a request is received, the method forwards the fetching data functionality to another
            //thread and starts listining for more requests while the second thread goes to the DB and fetches the main data.
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [Authorize]
        [HttpGet("{id}")] //This method / endpoint returns the single user with the specified ID that is passed down to the method or endpoint. 'api/users/3'
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
            
        }
    }
}