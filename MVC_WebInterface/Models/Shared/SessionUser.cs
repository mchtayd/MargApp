namespace MVC_WebInterface.Models.Shared
{
    public class SessionUser
    {
        public string SicilNo { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }

        public string GetUserInfo() => $"{SicilNo} - {Name}";
    }
}
