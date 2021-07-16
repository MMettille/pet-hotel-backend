using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets()
        {
            return _context.Pet.Include(pet => pet.petOwner).OrderBy(pet => pet.id).ToList();
            // return new List<Pet>();
        }


        [HttpPost]
        public IActionResult Post([FromBody] Pet pet)
        {
            PetOwner owner = _context.PetOwner
            .SingleOrDefault(owner => owner.id == pet.petOwnerid);

            if(owner == null)
            {
                ModelState.AddModelError("petOwnerId", "Invalid Pet Owner ID");
                return ValidationProblem(ModelState);
            }
            _context.Add(pet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPets), pet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pet pet = _context.Pet.Find(id);
            if (pet == null) return NotFound();
            _context.Pet.Remove(pet);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("/api/pets/{id}/checkin")]
        public IActionResult CheckIn(int id)
        {
            Pet petToUpdate = _context.Pet.Find(id);
            if (petToUpdate == null) return NotFound();
            petToUpdate.checkedIn();
            _context.Update(petToUpdate);
            _context.SaveChanges();
            return Ok(petToUpdate);
        }

        [HttpPut("/api/pets/{id}/checkout")]
        public IActionResult CheckOut(int id)
        {
            Pet petToUpdate = _context.Pet.Find(id);
            if (petToUpdate == null) return NotFound();
            petToUpdate.checkedOut();
            _context.Update(petToUpdate);
            _context.SaveChanges();
            return Ok(petToUpdate);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pet newPet)
        {
            if (id != newPet.id) return BadRequest();
            if(!_context.Pet.Any(pet => pet.id == id)) return NotFound();
            _context.Update(newPet);
            _context.SaveChanges();
            return Ok(newPet);
        }
    }
}
