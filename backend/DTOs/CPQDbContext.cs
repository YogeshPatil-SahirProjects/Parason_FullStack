using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;

namespace Parason_Api.DTOs
{
    public class CPQDbContext : DbContext
    {
        public CPQDbContext(DbContextOptions<CPQDbContext> options) : base(options)
        {
        }

        // Master Data Tables
        public DbSet<VerticalArea> VerticalAreas { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<AttributeDef> AttributeDefs { get; set; }
        public DbSet<AttributeListValue> AttributeListValues { get; set; }
        public DbSet<ItemMaster> ItemMasters { get; set; }

        // Link Tables
        public DbSet<VerticalProcess> VerticalProcesses { get; set; }
        public DbSet<ProcessEquipment> ProcessEquipments { get; set; }
        public DbSet<EquipmentAttribute> EquipmentAttributes { get; set; }
        public DbSet<EquipmentAttributeValue> EquipmentAttributeValues { get; set; }
        public DbSet<SeriesAttribute> SeriesAttributes { get; set; }

        // Supporting Tables
        public DbSet<ImageRef> ImageRefs { get; set; }
        public DbSet<Price> Prices { get; set; }

        // Transaction Tables (Quotation)
        public DbSet<QuoteHeader> QuoteHeaders { get; set; }
        public DbSet<QuoteVertical> QuoteVerticals { get; set; }
        public DbSet<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; }
        public DbSet<ScopeOfSupply> ScopeOfSupplies { get; set; }
        public DbSet<SpecDetails> SpecDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite keys for link tables
            modelBuilder.Entity<VerticalProcess>()
                .HasKey(vp => new { vp.VerticalID, vp.ProcessID });

            modelBuilder.Entity<ProcessEquipment>()
                .HasKey(pe => new { pe.ProcessID, pe.EquipmentID });

            modelBuilder.Entity<EquipmentAttribute>()
                .HasKey(ea => new { ea.EquipmentID, ea.AttributeID });

            modelBuilder.Entity<EquipmentAttributeValue>()
                .HasKey(eav => new { eav.EquipmentID, eav.AttributeID, eav.SequenceNo });

            modelBuilder.Entity<SeriesAttribute>()
                .HasKey(sa => new { sa.SeriesID, sa.AttributeID });

            modelBuilder.Entity<ScopeOfSupply>()
                .HasKey(sos => new { sos.RecordID, sos.ItemId });

            modelBuilder.Entity<SpecDetails>()
                .HasKey(sd => new { sd.RecordID, sd.AttributeID });

            // Configure composite key for QuoteHeader
            modelBuilder.Entity<QuoteHeader>()
                .HasKey(qh => new { qh.QuoteID, qh.QuoteRevision });

