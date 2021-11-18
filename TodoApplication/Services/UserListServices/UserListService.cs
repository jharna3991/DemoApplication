using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Helpers;
using TodoApplication.Migrations;
using TodoApplication.Models;

namespace TodoApplication.Services.UserListServices
{
    public class UserListService : IUserListService
    {   
        private readonly ApplicationDbContext _db;
        public UserListService(ApplicationDbContext db)
        {
            _db = db;
        }


        public bool CreateUser(UserModel user)
        {
            //validation
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new AppException("Password is required");

            if (_db.UserList.Any(x => x.UserName == user.UserName))
                throw new AppException("Username \" " + user.UserName + "\" is already taken");


            byte[] passwordHash, passwordSalt;
            string pword = CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.Password = pword;

            _db.UserList.Add(user);
            return Save();

        }


        private static string CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                string pwordSalt = System.Text.Encoding.Default.GetString(passwordSalt);
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwordSalt));
                string pwordHash = System.Text.Encoding.Default.GetString(passwordHash);
                return pwordHash;
            }
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
