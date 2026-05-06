using Microsoft.AspNetCore.Mvc;
using LogiTech.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogiTech.Controllers
{
    // MVC route — serves the view
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    // API route — serves the data
    [ApiController]
    [Route("api/driver")]
    public class DriverApiController : ControllerBase
    {
        private static List<Driver> drivers = new List<Driver>
        {
            new Driver { Id = "D-4231", Name = "Mike Chen",     OnTimeRate = 98, Rating = 4.9, Deliveries = 156, FuelEfficiency = 8.7, EfficiencyScore = 96 },
            new Driver { Id = "D-3892", Name = "Sarah Lee",     OnTimeRate = 96, Rating = 4.8, Deliveries = 142, FuelEfficiency = 8.4, EfficiencyScore = 91 },
            new Driver { Id = "D-4156", Name = "David Kim",     OnTimeRate = 94, Rating = 4.7, Deliveries = 138, FuelEfficiency = 8.1, EfficiencyScore = 87 },
            new Driver { Id = "D-4523", Name = "James Wilson",  OnTimeRate = 92, Rating = 4.6, Deliveries = 131, FuelEfficiency = 7.9, EfficiencyScore = 82 },
            new Driver { Id = "D-3987", Name = "Lisa Wang",     OnTimeRate = 89, Rating = 4.5, Deliveries = 124, FuelEfficiency = 7.6, EfficiencyScore = 74 }
        };

        [HttpGet]
        public IActionResult GetAllDrivers()
        {
            return Ok(new { success = true, count = drivers.Count, drivers });
        }

        [HttpGet("leaderboard")]
        public IActionResult GetLeaderboard()
        {
            var top = drivers.OrderByDescending(d => d.EfficiencyScore).Take(3).ToList();
            return Ok(new { success = true, leaderboard = top });
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            return Ok(new
            {
                success = true,
                summary = new
                {
                    total_drivers = drivers.Count,
                    average_on_time_rate = drivers.Average(d => d.OnTimeRate),
                    average_rating = drivers.Average(d => d.Rating),
                    average_deliveries = drivers.Average(d => d.Deliveries),
                    average_fuel_efficiency = drivers.Average(d => d.FuelEfficiency)
                }
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetDriverById(string id)
        {
            var driver = drivers.FirstOrDefault(d => d.Id == id);
            if (driver == null)
                return NotFound(new { success = false, message = "Driver not found" });
            return Ok(new { success = true, driver });
        }
    }
}