            // Configure relationships to prevent cascade delete cycles
            modelBuilder.Entity<VerticalProcess>()
                .HasOne(vp => vp.VerticalArea)
                .WithMany(v => v.VerticalProcesses)
                .HasForeignKey(vp => vp.VerticalID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VerticalProcess>()
                .HasOne(vp => vp.Process)
                .WithMany(p => p.VerticalProcesses)
                .HasForeignKey(vp => vp.ProcessID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProcessEquipment>()
                .HasOne(pe => pe.Process)
                .WithMany(p => p.ProcessEquipments)
                .HasForeignKey(pe => pe.ProcessID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProcessEquipment>()
                .HasOne(pe => pe.Equipment)
                .WithMany(e => e.ProcessEquipments)
                .HasForeignKey(pe => pe.EquipmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure QuoteHeader relationship
            modelBuilder.Entity<QuoteVertical>()
                .HasOne(qv => qv.QuoteHeader)
                .WithMany(qh => qh.QuoteVerticals)
                .HasForeignKey(qv => new { qv.QuoteID, qv.QuoteRevision })
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes for better performance
            modelBuilder.Entity<VerticalArea>()
                .HasIndex(v => v.VerticalCode)
                .IsUnique();

            modelBuilder.Entity<Process>()
                .HasIndex(p => p.ProcessCode)
                .IsUnique();

            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.EquipmentCode)
                .IsUnique();

            modelBuilder.Entity<Series>()
                .HasIndex(s => new { s.EquipmentID, s.SeriesCode })
                .IsUnique()
                .HasDatabaseName("UQ_Series");

            modelBuilder.Entity<Model>()
                .HasIndex(m => new { m.SeriesID, m.ModelCode })
                .IsUnique()
                .HasDatabaseName("UQ_Model");

            modelBuilder.Entity<AttributeDef>()
                .HasIndex(a => a.AttributeCode)
                .IsUnique();

            modelBuilder.Entity<AttributeListValue>()
                .HasIndex(alv => new { alv.AttributeID, alv.AttributeValue })
                .IsUnique()
                .HasDatabaseName("UQ_AttrList");

            modelBuilder.Entity<ItemMaster>()
                .HasIndex(i => i.ItemCode)
                .IsUnique();

            // Configure filtered indexes for Price table
            modelBuilder.Entity<Price>()
                .HasIndex(p => new { p.EquipmentID, p.EffectiveFrom })
                .HasDatabaseName("UX_Price_Equip")
                .HasFilter("EquipmentID IS NOT NULL AND ModelID IS NULL AND ItemId IS NULL")
                .IsUnique();

            modelBuilder.Entity<Price>()
                .HasIndex(p => new { p.ModelID, p.EffectiveFrom })
                .HasDatabaseName("UX_Price_Model")
                .HasFilter("ModelID IS NOT NULL AND EquipmentID IS NULL AND ItemId IS NULL")
                .IsUnique();

            modelBuilder.Entity<Price>()
                .HasIndex(p => new { p.ItemId, p.EffectiveFrom })
                .HasDatabaseName("UX_Price_Item")
                .HasFilter("ItemId IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL")
                .IsUnique();

            // Configure filtered indexes for QuoteEquipmentOrModel
            modelBuilder.Entity<QuoteEquipmentOrModel>()
                .HasIndex(qeom => new { qeom.RecordID, qeom.EquipmentID })
                .HasDatabaseName("UX_QEOM_EQUIP")
                .HasFilter("EquipmentID IS NOT NULL AND SeriesID IS NULL AND ModelID IS NULL")
                .IsUnique();

            modelBuilder.Entity<QuoteEquipmentOrModel>()
                .HasIndex(qeom => new { qeom.RecordID, qeom.SeriesID })
                .HasDatabaseName("UX_QEOM_SERIES")
                .HasFilter("SeriesID IS NOT NULL AND EquipmentID IS NULL AND ModelID IS NULL")
                .IsUnique();

            modelBuilder.Entity<QuoteEquipmentOrModel>()
                .HasIndex(qeom => new { qeom.RecordID, qeom.ModelID })
                .HasDatabaseName("UX_QEOM_MODEL")
                .HasFilter("ModelID IS NOT NULL AND EquipmentID IS NULL AND SeriesID IS NULL")
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<EquipmentAttributeValue>()
                .Property(eav => eav.NumValue)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<SpecDetails>()
                .Property(sd => sd.NumValue)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<Price>()
                .Property(p => p.BasePriceINR)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<QuoteEquipmentOrModel>()
                .Property(qeom => qeom.Price_INR)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<ScopeOfSupply>()
                .Property(sos => sos.Price_INR)
                .HasColumnType("decimal(18,4)");

            // Configure datetime columns
            modelBuilder.Entity<VerticalArea>()
                .Property(v => v.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Process>()
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Series>()
                .Property(s => s.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Model>()
                .Property(m => m.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<AttributeDef>()
                .Property(a => a.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<ItemMaster>()
                .Property(i => i.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<ImageRef>()
                .Property(ir => ir.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Price>()
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<Price>()
                .Property(p => p.EffectiveFrom)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<QuoteHeader>()
                .Property(qh => qh.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            modelBuilder.Entity<QuoteVertical>()
                .Property(qv => qv.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSUTCDATETIME()");

            // Configure default values
            modelBuilder.Entity<VerticalArea>()
                .Property(v => v.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<VerticalArea>()
                .Property(v => v.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<Process>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Process>()
                .Property(p => p.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.CreatedBy)
                .HasDefaultValue("System");

            modelBuilder.Entity<QuoteHeader>()
                .Property(qh => qh.Status)
                .HasDefaultValue("Draft");

            modelBuilder.Entity<QuoteHeader>()
                .Property(qh => qh.Currency)
                .HasDefaultValue("INR");

            modelBuilder.Entity<QuoteHeader>()
                .Property(qh => qh.ValidityDays)
                .HasDefaultValue(30);

            modelBuilder.Entity<QuoteHeader>()
                .Property(qh => qh.QuoteRevision)
                .HasDefaultValue((byte)0);


            //Add seed data
            modelBuilder.Entity<VerticalArea>()
                .HasData(
                new VerticalArea { VerticalID = 1, VerticalCode = "SPT", VerticalName = "Stock Preparation", Description = "Stock Preparation", IsActive = true, CreatedAt = new DateTime(2025, 10, 23), CreatedBy = "Yogesh P" },
                new VerticalArea { VerticalID = 2, VerticalCode = "PP", VerticalName = "Pulp Preparation", Description = "Pulp Preparation", IsActive = true, CreatedAt = new DateTime(2025, 10, 23), CreatedBy = "Yogesh P" }
                );

            modelBuilder.Entity<Process>()
               .HasData(
               new Process { ProcessID = 1, ProcessCode = "LCP", ProcessName = "Low Consistency Pulping", Description = "Stock Preparation", IsActive = true, CreatedAt = new DateTime(2025, 10, 23), CreatedBy = "Yogesh P" },
               new Process { ProcessID = 2, ProcessCode = "HCP", ProcessName = "High Consistency Pulping", Description = "Pulp Preparation", IsActive = true, CreatedAt = new DateTime(2025, 10, 23), CreatedBy = "Yogesh P" }
               );

            modelBuilder.Entity<QuoteHeader>()
                .HasData(
                    new QuoteHeader
                    {
                        QuoteID = 1,
                        QuoteRevision = 1,
                        QuoteNumber = "Q-2025-001",
                        QuoteName = "First Demo Quote",
                        CustomerName = "ABC Industries",
                        Status = "Draft",
                        Currency = "INR",
                        ValidityDays = 30,
                        Notes = "Seed sample quote",
                        CreatedAt = new DateTime(2025, 01, 01), // ✅ static date required
                        CreatedBy = "System",
                        ModifiedAt = new DateTime(2025, 01, 01),
                        ModifiedBy = "Yogesh Patil"
                    },
                    new QuoteHeader
                    {
                        QuoteID = 2,
                        QuoteRevision = 1,
                        QuoteNumber = "Q-2025-002",
                        QuoteName = "Second Demo Quote",
                        CustomerName = "XYZ Manufacturing",
                        Status = "Approved",
                        Currency = "USD",
                        ValidityDays = 45,
                        Notes = "Second seed record",
                        CreatedAt = new DateTime(2025, 01, 02),
                        CreatedBy = "System",
                        ModifiedAt = new DateTime(2025, 01, 01),
                        ModifiedBy = "Yogesh Patil"
                    }
                );
        }
    }
}
