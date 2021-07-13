using System;
using System.Collections.Generic;

#nullable disable

namespace OrgApi.Entity
{
    public partial class Employee
    {
        public int Empid { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string DateOfJoining { get; set; }
        public string Place { get; set; }
        public int? DeptId { get; set; }
    }
}
