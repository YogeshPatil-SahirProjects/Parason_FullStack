using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parason_Api.Models;

public partial class ParasonDbContext : DbContext
{
    public ParasonDbContext()
    {
    }

    public ParasonDbContext(DbContextOptions<ParasonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttributeDef> AttributeDefs { get; set; }

    public virtual DbSet<AttributeListValue> AttributeListValues { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<ImageRef> ImageRefs { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<LEquipmentAttribute> LEquipmentAttributes { get; set; }

    public virtual DbSet<LEquipmentAttributeValue> LEquipmentAttributeValues { get; set; }

    public virtual DbSet<LProcessEquipment> LProcessEquipments { get; set; }

    public virtual DbSet<LSeriesAttribute> LSeriesAttributes { get; set; }

    public virtual DbSet<LVerticalProcess> LVerticalProcesses { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; }

    public virtual DbSet<QuoteHeader> QuoteHeaders { get; set; }

    public virtual DbSet<QuoteVertical> QuoteVerticals { get; set; }

    public virtual DbSet<ScopeOfSupply> ScopeOfSupplies { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<SpecDetail> SpecDetails { get; set; }

    public virtual DbSet<StgAttributeDef> StgAttributeDefs { get; set; }

    public virtual DbSet<StgAttributeListValue> StgAttributeListValues { get; set; }

    public virtual DbSet<StgEquipment> StgEquipments { get; set; }

    public virtual DbSet<StgLEquipmentAttribute> StgLEquipmentAttributes { get; set; }

    public virtual DbSet<StgLEquipmentAttributeValue> StgLEquipmentAttributeValues { get; set; }

    public virtual DbSet<StgLProcessEquipment> StgLProcessEquipments { get; set; }

    public virtual DbSet<StgLVerticalProcess> StgLVerticalProcesses { get; set; }

    public virtual DbSet<StgModel> StgModels { get; set; }

    public virtual DbSet<StgProcess> StgProcesses { get; set; }

    public virtual DbSet<StgSeries> StgSeries { get; set; }

    public virtual DbSet<StgVerticalArea> StgVerticalAreas { get; set; }

    public virtual DbSet<VerticalArea> VerticalAreas { get; set; }

    public virtual DbSet<VwPriceLatest> VwPriceLatests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SPL-\\SQLEXPRESS;Database=ParaAI;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttributeDef>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Attribut__C189298AAAF0B326");

            entity.ToTable("AttributeDef");

            entity.HasIndex(e => e.AttributeCode, "UQ__Attribut__BD3ED16EE3E86E61").IsUnique();

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AttributeName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.DataType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.UnitDefault).HasMaxLength(50);
        });

        modelBuilder.Entity<AttributeListValue>(entity =>
        {
            entity.HasKey(e => e.ListValueId).HasName("PK__Attribut__DC3873223BDD7CB4");

            entity.ToTable("AttributeListValue");

            entity.HasIndex(e => new { e.AttributeId, e.AttributeValue }, "UQ_AttrList").IsUnique();

            entity.Property(e => e.ListValueId).HasColumnName("ListValueID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeValue).HasMaxLength(200);
            entity.Property(e => e.Display).HasMaxLength(200);

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeListValues)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttrList_AttrDef");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__Equipmen__34474599B8B2739B");

            entity.HasIndex(e => e.EquipmentCode, "UQ__Equipmen__09E4417E7B114A4D").IsUnique();

            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.EquipmentCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
        });

