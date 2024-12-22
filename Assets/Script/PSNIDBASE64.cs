using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using System;
using Newtonsoft.Json; //require Json.NET
using System.Threading.Tasks;

public static class PSNIDBASE64
{
    //RemotePlay Windows Client
    static readonly string CLIENT_ID = "ba495a24-818c-472b-b12d-ff231c1b5745";
    static readonly string CLIENT_SECRET = "mvaiZkRsAsI1IBkY";
    static readonly string TOKEN_URL = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token";
    //Open the following LOGIN_URL in your Browser and log in
    public static string LOGIN_URL = $"https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorize?service_entity=urn:service-entity:psn&response_type=code&client_id={CLIENT_ID}&redirect_uri=https://remoteplay.dl.playstation.net/remoteplay/redirect&scope=psn:clientapp&request_locale=en_US&ui=pr&service_logo=ps&layout_type=popup&smcid=remoteplay&prompt=always&PlatformPrivacyWs1=minimal&";
    //After logging in, when the page shows "redirect", Use url    
    public static async Task<string> GetBase64ID(string redirect_url)
    {
        Uri redirect;
        try { redirect = new Uri(redirect_url);}
        catch (Exception){ return "uri invalid"; }  
        
        if (!redirect.Query.Contains("code"))
            return "url did not contain code parameter";

        UnityEngine.Debug.Log("Requesting OAuth Token..");
        
        var code = HttpUtility.ParseQueryString(redirect.Query).Get("code");                
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CLIENT_ID}:{CLIENT_SECRET}")));
        var body = $"grant_type=authorization_code&code={code}&redirect_uri=https://remoteplay.dl.playstation.net/remoteplay/redirect&";
        var token_request = await client.PostAsync(TOKEN_URL, new StringContent(body, Encoding.ASCII, "application/x-www-form-urlencoded"));
        
        if (token_request.StatusCode != HttpStatusCode.OK)
            return $"Request failed with code {token_request.StatusCode} : {token_request.Content.ReadAsStringAsync().Result}";

        var tokeninfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(token_request.Content.ReadAsStringAsync().Result);

        if (!tokeninfo.ContainsKey("access_token"))
            return $"access_token is missing in tokeninfo";

        var account_request = await client.GetAsync(TOKEN_URL + "/" + HttpUtility.UrlEncode(tokeninfo["access_token"]));
        
        if (account_request.StatusCode != HttpStatusCode.OK)
            return $"Request failed with code {account_request.StatusCode} : {account_request.Content.ReadAsStringAsync().Result}";

        var account_info = JsonConvert.DeserializeObject<Dictionary<string, string>>(account_request.Content.ReadAsStringAsync().Result);

        if (!account_info.ContainsKey("user_id"))
            return $"user_id is missing in account_info";

        var user_id_64 = Convert.ToInt64(account_info["user_id"]);
        var bytes = BitConverter.GetBytes(user_id_64);
        var encoded = Convert.ToBase64String(bytes);
        return encoded;
    }
}
