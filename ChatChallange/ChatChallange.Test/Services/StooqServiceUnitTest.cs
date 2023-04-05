using ChatChallange.Service;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChatChallange.Test.Services
{
    public class StooqServiceUnitTest
    {
        private readonly ILogger<StooqService> _logger;

        public StooqServiceUnitTest()
        {
            _logger = Substitute.For<ILogger<StooqService>>();
        }

        [Test]
        public async Task Should_CallEndpointStooq()
        {
            var value = "Symbol,Date,Time,Open,High,Low,Close,Volume AAPL.US, 2023 - 04 - 03, 22:00:08,164.27,166.29,164.22,166.095,56930455";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(value)
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);
            
            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            var service = new StooqService(httpClient, _logger);

            var responseFromRequest = await service.CallEndpointStooq("message");

            Assert.NotNull(responseFromRequest);
            Assert.AreEqual("A cotação da message é de US$ 166.29 por ação.", responseFromRequest);
        }

        [Test]
        public async Task Should_CallEndpointStooq_NotFound()
        {
            var value = "Symbol,Date,Time,Open,High,Low,Close,Volume\r\nSTOCK_CODE,N/D,N/D,N/D,N/D,N/D,N/D,N/D\r\n";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(value)
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            var service = new StooqService(httpClient, _logger);

            var responseFromRequest = await service.CallEndpointStooq("message");

            Assert.NotNull(responseFromRequest);
            Assert.AreEqual("Não foi encontrado valor para o codigo enviado!", responseFromRequest);
        }

        [Test]
        public async Task Should_CallEndpointStooq_Exception()
        {
            var value = "Symbol,Date,Time,Open,High,Low,Close,Volume\r\nSTOCK_CODE,N/D,N/D,N/D,N/D,N/D,N/D,N/D\r\n";
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(value)
            };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
            var service = new StooqService(httpClient, _logger);
            var returned = service.CallEndpointStooq("message");

            Assert.IsNotNull(returned.Exception);
        }
    }
}
