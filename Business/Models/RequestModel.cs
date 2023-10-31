using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Models;

public class RequestModel
{
    public int UserId { get; set; }
    public int SenderId { get; set; }
    public int RequestId { get; set; }
    public string RequestContent { get; set; }
    public string ProfilePhoto { get; set; }
    public string RequestDate { get; set; }
}