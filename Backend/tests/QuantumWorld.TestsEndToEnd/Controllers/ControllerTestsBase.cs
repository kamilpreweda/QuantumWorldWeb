using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using QuantumWorld.Core.Domain;

namespace QuantumWorld.TestsEndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly WebApplicationFactory<Program> Server;
        protected readonly HttpClient Client;

        protected readonly IBattle Battle;


        public ControllerTestsBase()
        {
            Server = new WebApplicationFactory<Program>();
            Client = Server.CreateClient();
            Battle = new Battle();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}