namespace COMP1640.ViewModels.Idea.Responses
{
	public class CategoryForCreateIdeaResponse
	{
		public IList<DropDownListBaseResponse>? Categories { get; set; }
	}

	public class DropDownListBaseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
