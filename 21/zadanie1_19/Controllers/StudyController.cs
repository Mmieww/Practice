using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zadanie1_19.Models;
using System.Collections.Generic;
using System.Linq;

namespace zadanie1_19.Controllers
{
    public class StudyController : Controller
    {
        private readonly IStudyTaskService _studyTaskService;

        public StudyController(IStudyTaskService studyTaskService)
        {
            _studyTaskService = studyTaskService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _studyTaskService.GetUpcomingTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudyTask task)
        {
            if (ModelState.IsValid)
            {
                _studyTaskService.AddTask(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _studyTaskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudyTask task)
        {
            if (ModelState.IsValid)
            {
                _studyTaskService.UpdateTask(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsDone(int id)
        {
            _studyTaskService.MarkAsDone(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _studyTaskService.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
