using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMaze.Domain.Models;

namespace TvMaze.Persistence.Configurations
{
    public class ShowEfConfigurations : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Shows");

            builder.HasKey(x => x.Id);
        }
    }
}