        modelBuilder.Entity<ImageRef>(entity =>
        {
            entity.HasKey(e => e.ImageRefId).HasName("PK__ImageRef__85B7F5121A3A930F");

            entity.ToTable("ImageRef");

            entity.Property(e => e.ImageRefId).HasColumnName("ImageRefID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ImageFileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImagePurpose)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");

            entity.HasOne(d => d.Equipment).WithMany(p => p.ImageRefs)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_ImageRef_Equip");

            entity.HasOne(d => d.Model).WithMany(p => p.ImageRefs)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_ImageRef_Model");

            entity.HasOne(d => d.Series).WithMany(p => p.ImageRefs)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("FK_ImageRef_Series");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__ItemMast__727E838B0451C08B");

            entity.ToTable("ItemMaster");

            entity.HasIndex(e => e.ItemCode, "UQ__ItemMast__3ECC0FEA7E4F3AED").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.Uom)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("EA")
                .HasColumnName("UOM");
        });

        modelBuilder.Entity<LEquipmentAttribute>(entity =>
        {
            entity.HasKey(e => new { e.EquipmentId, e.AttributeId }).HasName("PK_LEA");

            entity.ToTable("L_Equipment_Attribute");

            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeCategory).HasMaxLength(40);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Attribute).WithMany(p => p.LEquipmentAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEA_AttrDef");

            entity.HasOne(d => d.Equipment).WithMany(p => p.LEquipmentAttributes)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEA_Equipment");
        });

        modelBuilder.Entity<LEquipmentAttributeValue>(entity =>
        {
            entity.HasKey(e => new { e.EquipmentId, e.AttributeId, e.SequenceNo }).HasName("PK_LEAV");

            entity.ToTable("L_Equipment_AttributeValue");

            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.SequenceNo).HasDefaultValue(1);
            entity.Property(e => e.ListValueId).HasColumnName("ListValueID");
            entity.Property(e => e.NumValue).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TextValue).HasMaxLength(1000);

            entity.HasOne(d => d.Attribute).WithMany(p => p.LEquipmentAttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEAV_AttrDef");

            entity.HasOne(d => d.Equipment).WithMany(p => p.LEquipmentAttributeValues)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEAV_Equipment");

            entity.HasOne(d => d.ListValue).WithMany(p => p.LEquipmentAttributeValues)
                .HasForeignKey(d => d.ListValueId)
                .HasConstraintName("FK_LEAV_List");
        });

        modelBuilder.Entity<LProcessEquipment>(entity =>
        {
            entity.HasKey(e => new { e.ProcessId, e.EquipmentId });

            entity.ToTable("L_Process_Equipment");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Equipment).WithMany(p => p.LProcessEquipments)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LPE_Equipment");

            entity.HasOne(d => d.Process).WithMany(p => p.LProcessEquipments)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LPE_Process");
        });

        modelBuilder.Entity<LSeriesAttribute>(entity =>
        {
            entity.HasKey(e => new { e.SeriesId, e.AttributeId }).HasName("PK_LSA");

            entity.ToTable("L_Series_Attribute");

            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.AttributeCategory).HasMaxLength(40);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Attribute).WithMany(p => p.LSeriesAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LSA_AttrDef");

            entity.HasOne(d => d.Series).WithMany(p => p.LSeriesAttributes)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LSA_Series");
        });

        modelBuilder.Entity<LVerticalProcess>(entity =>
        {
            entity.HasKey(e => new { e.VerticalId, e.ProcessId });

            entity.ToTable("L_Vertical_Process");

            entity.Property(e => e.VerticalId).HasColumnName("VerticalID");
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Process).WithMany(p => p.LVerticalProcesses)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LVP_Process");

            entity.HasOne(d => d.Vertical).WithMany(p => p.LVerticalProcesses)
                .HasForeignKey(d => d.VerticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LVP_Vertical");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Model__E8D7A1CC5EDD97FF");

            entity.ToTable("Model");

            entity.HasIndex(e => new { e.SeriesId, e.ModelCode }, "UQ_Model").IsUnique();

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModelCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModelName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");

            entity.HasOne(d => d.Series).WithMany(p => p.Models)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Model_Series");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__Price__4957584F3020D81E");

            entity.ToTable("Price");

            entity.HasIndex(e => new { e.EquipmentId, e.EffectiveFrom }, "UX_Price_Equip")
                .IsUnique()
                .HasFilter("([EquipmentID] IS NOT NULL AND [ModelID] IS NULL AND [ItemId] IS NULL)");

            entity.HasIndex(e => new { e.ItemId, e.EffectiveFrom }, "UX_Price_Item")
                .IsUnique()
                .HasFilter("([ItemId] IS NOT NULL AND [EquipmentID] IS NULL AND [ModelID] IS NULL)");

            entity.HasIndex(e => new { e.ModelId, e.EffectiveFrom }, "UX_Price_Model")
                .IsUnique()
                .HasFilter("([ModelID] IS NOT NULL AND [EquipmentID] IS NULL AND [ItemId] IS NULL)");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.BasePriceInr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BasePriceINR");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.EffectiveFrom).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Prices)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_Price_Equip");

            entity.HasOne(d => d.Item).WithMany(p => p.Prices)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Price_Item");

            entity.HasOne(d => d.Model).WithMany(p => p.Prices)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_Price_Model");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__Process__1B39A9766E86ACC3");

            entity.ToTable("Process");

            entity.HasIndex(e => e.ProcessCode, "UQ__Process__721D77A5B2390C54").IsUnique();

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ProcessCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProcessName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QuoteEquipmentOrModel>(entity =>
        {
            entity.HasKey(e => e.Qeomid).HasName("PK__Quote_Eq__949DC346FB948C40");

            entity.ToTable("Quote_Equipment_Or_Model");

            entity.HasIndex(e => new { e.RecordId, e.EquipmentId }, "UX_QEOM_EQUIP")
                .IsUnique()
                .HasFilter("([EquipmentID] IS NOT NULL AND [SeriesID] IS NULL AND [ModelID] IS NULL)");

            entity.HasIndex(e => new { e.RecordId, e.ModelId }, "UX_QEOM_MODEL")
                .IsUnique()
                .HasFilter("([ModelID] IS NOT NULL AND [EquipmentID] IS NULL AND [SeriesID] IS NULL)");

            entity.HasIndex(e => new { e.RecordId, e.SeriesId }, "UX_QEOM_SERIES")
                .IsUnique()
                .HasFilter("([SeriesID] IS NOT NULL AND [EquipmentID] IS NULL AND [ModelID] IS NULL)");

            entity.Property(e => e.Qeomid).HasColumnName("QEOMId");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.PriceInr)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Price_INR");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");

            entity.HasOne(d => d.Equipment).WithMany(p => p.QuoteEquipmentOrModels)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_QEOM_Equip");

            entity.HasOne(d => d.Model).WithMany(p => p.QuoteEquipmentOrModels)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_QEOM_Model");

            entity.HasOne(d => d.Record).WithMany(p => p.QuoteEquipmentOrModels)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QEOM_QV");

            entity.HasOne(d => d.Series).WithMany(p => p.QuoteEquipmentOrModels)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("FK_QEOM_Series");
        });

        modelBuilder.Entity<QuoteHeader>(entity =>
        {
            entity.HasKey(e => new { e.QuoteId, e.QuoteRevision });

            entity.ToTable("QuoteHeader");

            entity.Property(e => e.QuoteId)
                .ValueGeneratedOnAdd()
                .HasColumnName("QuoteID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("INR");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.QuoteName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QuoteNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Draft");
            entity.Property(e => e.ValidityDays).HasDefaultValue(30);
        });

        modelBuilder.Entity<QuoteVertical>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Quote_Ve__FBDF78C90D0AA89E");

            entity.ToTable("Quote_Vertical");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Layer).HasMaxLength(50);
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.QuoteId).HasColumnName("QuoteID");
            entity.Property(e => e.VerticalId).HasColumnName("VerticalID");

            entity.HasOne(d => d.Process).WithMany(p => p.QuoteVerticals)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QV_Process");

            entity.HasOne(d => d.Vertical).WithMany(p => p.QuoteVerticals)
                .HasForeignKey(d => d.VerticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QV_Vertical");

            entity.HasOne(d => d.QuoteHeader).WithMany(p => p.QuoteVerticals)
                .HasForeignKey(d => new { d.QuoteId, d.QuoteRevision })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QV_Quote");
        });

        modelBuilder.Entity<ScopeOfSupply>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.ItemId });

            entity.ToTable("ScopeOfSupply");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.PriceInr)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("Price_INR");
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Item).WithMany(p => p.ScopeOfSupplies)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SoS_Item");

            entity.HasOne(d => d.Model).WithMany(p => p.ScopeOfSupplies)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_SoS_Model");

            entity.HasOne(d => d.Record).WithMany(p => p.ScopeOfSupplies)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SoS_QV");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("PK__Series__F3A1C1015A3C93D4");

            entity.HasIndex(e => new { e.EquipmentId, e.SeriesCode }, "UQ_Series").IsUnique();

            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.SeriesCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SeriesName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Equipment).WithMany(p => p.Series)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Series_Equipment");
        });

        modelBuilder.Entity<SpecDetail>(entity =>
        {
            entity.HasKey(e => new { e.RecordId, e.AttributeId }).HasName("PK_SpecDetails");

            entity.ToTable("Spec_Details");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ListValueId).HasColumnName("ListValueID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.NumValue).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TextValue).HasMaxLength(1000);

            entity.HasOne(d => d.Attribute).WithMany(p => p.SpecDetails)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SD_AttrDef");

            entity.HasOne(d => d.Equipment).WithMany(p => p.SpecDetails)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_SD_Equip");

            entity.HasOne(d => d.ListValue).WithMany(p => p.SpecDetails)
                .HasForeignKey(d => d.ListValueId)
                .HasConstraintName("FK_SD_List");

            entity.HasOne(d => d.Model).WithMany(p => p.SpecDetails)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_SD_Model");

            entity.HasOne(d => d.Record).WithMany(p => p.SpecDetails)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SD_QV");
        });

        modelBuilder.Entity<StgAttributeDef>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_AttributeDef");

            entity.Property(e => e.AttributeCode).HasMaxLength(50);
            entity.Property(e => e.AttributeName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.UnitDefault).HasMaxLength(50);
        });

        modelBuilder.Entity<StgAttributeListValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_AttributeListValue");

            entity.Property(e => e.AttributeCode).HasMaxLength(50);
            entity.Property(e => e.AttributeValue).HasMaxLength(200);
            entity.Property(e => e.Display).HasMaxLength(200);
            entity.Property(e => e.SequenceNo).HasMaxLength(20);
        });

        modelBuilder.Entity<StgEquipment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_Equipment");

            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.EquipmentName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasMaxLength(10);
        });

        modelBuilder.Entity<StgLEquipmentAttribute>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_L_Equipment_Attribute");

            entity.Property(e => e.AttributeCategory).HasMaxLength(40);
            entity.Property(e => e.AttributeCode).HasMaxLength(50);
            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.IsEditable).HasMaxLength(10);
            entity.Property(e => e.IsRequired).HasMaxLength(10);
            entity.Property(e => e.SequenceNo).HasMaxLength(20);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<StgLEquipmentAttributeValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_L_Equipment_AttributeValue");

            entity.Property(e => e.AttributeCode).HasMaxLength(50);
            entity.Property(e => e.BoolValue).HasMaxLength(10);
            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.IsDefault).HasMaxLength(10);
            entity.Property(e => e.ListValue).HasMaxLength(200);
            entity.Property(e => e.NumValue).HasMaxLength(100);
            entity.Property(e => e.SequenceNo).HasMaxLength(20);
            entity.Property(e => e.TextValue).HasMaxLength(1000);
        });

        modelBuilder.Entity<StgLProcessEquipment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_L_Process_Equipment");

            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.IsRequired).HasMaxLength(10);
            entity.Property(e => e.ProcessCode).HasMaxLength(50);
            entity.Property(e => e.SequenceNo).HasMaxLength(20);
        });

        modelBuilder.Entity<StgLVerticalProcess>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_L_Vertical_Process");

            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.IsRequired).HasMaxLength(10);
            entity.Property(e => e.ProcessCode).HasMaxLength(50);
            entity.Property(e => e.SequenceNo).HasMaxLength(20);
            entity.Property(e => e.VerticalCode).HasMaxLength(50);
        });

        modelBuilder.Entity<StgModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_Model");

            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.ModelCode).HasMaxLength(50);
            entity.Property(e => e.ModelName).HasMaxLength(200);
            entity.Property(e => e.SeriesCode).HasMaxLength(50);
        });

        modelBuilder.Entity<StgProcess>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_Process");

            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.ProcessCode).HasMaxLength(50);
            entity.Property(e => e.ProcessName).HasMaxLength(200);
        });

        modelBuilder.Entity<StgSeries>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_Series");

            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.SeriesCode).HasMaxLength(50);
            entity.Property(e => e.SeriesName).HasMaxLength(200);
        });

        modelBuilder.Entity<StgVerticalArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Stg_Vertical_Area");

            entity.Property(e => e.CreatedAt).HasMaxLength(40);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.VerticalCode).HasMaxLength(50);
            entity.Property(e => e.VerticalName).HasMaxLength(200);
        });

        modelBuilder.Entity<VerticalArea>(entity =>
        {
            entity.HasKey(e => e.VerticalId).HasName("PK__Vertical__941FC6D0D60187F3");

            entity.ToTable("Vertical_Area");

            entity.HasIndex(e => e.VerticalCode, "UQ__Vertical__CA33F2F021D0B5CB").IsUnique();

            entity.Property(e => e.VerticalId).HasColumnName("VerticalID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("System");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.VerticalCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VerticalName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPriceLatest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PriceLatest");

            entity.Property(e => e.BasePriceInr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BasePriceINR");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.PriceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PriceID");
            entity.Property(e => e.Rn).HasColumnName("rn");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
