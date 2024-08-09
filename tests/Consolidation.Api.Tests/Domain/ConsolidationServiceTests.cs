using Consolidation.Api.Data.Repositories.Interfaces;
using Consolidation.Api.Domain.Services;
using Consolidation.Api.Integrations.Internal.Cashflow.Interfaces;
using Consolidation.Api.Tests.Common;
using Consolidation.Api.Tests.Common.DataFixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace Consolidation.Api.Tests.Domain
{
    public class ConsolidationServiceTests
    {
        private readonly Mock<ICashflowService> _mockCashflowService;
        private readonly Mock<ILogger<ConsolidationService>> _mockLogger;
        private readonly Mock<IConsolidationRepository> _mockConsolidationRepository;

        public ConsolidationServiceTests()
        {
            _mockCashflowService = new();
            _mockLogger = new();
            _mockConsolidationRepository = new();
        }

        [Fact]
        public async Task Should_ProcessConsolidation()
        {
            //Arrange
            var consolidationFake = ConsolidationFixture.GenerateConsolidationFake(1);

            _mockConsolidationRepository.SetupSequence(x => x.GetConsolidationByRange(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .ReturnsAsync(consolidationFake)
                .ReturnsAsync(ConsolidationFixture.GenerateConsolidationFake(1));

            _mockCashflowService.Setup(x => x.GetTransactions(It.IsAny<DateOnly>()))
                .ReturnsAsync(TransactionFixture.GetTransactionsResponsesFake(1));

            //Act
            var service = new ConsolidationService(_mockCashflowService.Object, _mockLogger.Object, _mockConsolidationRepository.Object);

            bool result = await service.ProcessConsolidation(DateOnly.FromDateTime(DateTime.Now));

            Assert.NotNull(result);
            Assert.True(result);
        }
    }
}