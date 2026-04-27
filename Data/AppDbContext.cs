using Microsoft.EntityFrameworkCore;
using TorqIQ.Models;

namespace TorqIQ.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Machine> Machines => Set<Machine>();
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();
    public DbSet<FailureRecord> FailureRecords => Set<FailureRecord>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Machine>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(20);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.Brand).HasMaxLength(100);
            e.Property(x => x.Period).HasMaxLength(50).HasDefaultValue("Annually");
            e.Property(x => x.Energy).HasMaxLength(50).HasDefaultValue("Electric");
            e.Property(x => x.Voltage).HasMaxLength(30);
            e.Property(x => x.Pressure).HasMaxLength(30);
            e.Property(x => x.Power).HasMaxLength(30);
            e.Property(x => x.Usage).HasMaxLength(100);
        });

        b.Entity<MaintenanceRecord>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Date).HasMaxLength(50);
            e.Property(x => x.PerformedBy).HasMaxLength(100);
            e.Property(x => x.Result).HasMaxLength(50).HasDefaultValue("OK");
            e.HasOne(x => x.Machine)
             .WithMany(x => x.MaintenanceRecords)
             .HasForeignKey(x => x.MachineId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<FailureRecord>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Date).HasMaxLength(50);
            e.Property(x => x.Description).HasMaxLength(200);
            e.Property(x => x.FaultType).HasMaxLength(50);
            e.Property(x => x.AssignedTo).HasMaxLength(100);
            e.Property(x => x.Result).HasMaxLength(50).HasDefaultValue("OK");
            e.HasOne(x => x.Machine)
             .WithMany(x => x.FailureRecords)
             .HasForeignKey(x => x.MachineId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }

    // Seed data — sadece DB ilk oluşturulduğunda çağrılır
    public void SeedIfEmpty()
    {
        if (Machines.Any()) return;

        var ak = new Machine
        {
            Code = "AK-02", Name = "Notching Machine", Brand = "In-house Fabrication",
            Period = "Annually", PlanDate = new DateOnly(2001, 1, 10), CardDate = new DateOnly(2017, 1, 3),
            Energy = "Electric", Voltage = "380 V", Usage = "NOTCHING",
            Specs = "Electric-drive machine used for corner bracket notching.",
            CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
        };
        var hy = new Machine
        {
            Code = "HY-05", Name = "Hydraulic Press", Brand = "Ermak",
            Period = "Semi-Annual", PlanDate = new DateOnly(2005, 3, 15), CardDate = new DateOnly(2020, 6, 1),
            Energy = "Hydraulic", Voltage = "220 V", Pressure = "200 bar", Power = "15 kW",
            Usage = "PRESSING", Specs = "Used for bending sheet metal components.",
            CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow
        };

        Machines.AddRange(ak, hy);
        SaveChanges();

        MaintenanceRecords.AddRange(
            new MaintenanceRecord { MachineId = ak.Id, Date = "10 Jan 2001", PerformedBy = "D. Subaşı",                WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "07 Jan 2002", PerformedBy = "T. Keske",                 WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "21 Oct 2003", PerformedBy = "D. Subaşı / E. Bunarcı",  WorkDone = "Machine refurbished, guards installed", Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "02 Jan 2004", PerformedBy = "S. Tanrıdağ / E. Bunarcı", WorkDone = "",                                    Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "17 Jan 2005", PerformedBy = "E. Bunarcı",               WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "25 Jan 2006", PerformedBy = "E. Bunarcı / M. Solak",   WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "25 Oct 2007", PerformedBy = "E. Bunarcı / Y. Kılıçlı", WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = ak.Id, Date = "12 Jan 2008", PerformedBy = "E. Bunarcı",               WorkDone = "Maintenance performed per checklist",   Result = "OK" },
            new MaintenanceRecord { MachineId = hy.Id, Date = "15 Mar 2005", PerformedBy = "M. Solak",                WorkDone = "Initial maintenance performed",          Result = "OK" }
        );

        FailureRecords.AddRange(
            new FailureRecord { MachineId = ak.Id, Date = "01 Oct 2003", Description = "Overhaul",          FaultType = "Mechanical", Action1 = "Overhaul completed — repairs done", AssignedTo = "D. Subaşı",    Result = "OK" },
            new FailureRecord { MachineId = ak.Id, Date = "13 Oct 2012", Description = "Valve piston seal", FaultType = "Pneumatic",  Action1 = "Seal replaced",                    AssignedTo = "T. Karayiğit", Result = "OK" },
            new FailureRecord { MachineId = ak.Id, Date = "20 Oct 2015", Description = "Wiring fault",      FaultType = "Electrical", Action1 = "Cable replaced",                   AssignedTo = "E. Bunarcı",   Result = "OK" },
            new FailureRecord { MachineId = ak.Id, Date = "03 Feb 2016", Description = "Air leak",          FaultType = "Pneumatic",  Action1 = "Repaired",                         AssignedTo = "T. Karayiğit", Result = "OK" },
            new FailureRecord { MachineId = hy.Id, Date = "10 May 2018", Description = "Oil leak",          FaultType = "Mechanical", Action1 = "Gasket replaced",                  AssignedTo = "M. Solak",     Result = "OK" }
        );

        SaveChanges();
    }
}
