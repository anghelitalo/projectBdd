using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace TestProject3;

[Binding]
public class PostAuthenticationStepDefinition : ApiManagerBase
{
    protected RestClient client = new RestClient("https://demostore.gatling.io/api");
    RestRequest request = new RestRequest("/authenticate", Method.Post);
    RestResponse response;
    protected String pureToken;

    [Given(@"the user password")]
    public void GivenTheUserPassword()
    {
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(new { username = "admin", password = "admin" });
        response = client.Execute(request);
    }

    [Given(@"the token with a valid value")]
    public void GivenTheTokenWithAValidValue()
    {
        pureToken = response.Content;
        var res = JObject.Parse(pureToken);
        pureToken = res["token"].ToString();
        Console.WriteLine(pureToken);
        Assert.IsNotNull(pureToken);
    }

    [Then(@"the result status code should be (.*)")]
    public void ThenTheResultStatusCodeShouldBe(int p0)
    {
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
