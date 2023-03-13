using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TvMaze.Application.Services;
using TvMaze.Application.Tests.Shared;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Domain.Persistence;
using System.Linq.Expressions;

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

            // Act
            var result = await showsService.GetAsync(id, token);

            // Assert
            showsRepositoryMock.Verify();
            result.Should().Be(show);
        }

        [Theory]
        [AutoMoqData]
        public async void GetByShowIdAsyncShouldReturnShow(int showId, Show show, CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            showRepositoryMock.Setup(x => x.GetByShowIdAsync(It.Is<int>(s => s == showId), token))
                .ReturnsAsync(show).Verifiable();

            // Act
            var result = await showsService.GetByShowIdAsync(showId, token);

            // Assert
            showRepositoryMock.Verify();
            result.Should().Be(show);
        }
        
        #endregion

        #region ListAsync
        
        [Theory]
        [TheoryAutoData]
        public async Task GetAllShowAsyncShouldReturnResult(EntityListRequest<Show> listRequest, CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            showRepositoryMock.Setup(x => x.ListAsync(It.Is<EntityListRequest<Show>>(y => y == listRequest), token))
                .Returns(Task.FromResult(EntityListResponse<Show>.Empty)).Verifiable();

            // Act
            var result = await showsService.GetAllShowAsync(listRequest, token);

            // Assert
            showRepositoryMock.Verify();
            result.Should().NotBeNull();
            result.ResultCount.Should().Be(0);
            result.TotalResultCount.Should().Be(0);
            result.Results.Count().Should().Be(0);
        }

        [Theory]
        [TheoryAutoData]
        public async Task GetAllShowAsyncShouldReturnResultWithData(EntityListRequest<Show> listRequest, EntityListResponse<Show> listResponse, CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            showRepositoryMock.Setup(x => x.ListAsync(It.Is<EntityListRequest<Show>>(y => y == listRequest), token))
                .Returns(Task.FromResult(listResponse)).Verifiable();

            // Act
            var result = await showsService.GetAllShowAsync(listRequest, token);

            // Assert
            showRepositoryMock.Verify();
            result.Should().NotBeNull();
            result.ResultCount.Should().Be(listResponse.ResultCount);
            result.TotalResultCount.Should().Be(listResponse.TotalResultCount);
            result.Results.Count().Should().Be(listResponse.Results.Count());
        }


        [Theory]
        [TheoryAutoData]
        public async Task GetAllWithCastSortedAsyncShouldReturnResult(EntityListRequest<Show> listRequest, CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            showRepositoryMock.Setup(x => x.ListAsync(It.Is<EntityListRequest<Show>>(y => y == listRequest), token))
                .Returns(Task.FromResult(EntityListResponse<Show>.Empty)).Verifiable();

            // Act
            var result = await showsService.GetAllWithCastSortedAsync(listRequest, token);

            // Assert
            showRepositoryMock.Verify();
            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Theory]
        [TheoryAutoData]
        public async Task GetAllWithCastSortedAsyncShouldReturnResultWithData(EntityListRequest<Show> listRequest,  CancellationToken token, [Frozen] Mock<IShowsRepository> showRepositoryMock, ShowsService showsService)
        {
            // Arrange
            var showIdOne = 1;
            var showIdTwo = 2;
            var firstShowDob = DateTimeOffset.Parse("2001-03-01 00:00:00.0000000 +02:00");
            var secondShowDob = DateTimeOffset.Parse("1991-09-25 00:00:00.0000000 +02:00");

            var showList = new List<Show>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "ShowA",
                    ShowId = showIdOne,
                    Casts = new List<Cast>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor A",
                            Birthday = DateTimeOffset.Parse("2001-01-01 00:00:00.0000000 +02:00"),
                            CharacterName = "Character A"
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor B",
                            Birthday = DateTimeOffset.Parse("2001-02-01 00:00:00.0000000 +02:00"),
                            CharacterName = "Character C"
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor C",
                            Birthday = firstShowDob,
                            CharacterName = "Character C"
                        }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "ShowB",
                    ShowId = showIdTwo,
                    Casts = new List<Cast>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor A",
                            Birthday = DateTimeOffset.Parse("1989-08-21 00:00:00.0000000 +02:00"),
                            CharacterName = "Character A"
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor B",
                            Birthday = secondShowDob,
                            CharacterName = "Character C"
                        },
                        new()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Actor C",
                            Birthday = DateTimeOffset.Parse("1990-05-01 00:00:00.0000000 +02:00"),
                            CharacterName = "Character C"
                        }
                    }
                }
            };
            var listResponse =  new EntityListResponse<Show>(showList, 2 , 0);

            showRepositoryMock.Setup(x => x.ListAsync(It.Is<EntityListRequest<Show>>(y => y == listRequest), token))
                .Returns(Task.FromResult(listResponse)).Verifiable();

            // Act
            var result = await showsService.GetAllWithCastSortedAsync(listRequest, token);

            // Assert
            showRepositoryMock.Verify();
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.FirstOrDefault(show => show.ShowId == showIdOne)?.Casts.FirstOrDefault()?.Birthday.Should().Be(firstShowDob);
            result.FirstOrDefault(show => show.ShowId == showIdTwo)?.Casts.FirstOrDefault()?.Birthday.Should().Be(secondShowDob);
        }

        #endregion

        #region Create



        #endregion
    }
}
