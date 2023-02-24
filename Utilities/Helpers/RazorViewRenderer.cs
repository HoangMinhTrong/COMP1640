using System.Web.Mvc;

namespace Utilities.Helpers
{
    public static class RazorViewRenderer
    {
        public static string RenderToString(string viewPath, object model)
        {
            try
            {
                var viewEngine = new RazorViewEngine();
                var viewData = new ViewDataDictionary(model);

                // Find the Razor view file by its path
                var viewResult = viewEngine.FindView(null, viewPath, "", false);

                // Render the view into a string
                using (var stringWriter = new StringWriter())
                {
                    var view = viewResult.View;
                    var viewContext = new ViewContext(null, view, viewData, new TempDataDictionary(), stringWriter);
                    view.Render(viewContext, stringWriter);
                    return stringWriter.ToString();
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
