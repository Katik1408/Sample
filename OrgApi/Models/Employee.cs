using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OrgApi.Models
{
    public partial class Employee
    {
        public int Empid { get; set; }

        [Column("Name")]
        public string EmployeeName { get; set; }
        public int? Age { get; set; }
        public string DateOfJoining { get; set; }
        public string Place { get; set; }
        public int? DeptId { get; set; }
    }
}
