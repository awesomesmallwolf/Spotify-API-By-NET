namespace SpotifyAPI.Web
{
    public class NewReleasesRequest : RequestParams
    {
        [QueryParam("country")]
        public string Country { get; set; }

        [QueryParam("limit")]
        public int? Limit { get; set; }

        [QueryParam("offset")]
        public int? Offset { get; set; }
    }
}
