using Core.Entities.Concrete;

namespace Business.Constants;

public class Messages
{
    public static string PostLiked = "Post liked";
    public static string PostUnLiked = "Post unliked";
    public static string CommentLiked = "Comment liked";
    public static string CommentUnLiked = "Comment unliked";
    public static string ConenctionIdRecorded = "Connection Id recorded successfully.";
    public static string UserExists = "User already exists on system.";
    public static string UserCreated = "Registration completed successfully.";
    public static string UserNotFound = "User not found.";
    public static string PasswordError = "Password is wrong.";
    public static string PasswordMatchError = "The entered passwords do not match. Please try again.";
    public static string NotExists = "Username or email is not exists.";
    public static string UserIsNotOnline = "User is not online.";
    public static string MessageNotSent = "Message did not sent.";
    public static object? NewConversation = "New chat started.";
}