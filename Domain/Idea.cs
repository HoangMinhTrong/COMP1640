﻿using Domain.Base;

namespace Domain
{
    public class Idea : TenantAuditEntity<int>
    {
        public Idea()
        {

        }

        public Idea(string title, string content, bool isAnonymous, int categoryId, int academicYearId, int departmentId)
        {
            Title = title;
            Content = content;
            IsAnonymous = isAnonymous;
            CategoryId = categoryId;
            AcademicYearId = academicYearId;
            DepartmentId = departmentId;    
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Department Department { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();

        public void EditInfo(string title
            , string content
            , bool isAnonymous
            , int categoryId)
        {
            Title = title;
            Content = content;
            IsAnonymous = isAnonymous;
            CategoryId = categoryId;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
