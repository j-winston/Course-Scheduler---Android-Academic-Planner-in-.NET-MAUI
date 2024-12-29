using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;

using c971.Models;
using c971.Models.c971.Models;
using SQLite;

namespace c971.Data
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string connectionString)
        {
            _database = new SQLiteAsyncConnection(connectionString);

            InitializeDatabase().Wait();
        }

        private async Task InitializeDatabase()
        {
            await _database.CreateTableAsync<Term>();
            await _database.CreateTableAsync<Course>();
            await _database.CreateTableAsync<Assessment>();
        }


        public Task<int> AddTerm(Term term)
        {
            return _database.InsertAsync(term);
        }

        public Task<List<Term>> GetAllTerms()
        {
            return _database.Table<Term>().ToListAsync();
        }

        public Task<int> DeleteTerm(Term term)
        {
            DeleteCoursesByTermId(term.Id);

            return _database.DeleteAsync(term);
        }

        public Task<int> SaveTerm(Term term)
        {
            return _database.UpdateAsync(term);
        }

        public Task<int> AddCourse(Course course)
        {
            return _database.InsertAsync(course);
        }

        public Task<int> SaveCourse(Course course)
        {
            return _database.UpdateAsync(course);
        }


        public Task<List<Course>> GetCoursesByTermId(int termId)
        {
            return _database.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
        }


        public void DeleteCoursesByTermId(int termId)
        {
            _database.Table<Course>().Where(c => c.TermId == termId).DeleteAsync();
        }

        public async Task<int> DeleteCourse(Course course)
        {
            return await _database.DeleteAsync(course);
        }

        public Task<Term> GetTermById(int termId)
        {
            return _database.Table<Term>().Where(t => t.Id == termId).FirstOrDefaultAsync();
        }

        public Task<List<Assessment>> GetAssessmentsByCourseId(int courseId)
        {
            return _database.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
        }

        public Task<int> AddAssessment(Assessment assessment)
        {
            return _database.InsertAsync(assessment);
        }

        public Task<int> SaveAssessment(Assessment assessment)
        {
            return _database.UpdateAsync(assessment);
        }

        public async Task<int> DeleteAssessment(Assessment assessment)
        {
            return await _database.DeleteAsync(assessment);
        }

        public async Task SeedDataAsync()
        {
            try
            {
                // Clear existing data 
               await RemoveTestDataAsync();

                // Create test data
                var testTerm = new Term
                {
                    IsTestData = true,

                    Title = "Summer",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3)
                };
                await _database.InsertAsync(testTerm);

                var testCourse = new Course
                {
                    IsTestData = true,

                    Title = "Javascript I",
                    TermId = testTerm.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    InstructorName = "Anika Patel",
                    InstructorPhone = "555-123-4567",
                    InstructorEmail = "anika.patel@strimeuniversity.edu",
                    StartDateNotification = false,
                    Notes = "These are example notes which can be shared",
                    SelectedStatus = "In progress"
                };
                await _database.InsertAsync(testCourse);

                var testAssessment1 = new Assessment
                {
                    IsTestData = true,

                    Title = "Javascript I OA",
                    CourseId = testCourse.Id,
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(15),
                    StartDateNotification = false,
                    EndDateNotification = false,
                    SelectedType = "Objective Assessment",
                };
                await _database.InsertAsync(testAssessment1);

                var testAssessment2 = new Assessment
                {
                    IsTestData = true,

                    Title = "Javascript PA",
                    CourseId = testCourse.Id,
                    StartDate = DateTime.Now.AddDays(30),
                    StartDateNotification = false,
                    EndDateNotification = false,
                    EndDate = DateTime.Now.AddDays(30),
                    SelectedType = "Performance Assessment",
                };


                await _database.InsertAsync(testAssessment2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding test data:{ex.Message}");
            }
        }

        private async Task RemoveTestDataAsync()
        {
            try
            {
                // Remove test terms

                var testTerms = await _database.Table<Term>().Where(t => t.IsTestData).ToListAsync();
                foreach (var term in testTerms)
                {
                    await _database.DeleteAsync(term);
                }

                // Remove test courses

                var testCourses = await _database.Table<Course>().Where(c => c.IsTestData).ToListAsync();
                foreach (var course in testCourses)
                {
                    await _database.DeleteAsync(course);
                }

                // Remove test assessments 

                var testAssessments = await _database.Table<Assessment>().Where(t => t.IsTestData).ToListAsync();
                foreach (var assessment in testAssessments)
                {
                    await _database.DeleteAsync(assessment);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error removing test data: {ex.Message}");
            }
        }
    }
}