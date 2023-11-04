using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace WildLifeApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        Batteries_V2.Init();

        Database.EnsureCreated();
    }

    public DbSet<Ability> AbilityDex { get; set; }
    public DbSet<Move> MoveDex { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<MoveClass> MoveClasses { get; set; }
    public DbSet<Pokemon> Pokedex { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<AttackMod> AttackModifiers { get; set; }
    public DbSet<Learnset> Learnsets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");

        using var inputStream = FileSystem.Current.OpenAppPackageFileAsync("app.db").Result;


        bool create = true;
        bool exists = File.Exists(dbPath);

        if (exists)
        {
            var file = new FileStream(dbPath, FileMode.Open);
            var virt = FileSystem.Current.OpenAppPackageFileAsync("app.db").Result;

            using (var md5 = MD5.Create())
            {
                
                byte[] firstHash = MD5.Create().ComputeHash(file);
                byte[] secondHash = MD5.Create().ComputeHash(virt);

                for (int i=0; i<firstHash.Length; i++)
                {
                    if (firstHash[i] != secondHash[i])
                        create= false;
                }
            }
            
            file.Close();
        }

        if (create)
        {
            if (exists)
            {
                File.Delete(dbPath);
            }

            // Create an output filename
            // Copy the file to the AppDataDirectory
            using var outputStream = File.Create(dbPath);
            inputStream.CopyTo(outputStream);
        }

        optionsBuilder
            .UseSqlite($"Filename={dbPath}");
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Pokemon>().HasMany(e => e.Abilities).WithMany();
        base.OnModelCreating(builder);
    }
}

public class Ability
{
    public string ID { get; set; }

    public string? Name_DE { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }

    public bool IsTrait { get; set; }
    public string? Requirement { get; set; }
}

public class Move
{
    public string ID { get; set; }
    public string? Name_DE { get; set; }
    public string Name { get; set; }

    public Type type { get; set; }
    public string? DamageDice { get; set; }
    public MoveClass MoveClass { get; set; }
    public string Target { get; set; }
    public string Effect { get; set; }
}

public class Type
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string? Name_DE { get; set; }
}

public class MoveClass
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string? Name_DE { get; set; }
}

public class Item
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }
    public string? Name_DE { get; set; }
}

public class Pokemon
{
    [Column(Order = 2)] public string ID { get; set; }

    [Column(Order = 0)] public int Order { get; set; }

    public string? ImageLink { get; set; }

    public string Name { get; set; }
    public string? Name_DE { get; set; }

    public string? Form { get; set; }
    public List<Ability> Abilities { get; set; }
    public Type Type1 { get; set; }
    public Type? Type2 { get; set; }

    public int HEALTH { get; set; }
    public int ATK { get; set; }
    public int DEF { get; set; }
    public int SP_ATK { get; set; }
    public int SP_DEF { get; set; }
    public int SPEED { get; set; }
}

public class Learnset
{
    public string ID { get; set; }

    public Pokemon mon { get; set; }

    public Move move { get; set; }
    public string how { get; set; }

    public int level { get; set; }

    public string? source { get; set; }
}

public class SkillProfiency
{
    public string ID { get; set; }
    public Skill skill { get; set; }
    public int level { get; set; }
}

public class Skill
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string? Name_DE { get; set; }

    public string Effect { get; set; }
}

public class AttackMod
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string? Name_DE { get; set; }

    public string Effect { get; set; }
}