using System.Net;
using RestSharp;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;

namespace TestProject3;

[Binding]
public class ProductStepDefinition : ApiManagerBase
{
    protected RestClient client = new RestClient("https://demostore.gatling.io/api");
    RestRequest requestAuth = new RestRequest("/authenticate", Method.Post);
    RestRequest requestProd = new RestRequest("/product", Method.Post);
    RestResponse response;
    protected String pureToken;
    
   
    [Then(@"the user creates a new product with the following information")]
    public void ThenTheUserCreatesANewProductWithTheFollowingInformation(Table table)
    {
        string[] expectedName = AsStrings(table, "name");
        string[] expecteDescription = AsStrings(table, "description");
        string[] expectedImage = AsStrings(table, "image");
        string[] expectedPrice = AsStrings(table, "price");
        string[] expectedCategory = AsStrings(table, "category");
    
        requestProd.RequestFormat = DataFormat.Json;
        requestProd.AddHeader("Authorization", pureToken);
        requestProd.AddJsonBody(new { name = expectedName, description = expecteDescription
            ,image = expectedImage, price = expectedPrice,category = expectedCategory});
        response = client.Execute(requestProd);
    }

    [Then(@"Then the result status code should be (.*)")]
    public void ThenThenTheResultStatusCodeShouldBe(int p0)
    {
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Then(@"The user sees the proper product information created")]
    public void ThenTheUserSeesTheProperProductInformationCreated()
    {
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    public static string[] AsStrings(Table table, string column)
    {
        return table.Rows.Select(row => row[column]).ToArray();
    }

    [Given(@"the user password to product")]
    public void GivenTheUserPasswordToProduct()
    {
        requestAuth.RequestFormat = DataFormat.Json;
        requestAuth.AddJsonBody(new { username = "admin", password = "admin" });
        response = client.Execute(requestAuth);
    }

    [Given(@"the token with a valid value to product")]
    public void GivenTheTokenWithAValidValueToProduct()
    {
        pureToken = response.Content;
        var res = JObject.Parse(pureToken);
        pureToken = res["token"].ToString();
        Console.WriteLine(pureToken);
        Assert.IsNotNull(pureToken);
    }
}