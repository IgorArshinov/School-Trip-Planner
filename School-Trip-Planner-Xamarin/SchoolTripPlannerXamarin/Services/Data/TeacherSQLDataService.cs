using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SchoolTripPlannerXamarin.Services.Data
{
    public class TeacherSQLDataService : ITeacherSQLDataService
    {
        readonly SQLiteAsyncConnection _database;
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Teachers.db3");

        public TeacherSQLDataService()
        {
            _database = new SQLiteAsyncConnection(_dbPath);
            _database.CreateTableAsync<Teacher>().Wait();
        }

        public Task<List<Teacher>> GetTeachersAsync()
        {
            return _database.Table<Teacher>().ToListAsync();
        }

        public Task<Teacher> GetTeacherByUsernameAsync(string username)
        {
            return _database.Table<Teacher>()
                .Where(t => t.Username == username)
                .FirstOrDefaultAsync();
        }

        public Task<Teacher> GetTeacherByIdAsync(long id)
        {
            return _database.Table<Teacher>()
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<long> SaveTeacherAsync(Teacher teacher)
        {
            var teacherExists = _database.Table<Teacher>().ToListAsync().Result.Exists(t => t.Id == teacher.Id);
            if (teacherExists)
            {
                await _database.UpdateAsync(teacher);
                return teacher.Id;
            }
            else
            {
                await _database.InsertAsync(teacher);
                return teacher.Id;
            }
        }

        public Task<int> DeleteTeacherAsync(Teacher teacher)
        {
            return _database.DeleteAsync(teacher);
        }
    }
}