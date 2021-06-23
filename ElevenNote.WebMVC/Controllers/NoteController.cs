using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var model = new NoteListItem[0];
            return View(model);
        }
        // Add method here VVVV
        // GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Below method makes sure modelstate is valid, grabs the current userId, calls on CreateNote, and returns the user back to the index view
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            // Used Edit-Refactor-Extract_Method to create helper method that does the same as lines 36-37
            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                /*ViewBag.SaveResult*/ 
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

            private NoteService CreateNoteService()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new NoteService(userId);
                return service;
            }
        }
    }