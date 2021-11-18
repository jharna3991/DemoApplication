using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
