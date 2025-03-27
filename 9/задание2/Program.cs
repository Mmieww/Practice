public class Student
{
    public string Name { get; set; }
    public int Score { get; set; }

    public Student(string name, int score)
    {
        Name = name;
        Score = score;
    }
}

public class StudentFileWriter
{
    private readonly string _filePath;

    public StudentFileWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteSortedStudents(List<Student> students)
    {
        var sortedStudents = students.OrderByDescending(s => s.Score).ToList();

        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            foreach (var student in sortedStudents)
            {
                writer.WriteLine($"{student.Name};{student.Score}");
            }
        }

        Console.WriteLine($"Студенты записаны в файл '{_filePath}' в отсортированном порядке.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Student> students = new List<Student>
        {
            new Student("Kate", 9),
            new Student("Ann", 10),
            new Student("Max", 5),
            new Student("Ksenia", 1)
        };

        string filePath = "file.data"; //указать путь к файлу

        StudentFileWriter studentFileWriter = new StudentFileWriter(filePath);

        studentFileWriter.WriteSortedStudents(students);
    }
}