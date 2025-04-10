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

        public IActionResult Upcoming()
        {
            var upcomingTasks = _studyTaskService.GetUpcomingTasks();
            return View(upcomingTasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new StudyTaskViewModel());
        }

        [HttpPost]
        public IActionResult Add(StudyTaskViewModel studyTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var studyTask = new StudyTask
                {
                    Subject = studyTaskViewModel.Subject,
                    DeadLine = studyTaskViewModel.DeadLine,
                    Completed = false 
                };
                _studyTaskService.AddTask(studyTask);

                TempData["SuccessMessage"] = "Task added successfully.";
                return RedirectToAction(nameof(Upcoming));
            }
            return View(studyTaskViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _studyTaskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new StudyTaskViewModel
            {
                Subject = task.Subject,
                DeadLine = task.DeadLine,
                Priority = task.Priority 
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudyTaskViewModel studyTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new StudyTask
                {
                    Id = _studyTaskService.GetTaskById(studyTaskViewModel.Id).Id,
                    Subject = studyTaskViewModel.Subject,
                    DeadLine = studyTaskViewModel.DeadLine,
                    Completed = false
                };
                _studyTaskService.UpdateTask(task);

                TempData["SuccessMessage"] = "Task updated successfully.";
                return RedirectToAction(nameof(Upcoming));
            }
            return View(studyTaskViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _studyTaskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _studyTaskService.DeleteTask(id);
            TempData["SuccessMessage"] = "Task deleted successfully.";
            return RedirectToAction(nameof(Upcoming));
        }
    }
}
