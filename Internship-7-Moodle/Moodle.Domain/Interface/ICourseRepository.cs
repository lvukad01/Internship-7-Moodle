using Moodle.Domain.Entities;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id);
    Task<List<Course>> GetByProfessorIdAsync(int professorId);
    Task<List<Course>> GetByStudentIdAsync(int studentId);
    Task AddAsync(Course course);
    Task SaveChangesAsync();
}
