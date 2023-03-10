namespace Utilities.Helpers;

public static class ImageHelper
{
    public static string GetAuthorImageSource(int? authorId)
    {
        return authorId != null 
            ? $"https://i.pravatar.cc/300?u={authorId}" 
            : "https://img.freepik.com/free-icon/spy_318-185018.jpg?t=st=1678437093~exp=1678437693~hmac=0fac2f3de838512ef61fc851149af70e1489f3e8113cda069757c9b03e294c3e";
    }
}