namespace AuthServer.Core.DTO_s
{
    public class ClientTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
