namespace Octapull.Application.Constants
{
    public static class ApiUrls
    {
        public const string MeetingApiRequestBaseUrl = "https://localhost:7289/api/Meeting";
        public static string GetMeetingApiRequestUrlWithId(string id)
        {
            return $"{MeetingApiRequestBaseUrl}/{id}";
        }
    }
}
