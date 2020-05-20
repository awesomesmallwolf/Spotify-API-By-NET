using System.Collections.Generic;

namespace SpotifyAPI.Web
{
  public class LibraryCheckAlbumsRequest : RequestParams
  {
    public LibraryCheckAlbumsRequest(IList<string> ids)
    {
      Ensure.ArgumentNotNull(ids, nameof(ids));

      Ids = ids;
    }


    [QueryParam("ids")]
    public IList<string> Ids { get; }
  }
}
