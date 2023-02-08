namespace TvMaze.Application.Abstractions.Services.Shows;

public interface IScraperService
{
    Task<bool> ShowAndCastScraperAsync(CancellationToken cancellationToken);
}