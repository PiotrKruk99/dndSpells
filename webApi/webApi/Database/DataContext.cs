using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webApi.Api.DataClassesDto;
// using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;

namespace webApi.Database;

public class DataContext : DbContext
{
    public DbSet<LastUpdate> LastUpdates {get;set;} = null!;
    public DbSet<SpellLongDto> SpellLongDtos { get; set; } = null!;
    public DbSet<SpellShortDto> SpellShortDtos { get; set; } = null!;

    private string DbPath { get; } = @"AppData/base.db";

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // var optionsForTests = options.FindExtension<InMemoryOptionsExtension>();

        if (/*(optionsForTests is null) && */!Directory.Exists(Path.GetDirectoryName(DbPath)))
            throw new Exception("'Data' folder for database file not exists.");

        // if (optionsForTests is null)
        //     this.Database.Migrate();

        // LastUpdates = Set<LastUpdate>();
        // SpellLongDtos = Set<SpellLongDto>();
        // SpellShortDtos = Set<SpellShortDto>();
    }

    public DataContext() : base()
    {
        if (/*(optionsForTests is null) && */!Directory.Exists(Path.GetDirectoryName(DbPath)))
            throw new Exception("'Data' folder for database file not exists.");

        // LastUpdates = Set<LastUpdate>();
        // SpellLongDtos = Set<SpellLongDto>();
        // SpellShortDtos = Set<SpellShortDto>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var splitStringConverter = new ValueConverter<List<string>, string>(v => string.Join("|", v), v => v.Split(new[] { '|' }).ToList());

        modelBuilder.Entity<SpellLongDto>(
            x =>
            {
                x.Property(y => y.Desc).HasConversion(splitStringConverter);
                x.Property(y => y.HigherLevel).HasConversion(splitStringConverter);
                x.Property(y => y.Components).HasConversion(splitStringConverter);
                x.Property(y => y.Classes).HasConversion(splitStringConverter);
                x.Property(y => y.Subclasses).HasConversion(splitStringConverter);
            }
        );
    }
}
