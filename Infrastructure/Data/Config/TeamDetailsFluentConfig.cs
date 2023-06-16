using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TeamDetailsFluentConfig : BaseInfoFluentConfig<TeamDetails>
    {
        public override void Configure(EntityTypeBuilder<TeamDetails> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }
}