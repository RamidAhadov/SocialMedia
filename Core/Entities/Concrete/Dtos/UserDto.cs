namespace Core.Entities.Concrete.Dtos;

public class UserDto:IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfilePhoto { get; set; }
    public string CreatedDate { get; set; }
}