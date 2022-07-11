using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SleepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SleepController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SleepController> _logger;
        private static Timer _timer;

        public SleepController(ILogger<SleepController> logger)
        {
            _logger = logger;
        }

        public bool WebSiteIsAvailable(string Url)
        {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += ((Message.Length > 0) ? "\n" : "") + ex.Message;
            }

            return (Message.Length == 0);
        }

        [HttpGet]
        public List<String> Get()
        {
            int counter = 0;
            var result = new List<String>();
            _timer = new Timer(state =>
                {
                    counter++;
                    var newconnection = WebSiteIsAvailable("http://www.google.com");
                    Console.WriteLine(counter.ToString() + " " + newconnection.ToString());
                    result.Add(counter.ToString() + " " + newconnection.ToString());
                }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            
            System.Threading.Thread.Sleep(30000);
            _timer.Dispose();
            
            return result;
        }
    }
}
