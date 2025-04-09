using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zadanie1_19.Models;
using System.Collections.Generic;
using System.Linq;

namespace zadanie1_19.Controllers
{
    public class StudyController : Controller
    {
        private static List<StudyTask> tasks = new List<StudyTask>();

        public IActionResult Upcoming()
        {
            var upcomingTasks = tasks.Where(t => !t.Completed).OrderBy(t => t.DeadLine).ToList();
            return View(upcomingTasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StudyTask studyTask)
        {
            if (ModelState.IsValid)
            {
                studyTask.Completed = false;
                tasks.Add(studyTask);
                return RedirectToAction(nameof(Upcoming));
            }
            return View(studyTask);
        }
    }
}
