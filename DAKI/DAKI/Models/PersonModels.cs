using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAKI.Models
{

    [Table("Person")]
        public class Person
        {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int PersonId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public Nullable<System.DateTime> BirthDate { get; set; }
            public string Adress { get; set; }

            public virtual ICollection<PersonHasJobInDep> PersonHasJobInDeps { get; set; }
            public virtual ICollection<UserProfile> UserProfiles { get; set; }
        }

    [Table("Department")]
        public class Department
        {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int DepartmentId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Nullable<int> ParentId { get; set; }
            public Nullable<bool> Rules { get; set; }

            public virtual ICollection<Department> Children { get; set; }
            public virtual Department Parent { get; set; }
            public virtual ICollection<PersonHasJobInDep> PersonHasJobInDeps { get; set; }
        }

    [Table("Job")]
        public class Job
        {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int JobId { get; set; }
            public string Title { get; set; }
            public bool Manages { get; set; }

            public virtual ICollection<PersonHasJobInDep> PersonHasJobInDeps { get; set; }
        }

    [Table("PersonHasJobInDep")]
    public class PersonHasJobInDep
    {
        [Key, Column(Order = 0)]
        public int PersonId { get; set; }
        [Key, Column(Order = 1)]
        public int JobId { get; set; }
        [Key, Column(Order = 2)]
        public int DepartmentId { get; set; }
        public System.DateTime StartingDate { get; set; }
        public Nullable<int> Salary { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Job Job { get; set; }
        public virtual Person Person { get; set; }
    }


    
}