using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StudyKafka.Persistance.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudyKafkaContext>
{
    public StudyKafkaContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<StudyKafkaContext>();
        var connectionString ="Host=127.0.0.1;Database=StudyKafka;Username=admin;Password=123";
        builder.UseNpgsql(connectionString);

        return new StudyKafkaContext(builder.Options);
    }
}