namespace BirthdayApp.Api
{
    public static class ApiUrls
    {
        public static string OauthUrl => @"https://oauth.vk.com/authorize?client_id=" + AppCommon.AppInfo.VkAppId + "&display=page&scope=friends,photos,groups&response_type=token&v=5.102&state=123456";
    }
}
