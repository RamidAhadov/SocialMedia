using Core.Entities;

namespace DataAccess.Concrete.Dtos;

public class PostForAddDto:IDto
{
    public string PostText { get; set; }
    public string Token { get; set; }
}