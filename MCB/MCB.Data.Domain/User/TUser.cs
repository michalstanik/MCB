using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCB.Data.Domain.User
{
    public class TUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public List<UserCountry> UserCountries { get; set; }
    }
}
