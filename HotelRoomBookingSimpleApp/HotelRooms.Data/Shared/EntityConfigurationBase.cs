using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelRooms.Persistence.Shared
{
    public abstract class EntityConfigurationBase<T, K> :
        IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureMore(builder);
        }

        public abstract void ConfigureMore(EntityTypeBuilder<T> builder);
    }
}
