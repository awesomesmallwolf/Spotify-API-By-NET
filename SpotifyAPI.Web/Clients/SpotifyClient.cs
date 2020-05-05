using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyAPI.Web.Http;

namespace SpotifyAPI.Web
{
  public class SpotifyClient : ISpotifyClient
  {
    private readonly IAPIConnector _apiConnector;

    public SpotifyClient(string token, string tokenType = "Bearer") :
      this(SpotifyClientConfig.CreateDefault(token, tokenType))
    { }

    public SpotifyClient(SpotifyClientConfig config)
    {
      Ensure.ArgumentNotNull(config, nameof(config));

      _apiConnector = config.CreateAPIConnector();
      DefaultPaginator = config.Paginator;
      UserProfile = new UserProfileClient(_apiConnector);
      Browse = new BrowseClient(_apiConnector);
      Shows = new ShowsClient(_apiConnector);
      Playlists = new PlaylistsClient(_apiConnector);
      Search = new SearchClient(_apiConnector);
      Follow = new FollowClient(_apiConnector);
      Tracks = new TracksClient(_apiConnector);
    }

    public IPaginator DefaultPaginator { get; }

    public IUserProfileClient UserProfile { get; }

    public IBrowseClient Browse { get; }

    public IShowsClient Shows { get; }

    public IPlaylistsClient Playlists { get; }

    public ISearchClient Search { get; }

    public IFollowClient Follow { get; }

    public ITracksClient Tracks { get; }

    public Task<List<T>> Paginate<T>(Paging<T> firstPage)
    {
      return DefaultPaginator.Paginate(firstPage, _apiConnector);
    }

    public Task<List<T>> Paginate<T, TNext>(Paging<T, TNext> firstPage, Func<TNext, Paging<T, TNext>> mapper)
    {
      return DefaultPaginator.Paginate(firstPage, mapper, _apiConnector);
    }

    public Task<List<T>> Paginate<T>(Paging<T> firstPage, IPaginator paginator)
    {
      Ensure.ArgumentNotNull(paginator, nameof(paginator));

      return paginator.Paginate(firstPage, _apiConnector);
    }

    public Task<List<T>> Paginate<T>(Func<Task<Paging<T>>> getFirstPage)
    {
      return DefaultPaginator.Paginate(getFirstPage, _apiConnector);
    }

    public Task<List<T>> Paginate<T, TNext>(Func<Task<Paging<T, TNext>>> getFirstPage, Func<TNext, Paging<T, TNext>> mapper)
    {
      return DefaultPaginator.Paginate(getFirstPage, mapper, _apiConnector);
    }

    public Task<List<T>> Paginate<T>(Func<Task<Paging<T>>> getFirstPage, IPaginator paginator)
    {
      Ensure.ArgumentNotNull(paginator, nameof(paginator));

      return paginator.Paginate(getFirstPage, _apiConnector);
    }
  }
}
