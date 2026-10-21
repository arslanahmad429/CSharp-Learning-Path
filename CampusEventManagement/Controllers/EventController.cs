using System;
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
            List<Event> events = repo.GetAllEvents();

            ViewBag.Heading = "Welcome to Campus Events";
            ViewData["Message"] = "Explore Upcoming Campus Events";

            return View(events);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string title, string category, DateTime date, string venue)
        {
            Event newEvent = new Event();
            newEvent.Title = title;
            newEvent.Category = category;
            newEvent.Date = date;
            newEvent.Venue = venue;

            repo.AddEvent(newEvent);

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
            if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(studentEmail))
            {
                ViewBag.Error = "Please provide both your name and email address.";
                ViewBag.EventTitle = eventTitle;
                return View();
            }

            ViewBag.Confirmation = "Registration successful! Thank you " + studentName + ", you have been registered for " + eventTitle + ". We will send details to " + studentEmail + ".";
            return View();
        }
    }
}
