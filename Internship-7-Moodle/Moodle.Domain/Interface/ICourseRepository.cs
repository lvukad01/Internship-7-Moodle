using Moodle.Domain.Entities;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int courseId);
    Task<List<Course>> GetByProfessorIdAsync(int professorId);
    Task<List<Course>> GetByStudentIdAsync(int studentId); 
}
