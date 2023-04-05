using ChatChallange.Service;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatChallange.Test.Services
{
    public class StooqServiceUnitTest
    {
        public StooqServiceUnitTest()
        {

        }

        //[Test]
        //public void Should_CallEndpointStooq()
        //{
        //    var service = new StooqService();

        //    var mockHttpClient = new Mock<HttpClient>();
        //    mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>()))
        //      .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
        //      {
        //          Content = new StringContent("Conteúdo da resposta")
        //      });

        //    var response = service.CallEndpointStooq("message");
        //}
    }
}
