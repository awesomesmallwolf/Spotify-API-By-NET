using System.Collections.Generic;

namespace SpotifyAPI.Web
{
  public class FullArtist
  {
    public Dictionary<string, string> ExternalUrls { get; private set; }
    public Followers Followers { get; private set; }
    public List<string> Genres { get; private set; }
    public string Href { get; private set; }
    public string Id { get; private set; }
    public List<Image> Images { get; private set; }
    public string Name { get; private set; }
    public int Popularity { get; private set; }
    public string Type { get; private set; }
    public string Uri { get; private set; }
  }
}
