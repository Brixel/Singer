namespace Singer.DTOs;

public class AboutDTO
{
    public string ApiVersion { get; set; }

    public UserInfoDTO UserInfo { get; set; }
}

public class UserInfoDTO
{
    public bool IsAdmin { get; set; }
}
