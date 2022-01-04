namespace ABS_SystemManager.Models
{
    public class AbsUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
