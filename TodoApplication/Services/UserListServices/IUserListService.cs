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
        //ICollection<UserModel> GetUsers();
        //UserModel GetUser(int nationalParkId);
        bool CreateUser(UserModel user);
        //bool UpdateNationalPark(NationalPark nationalPark);
        //bool DeleteNationalPark(NationalPark nationalPark);

        bool Save();
    }
}
