namespace API.Entities
{
    public class AppUser
    {
        public AppUser()
        {
        }

        public AppUser(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }

    }
}