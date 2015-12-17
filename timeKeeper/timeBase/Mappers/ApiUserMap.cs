using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timeBase.Mappers
{
    class ApiUserMap : EntityTypeConfiguration<ApiUser>
    {
        public ApiUserMap()
        {
            this.ToTable("ApiUsers");
        }
    }
}
