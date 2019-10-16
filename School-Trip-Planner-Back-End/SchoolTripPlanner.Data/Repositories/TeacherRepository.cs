using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Domain.DTOs;
using SchoolTripPlanner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolTripPlanner.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ISchoolTripPlannerContext _context;
        private IMapper _mapper;

        public TeacherRepository(ISchoolTripPlannerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Teacher Authenticate(TeacherDTO teacherDTOFromJson)
        {
            if (string.IsNullOrEmpty(teacherDTOFromJson.Username) || string.IsNullOrEmpty(teacherDTOFromJson.Password))
                return null;

            var teacher = _context.Teachers.Where(x => x.Username == teacherDTOFromJson.Username);

            return !VerifyPasswordHash(teacherDTOFromJson.Password, teacher.FirstOrDefault()?.PasswordHash, teacher.FirstOrDefault()?.PasswordSalt) ? null : teacher.FirstOrDefault();
        }

        public List<TeacherDTO> GetAll()
        {
            return _context.Teachers.ProjectTo<TeacherDTO>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public TeacherDTO GetById(long id)
        {
            var teacher = _context.Teachers.SingleOrDefault(t => t.Id == id);
            var teacherDTO = _mapper.Map<TeacherDTO>(teacher);

            return teacherDTO;
        }

        public long Add(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);

            if (string.IsNullOrWhiteSpace(teacherDTO.Password))
                throw new Exception("Password is required.");

            if (_context.Teachers.Any(t => t.Username.Equals(teacher.Username.ToLower())))
                throw new Exception("This teacher's username is already taken.");

            CreatePasswordHash(teacherDTO.Password, out var passwordHash, out var passwordSalt);

            teacher.PasswordHash = passwordHash;
            teacher.PasswordSalt = passwordSalt;

            _context.Teachers.Add(teacher);

            return teacher.Id;
        }

        public void Update(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);

            if (string.IsNullOrWhiteSpace(teacherDTO.Password))
                throw new Exception("Password is required.");

            if (_context.Teachers.Any(t => t.Username.Equals(teacher.Username.ToLower()) && t.Id != teacher.Id))
                throw new Exception("This teacher's username is already taken.");

            CreatePasswordHash(teacherDTO.Password, out var passwordHash, out var passwordSalt);

            teacher.PasswordHash = passwordHash;
            teacher.PasswordSalt = passwordSalt;

            _context.Teachers.Update(teacher);
        }

        public void Delete(long id)
        {
            var teacher = _context.Teachers.SingleOrDefault(t => t.Id == id);

            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
        }

        public bool TeacherExists(long id)
        {
            return _context.Teachers.Any(t => t.Id == id);
        }

        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Count != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}