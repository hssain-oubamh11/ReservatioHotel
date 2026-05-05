using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using SystemGestionReservation.Infrastructure.Data;

namespace SolutionGestionUniversitaire.Infrastructure
{
    public class SystemGestionReservationContextContextFactory : IDesignTimeDbContextFactory<SystemGestionReservationContext>

    {
        public SystemGestionReservationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new
            DbContextOptionsBuilder<SystemGestionReservationContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=Hotel_MINIDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=""SQL Server Management Studio"";Command Timeout=0")
;
            return new SystemGestionReservationContext(optionsBuilder.Options);
        }
    }

}
