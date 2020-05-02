using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace SpotifyAPI.Web
{
  public abstract class RequestParams
  {
    public JObject BuildBodyParams()
    {
      // Make sure everything is okay before building query params
      CustomEnsure();

      var bodyProps = GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
        .Where(prop => prop.GetCustomAttributes(typeof(BodyParamAttribute), true).Length > 0);

      var obj = new JObject();
      foreach (var prop in bodyProps)
      {
        var attribute = prop.GetCustomAttribute(typeof(BodyParamAttribute)) as BodyParamAttribute;
        object value = prop.GetValue(this);
        if (value != null)
        {
          obj[attribute.Key ?? prop.Name] = JToken.FromObject(value);
        }
      }
      return obj;
    }

    public Dictionary<string, string> BuildQueryParams()
    {
      // Make sure everything is okay before building query params
      CustomEnsure();

      var queryProps = GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
        .Where(prop => prop.GetCustomAttributes(typeof(QueryParamAttribute), true).Length > 0);

      var queryParams = new Dictionary<string, string>();
      foreach (var prop in queryProps)
      {
        var attribute = prop.GetCustomAttribute(typeof(QueryParamAttribute)) as QueryParamAttribute;
        object value = prop.GetValue(this);
        if (value != null)
        {
          if (value is List<string>)
          {
            List<string> list = value as List<string>;
            var str = string.Join(",", list);
            queryParams.Add(attribute.Key ?? prop.Name, str);
          }
          else
          {
            queryParams.Add(attribute.Key ?? prop.Name, value.ToString());
          }
        }
      }

      AddCustomQueryParams(queryParams);

      return queryParams;
    }

    protected virtual void CustomEnsure() { }
    protected virtual void AddCustomQueryParams(Dictionary<string, string> queryParams) { }
  }

  public class QueryParamAttribute : Attribute
  {
    public string Key { get; }

    public QueryParamAttribute() { }
    public QueryParamAttribute(string key)
    {
      Key = key;
    }
  }

  public class BodyParamAttribute : Attribute
  {
    public string Key { get; }

    public BodyParamAttribute() { }
    public BodyParamAttribute(string key)
    {
      Key = key;
    }
  }
}
