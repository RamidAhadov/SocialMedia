namespace Business.Constants;

public class Notifications
{
    public static string FriendRequestTemplate(string firstName,string lastName)
    {
        string notification = $"{firstName} {lastName} has sent you a friend request.";
        return notification;
    }

    public static string AcceptFriendRequestTemplate(string firstName,string lastName)
    {
        string notification = $"{firstName} {lastName} accepted your friend request.";
        return notification;
    }
    public static string LikePostTemplate(string firstName,string lastName)
    {
        string notification = $"{firstName} {lastName} has liked your post.";
        return notification;
    }
    public static string LikeCommentTemplate(string firstName,string lastName)
    {
        string notification = $"{firstName} {lastName} has liked your comment.";
        return notification;
    }

    public static string WriteCommentTemplate(string firstName,string lastName)
    {
        string notification = $"{firstName} {lastName} has commented on your post.";
        return notification;
    }
    
}