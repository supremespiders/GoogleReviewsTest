using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.MyBusinessAccountManagement.v1;
using Google.Apis.Services;
using Newtonsoft.Json;

try
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "client_secret.json");
    Debug.WriteLine("Reading credentials from " + path);
    var contents = File.ReadAllText(path);
    var c = JsonConvert.DeserializeObject<WebCredentials>(contents);

    var clientId = c.installed.client_id;
    var clientSecret = c.installed.client_secret;
    Console.WriteLine("Obtained client id " + clientId + " and secret " + clientSecret);
    var scopes = new string[] { "https://www.googleapis.com/auth/plus.business.manage" };

    var userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
        },
        scopes,
        "user",
        CancellationToken.None).Result;

    var service = new MyBusinessAccountManagementService(new BaseClientService.Initializer() { HttpClientInitializer = userCredential });

    var accountsListResponse = service.Accounts.List().Execute();
    Console.WriteLine("Completed");
    Console.WriteLine(JsonConvert.SerializeObject(accountsListResponse));
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
Console.ReadLine();

public class WebCredentials
{
    public Installed installed { get; set; }

public class Installed
{
    public string client_id { get; set; }
    public string project_id { get; set; }
    public string auth_uri { get; set; }
    public string token_uri { get; set; }
    public string auth_provider_x509_cert_url { get; set; }
    public string client_secret { get; set; }
    public string[] redirect_uris { get; set; }
}



}