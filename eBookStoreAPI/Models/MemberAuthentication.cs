namespace eBookStoreAPI.Models
{
    public class UserAuthentication
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
