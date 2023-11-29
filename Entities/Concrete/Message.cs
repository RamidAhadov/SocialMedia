using Core.Entities;

namespace Entities.Concrete;

public class Message:IEntity
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string MessageText { get; set; }
    public DateTime Date { get; set; }
}