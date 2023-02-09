using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TvMaze.Application.Services;
using TvMaze.Application.Tests.Shared;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Tests.Services
{
    public class ShowsServiceTests
    {

        #region Get
        [Theory]
        [AutoMoqData]
        public async void GetAsyncShouldReturnShow(Guid id, Show show, CancellationToken token, [Frozen] Mock<IShowsRepository> showsRepositoryMock, ShowsService showsService)
        {
            showsRepositoryMock.Setup(x => x.GetAsync(It.Is<Guid>(s => s == id), token))
                .Returns(Task.FromResult(show)).Verifiable();

            var result = await showsService.GetAsync(id, token);

            showsRepositoryMock.Verify();
            result.Should().Be(show);
        }

        [Theory]
        [AutoMoqData]
        public async void GetByAgbCodeAsyncShouldReturnPharmacies(int showId, Show show, CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            showRepositoryMock.Setup(x => x.GetByShowIdAsync(It.Is<int>(s => s == showId), token))
                .ReturnsAsync(show).Verifiable();

            var result = await showsService.GetByShowIdAsync(showId, token);

            showRepositoryMock.Verify();
            result.Should().Be(show);
        }
        #endregion
    }
}
