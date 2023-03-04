namespace Utilities.EmailService.Interfaces
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
