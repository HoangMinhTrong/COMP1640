namespace COMP1640.ViewModels.Department.Responses
{
    public class InforDepartmentResponse
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string QaCoordinatorName { get; set; }
        public int TenantId { get; set; }
        public bool IsDelete { get; set; }
    }
}
