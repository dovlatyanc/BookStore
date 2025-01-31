using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserCredentialId {  get; set; }//внешний ключ
      

    }
    public class UserCredential
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        List<UserProfile> Users { get; set; }
    }
}
