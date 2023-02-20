namespace COMP1640.ViewModels.HRM.Responses;

public class SelectPropertyForCreateAccountResponse : DropDownListBaseResponse
{
    public IList<DropDownListBaseResponse>? Roles { get; set; }
    public IList<DropDownListBaseResponse>? Departments { get; set; }

}

public class DropDownListBaseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
