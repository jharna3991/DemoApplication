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


        public bool CreateUser(UserModel user, string password)
        {
            //validation
            if (_db.UserList is null)
            {
                if(UserNameExists(user.UserName))
                    throw new AppException("Username \" " + user.UserName + "\" is already taken");
            }

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

           
            byte[] passwordHash, passwordSalt;
            CreatePassword(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _db.UserList.Add(user);
            Console.WriteLine("I am here");
            _db.SaveChanges();
            return true;

        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public bool UserNameExists(string name)
        {
            
            bool value = _db.UserList.Any(a => a.UserName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool Authenticate(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            var user = _db.UserList.SingleOrDefault(x => x.UserName == username);

            //check if username exists
            if (user == null)
                throw new AppException("Username is required");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AppException("Password does not match");

            //authentication successful
            return true;

        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
