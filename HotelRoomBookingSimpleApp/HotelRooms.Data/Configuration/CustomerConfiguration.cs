using HotelRooms.Persistence.Models;
using HotelRooms.Persistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRooms.Persistence.Configuration
{
    internal class CustomerConfiguration : EntityConfigurationBase<CustomerModel, int>
    {
        public override void ConfigureMore(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("Customer");
            
        }
    }
}
