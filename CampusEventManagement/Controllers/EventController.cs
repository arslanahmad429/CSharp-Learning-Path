using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CampusEventManagement.Models;
using CampusEventManagement.Repositories;

namespace CampusEventManagement.Controllers
{
    public class EventController : Controller
    {
        EventRepository repo = new EventRepository();

        public IActionResult Index()
        {
            List<Event> list = repo.GetAllEvents();
            ViewBag.Heading = "Welcome to Campus Events";
            ViewData["Message"] = "Explore Upcoming Campus Events";
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string title, string category, DateTime date, string venue)
        {
            Event ev = new Event();
            ev.Title = title;
            ev.Category = category;
            ev.Date = date;
            ev.Venue = venue;
            repo.AddEvent(ev);
            return RedirectToAction("Index");
        }

        public IActionResult Register(int id, string title)
        {
            ViewBag.EventTitle = title;
            return View();
        }

        [HttpPost]
        public IActionResult Register(string eventTitle, string studentName, string studentEmail)
        {
            ViewBag.Confirmation = "Thanks " + studentName + ", you are registered for " + eventTitle + ". We will contact you at " + studentEmail + ".";
            return View();
        }
    }
}
