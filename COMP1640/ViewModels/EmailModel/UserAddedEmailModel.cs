using Domain;

namespace COMP1640.ViewModels.EmailModel
{
    public class UserAddedEmailModel
    {
        public UserAddedEmailModel(User user, string apiRoute)
        {
            User = user;
            APIRoute = apiRoute;
        }

        public User User { get; set; }
        public string APIRoute { get; set; }
    }
}
