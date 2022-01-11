using System.Text.Json;
using System;

namespace SpotifyCloneAPI.API.Helpers;

public partial class AppSettings
{
    public SpotifySettings SpotifySettings { get; set; }
    public AppSettings()
    {
        SpotifySettings = new SpotifySettings();
    }
}
public partial class SpotifySettings
{
    public string BaseUrl { get; set; }
    public string LoginUrl { get; set; }
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string Scopes { get; set; }
    public string RedirectUri { get; set; }

    public SpotifySettings()
    {
        BaseUrl = String.Empty;
        LoginUrl = String.Empty;
        ClientID = String.Empty;
        ClientSecret = String.Empty;
        Scopes = String.Empty;
        RedirectUri = String.Empty;
    }
}

public partial class ApplicationWebSettings
{
    public string BaseUrl { get; set; }
    public ApplicationWebSettings()
    {
        BaseUrl = String.Empty;
    }
}

public partial class ApplicationAPISettings
{
    public string BaseUrl { get; set; }
    public ApplicationAPISettings()
    {
        BaseUrl = String.Empty;
    }
}
