using System.ComponentModel.DataAnnotations;

namespace HttpMethodsLesson1.Models
{

    public class UserModel 
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
       
    }
    public class User : UserModel
    {
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        [Key]
        public string Id { get; set; }
    }

}
