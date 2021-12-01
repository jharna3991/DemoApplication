using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Migrations;
using TodoApplication.Models;

namespace TodoApplication.Services.UserListServices
{
    public interface IUserListService
    {
        bool Authenticate(string username, string password);

        //ICollection<UserModel> GetUsers();
        UserModel GetUserById(int Id);
        bool CreateUser(UserModel user, string password);
        bool UserNameExists(string name);
        //bool UpdateNationalPark(NationalPark nationalPark);
        //bool DeleteNationalPark(NationalPark nationalPark);
    }
}
