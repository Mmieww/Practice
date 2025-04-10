using System.Collections.Generic;
using System.Linq;

namespace zadanie1_19.Models
{
    public interface IStudyTaskService
    {
            void AddTask(StudyTask task);
            List<StudyTask> GetUpcomingTasks();
            StudyTask GetTaskById(int id);
            void UpdateTask(StudyTask task);
            void DeleteTask(int id);
            void MarkAsDone(int id);
    }
}
