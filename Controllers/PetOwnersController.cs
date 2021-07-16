using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller

        [HttpGet]
                public IEnumerable<PetOwner> GetPets()
        {

            // tell context that we're needing to go to table 
            // include here
            // return _context.Pet.Include(pet => pet.petOwner).OrderBy(pet => pet.id).ToList();
            var petOwner = _context.PetOwner.Include(pet => pet.pets).OrderBy(pet => pet.id).ToList();
            return petOwner;
            // return Ok(petOwner);
            //-???? // ya i think so
            // return new List<PetOwner>(); // Commented out to add code.
        }

        [HttpPost]
        public IActionResult Post([FromBody] PetOwner petOwner)
        {
            _context.Add(petOwner);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPets), petOwner);
        } 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PetOwner petOwner = _context.PetOwner.Find(id);
            if (petOwner == null) return NotFound();
            _context.PetOwner.Remove(petOwner);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
