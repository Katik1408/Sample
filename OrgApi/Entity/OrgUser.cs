using System;
using System.Collections.Generic;

#nullable disable

namespace OrgApi.Entity
{
    public partial class OrgUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
