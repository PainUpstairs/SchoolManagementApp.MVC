using System;
using System.Collections.Generic;

namespace SchoolManagementApp.MVC.Data;

public partial class Lecturer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<ClassesTaken> ClassesTakenCourses { get; set; } = new List<ClassesTaken>();

    public virtual ICollection<ClassesTaken> ClassesTakenLecturers { get; set; } = new List<ClassesTaken>();
}
