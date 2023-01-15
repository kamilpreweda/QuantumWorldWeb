using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace QuantumWorld.TestsEndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly WebApplicationFactory<Program> Server;
        protected readonly HttpClient Client;


        public ControllerTestsBase()
        {
            Server = new WebApplicationFactory<Program>();
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}