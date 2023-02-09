using TvMaze.Domain.Models;
using TvMaze.Persistence.EntityFramework;

namespace TvMazeScraper.WebApi.Integration.Tests.Infrastructure
{
    internal static class DbUtilities
    {
        internal static void InitializeDbForTests(TvMazeDbContext db)
        {
            var show = new Show
            {
                Id = new Guid(),
                Name = "Game of Thrones",
                OfficialSite = "http://www.hbo.com/game-of-thrones",
                Url = "https://www.tvmaze.com/shows/82/game-of-thrones",
                Casts = new List<Cast>
                {
                    new()
                    {
                        Id = new Guid(),
                        Name = "Peter Dinklage",
                        CastId = 14072,
                        CharacterName = "Tyrion Lannister",
                        Birthday = DateTimeOffset.Parse("1969-06-11 00:00:00")
                    },
                    new()
                    {
                        Id = new Guid(),
                        Name = "Kit Harington",
                        CastId = 14075,
                        CharacterName = "Jon Snow",
                        Birthday = DateTimeOffset.Parse("1986-12-26 00:00:00")
                    },
                    new()
                    {
                        Id = new Guid(),
                        Name = "Emilia Clarke",
                        CastId = 14079,
                        CharacterName = "Daenerys Targaryen",
                        Birthday = DateTimeOffset.Parse("1969-10-26 00:00:00")
                    }
                }
            };

            db.Shows.Add(show);

            db.SaveChanges();
        }

        internal static void ReInitializeDbForTests(TvMazeDbContext db)
        {
            InitializeDbForTests(db);
        }
    }
}
