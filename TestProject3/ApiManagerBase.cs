using System.Text.Json;
using RestSharp;

namespace TestProject3;

public class ApiManagerBase
{
    public String uriBase = "https://demostore.gatling.io/api";
    public String pureToken { get; set; }
}