namespace COMP1640.ViewModels.Idea.Responses
{
	public class IdeaContentResponse
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int DepartmentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

		//public virtual User CreatedByNavigation { get; set; }
		//public virtual Department Department { get; set; }
		//public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
	}
}
