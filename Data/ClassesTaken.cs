﻿using System;
using System.Collections.Generic;

namespace SchoolManagementApp.MVC.Data;

public partial class ClassesTaken
{
    public int Id { get; set; }

    public int? LecturerId { get; set; }

    public int? CourseId { get; set; }

    public TimeSpan? Time { get; set; }

    public virtual Lecturer? Course { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Lecturer? Lecturer { get; set; }
}
