using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace zadanie1_19.Models
{
    public class StudyTaskService : IStudyTaskService
    {
       private static List<StudyTask> tasks = new List<StudyTask>();

        public void AddTask(StudyTask task)
        {
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
        }

        public List<StudyTask> GetUpcomingTasks()
        {
            return tasks.Where(t => !t.Completed).OrderBy(t => t.DeadLine).ToList();
        }

        public StudyTask GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTask(StudyTask task)
        {
            var existingTask = GetTaskById(task.Id);
            if (existingTask != null)
            {
                existingTask.Subject = task.Subject;
                existingTask.DeadLine = task.DeadLine;
                existingTask.Completed = task.Completed;
                existingTask.Priority = task.Priority;
            }
        }

        public void DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                tasks.Remove(task);
            }
        }

        public void MarkAsDone(int id)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                task.IsDone = true;
            }
        }
    }
}
