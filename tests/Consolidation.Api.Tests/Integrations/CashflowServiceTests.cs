using Microsoft.Extensions.Configuration;
using Moq;
using Consolidation.Api.Integrations.Internal.Cashflow.Models;
using Bogus;
using RestSharp;
using System.Net;
using Consolidation.Api.Integrations.Internal.Cashflow;
using System.Text.Json;
using System.Net.Http;
using Moq.Protected;
using Consolidation.Api.Tests.Common;
using Consolidation.Api.Tests.Common.DataFixtures;

namespace Consolidation.Api.Tests.Integrations
{
    public class CashflowServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IRestClient> _restClientMock;
        private readonly CashflowService _cashflowService;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly RestClient _restClient;
        private const string Host = "http://localhost";

        public CashflowServiceTests()
        {
            _configuration = new();
            _restClientMock = new();
            _httpMessageHandlerMock = new ();
            _restClient = new(new RestClientOptions { ConfigureMessageHandler = _ => _httpMessageHandlerMock.Object });

            _cashflowService = new(_configuration.Object, _restClient);
        }

        [Fact]
        private async Task Should_GetTransactions()
        {
            //Arrange
            var transactionsFake = TransactionFixture.GetTransactionsResponsesFake(2);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(transactionsFake))
                });

            _configuration.Setup(x => x.GetSection(It.IsAny<string>()).Value)
                .Returns(Host);

            //Act
            var result = await _cashflowService.GetTransactions(new DateOnly());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TransactionsResponse>>(result);
            Assert.Contains(result, item => item.Id == transactionsFake.FirstOrDefault().Id);

        }
    }
}