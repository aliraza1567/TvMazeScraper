using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TvMaze.Application.Services;
using TvMaze.Application.Tests.Shared;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Tests.Services
{
    public class CastsServiceTests
    {

        #region Get
        [Theory]
        [AutoMoqData]
        public async void GetAsyncShouldReturnCast(Guid id, Cast cast, CancellationToken token, [Frozen] Mock<ICastsRepository> castsRepositoryMock, CastsService castsService)
        {
            castsRepositoryMock.Setup(x => x.GetAsync(It.Is<Guid>(s => s == id), token))
                .Returns(Task.FromResult(cast)).Verifiable();

            // Act
            var result = await castsService.GetAsync(id, token);

            // Assert
            castsRepositoryMock.Verify();
            result.Should().Be(cast);
        }
        #endregion
    }
}
