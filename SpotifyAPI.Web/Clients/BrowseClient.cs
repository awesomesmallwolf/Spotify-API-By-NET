using System;
using System.Threading.Tasks;
using SpotifyAPI.Web.Http;
using SpotifyAPI.Web.Models;
using URLs = SpotifyAPI.Web.SpotifyUrls;

namespace SpotifyAPI.Web
{
  public class BrowseClient : APIClient, IBrowseClient
  {
    public BrowseClient(IAPIConnector apiConnector) : base(apiConnector) { }

    public Task<CategoriesResponse> GetCategories()
    {
      return API.Get<CategoriesResponse>(URLs.Categories());
    }

    public Task<CategoriesResponse> GetCategories(CategoriesRequest request)
    {
      Ensure.ArgumentNotNull(request, nameof(request));

      return API.Get<CategoriesResponse>(URLs.Categories(), request.BuildQueryParams());
    }

    public Task<Category> GetCategory(string categoryId)
    {
      Ensure.ArgumentNotNullOrEmptyString(categoryId, nameof(categoryId));

      return API.Get<Category>(URLs.Category(categoryId));
    }

    public Task<Category> GetCategory(string categoryId, CategoryRequest request)
    {
      Ensure.ArgumentNotNullOrEmptyString(categoryId, nameof(categoryId));
      Ensure.ArgumentNotNull(request, nameof(request));

      return API.Get<Category>(URLs.Category(categoryId), request.BuildQueryParams());
    }

    public Task<CategoryPlaylistsResponse> GetCategoryPlaylists(string categoryId)
    {
      Ensure.ArgumentNotNullOrEmptyString(categoryId, nameof(categoryId));

      return API.Get<CategoryPlaylistsResponse>(URLs.CategoryPlaylists(categoryId));
    }

    public Task<CategoryPlaylistsResponse> GetCategoryPlaylists(string categoryId, CategoriesPlaylistsRequest request)
    {
      Ensure.ArgumentNotNullOrEmptyString(categoryId, nameof(categoryId));
      Ensure.ArgumentNotNull(request, nameof(request));

      return API.Get<CategoryPlaylistsResponse>(URLs.CategoryPlaylists(categoryId), request.BuildQueryParams());
    }
  }
}
