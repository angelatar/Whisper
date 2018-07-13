namespace AuthServer.UserRep
{
    public class User
    {
        public int Id { set; get; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { set; get; }

    }
}
