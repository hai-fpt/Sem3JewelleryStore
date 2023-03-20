using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JewelleryStore.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminLoginMst> AdminLoginMsts { get; set; }

    public virtual DbSet<BrandMst> BrandMsts { get; set; }

    public virtual DbSet<CartList> CartLists { get; set; }

    public virtual DbSet<CatMst> CatMsts { get; set; }

    public virtual DbSet<CertifyMst> CertifyMsts { get; set; }

    public virtual DbSet<DimInfoMst> DimInfoMsts { get; set; }

    public virtual DbSet<DimMst> DimMsts { get; set; }

    public virtual DbSet<DimQltyMst> DimQltyMsts { get; set; }

    public virtual DbSet<DimQltySubMst> DimQltySubMsts { get; set; }

    public virtual DbSet<GoldKrtMst> GoldKrtMsts { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }

    public virtual DbSet<ItemMst> ItemMsts { get; set; }

    public virtual DbSet<JewelTypeMst> JewelTypeMsts { get; set; }

    public virtual DbSet<ProdMst> ProdMsts { get; set; }

    public virtual DbSet<StoneMst> StoneMsts { get; set; }

    public virtual DbSet<StoneQltyMst> StoneQltyMsts { get; set; }

    public virtual DbSet<UserRegMst> UserRegMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=JewelryStore;uid=root;password=password", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AdminLoginMst>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PRIMARY");

            entity.ToTable("AdminLoginMst");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<BrandMst>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("BrandMst");

            entity.Property(e => e.BrandId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Brand_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BrandType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Brand_Type");
        });

        modelBuilder.Entity<CartList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CartList");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mrp)
                .HasPrecision(10, 2)
                .HasColumnName("MRP");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Product_Name");

            entity.HasMany(d => d.Jewels).WithMany(p => p.Carts)
                .UsingEntity<Dictionary<string, object>>(
                    "CartJewel",
                    r => r.HasOne<JewelTypeMst>().WithMany()
                        .HasForeignKey("JewelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("cartjewel_ibfk_2"),
                    l => l.HasOne<CartList>().WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("cartjewel_ibfk_1"),
                    j =>
                    {
                        j.HasKey("CartId", "JewelId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("CartJewel");
                        j.HasIndex(new[] { "JewelId" }, "Jewel_ID");
                        j.IndexerProperty<string>("CartId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("Cart_ID")
                            .UseCollation("utf8mb3_general_ci")
                            .HasCharSet("utf8mb3");
                        j.IndexerProperty<string>("JewelId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("Jewel_ID")
                            .UseCollation("utf8mb3_general_ci")
                            .HasCharSet("utf8mb3");
                    });
        });

        modelBuilder.Entity<CatMst>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PRIMARY");

            entity.ToTable("CatMst");

            entity.Property(e => e.CatId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Cat_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CatName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Cat_Name");
        });

        modelBuilder.Entity<CertifyMst>(entity =>
        {
            entity.HasKey(e => e.CertifyId).HasName("PRIMARY");

            entity.ToTable("CertifyMst");

            entity.Property(e => e.CertifyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Certify_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CertifyType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Certify_Type");
        });

        modelBuilder.Entity<DimInfoMst>(entity =>
        {
            entity.HasKey(e => e.DimId).HasName("PRIMARY");

            entity.ToTable("DimInfoMst");

            entity.Property(e => e.DimId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimCrt)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DimImg)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DimPrice)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimSubType)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DimType)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<DimMst>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DimMst");

            entity.HasIndex(e => e.DimId, "DimID");

            entity.HasIndex(e => e.DimQltyId, "DimQlty_ID");

            entity.HasIndex(e => e.DimSubTypeId, "DimSubType_ID");

            entity.HasIndex(e => e.StyleCode, "Style_Code");

            entity.Property(e => e.DimAmt)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Amt");
            entity.Property(e => e.DimCrt)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Crt");
            entity.Property(e => e.DimGm)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Gm");
            entity.Property(e => e.DimId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimPcs)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Pcs");
            entity.Property(e => e.DimQltyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimQlty_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimRate)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Rate");
            entity.Property(e => e.DimSize)
                .HasPrecision(10, 2)
                .HasColumnName("Dim_Size");
            entity.Property(e => e.DimSubTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimSubType_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.StyleCode)
                .HasMaxLength(50)
                .HasColumnName("Style_Code");

            entity.HasOne(d => d.Dim).WithMany()
                .HasForeignKey(d => d.DimId)
                .HasConstraintName("dimmst_ibfk_2");

            entity.HasOne(d => d.DimQlty).WithMany()
                .HasForeignKey(d => d.DimQltyId)
                .HasConstraintName("dimmst_ibfk_3");

            entity.HasOne(d => d.DimSubType).WithMany()
                .HasForeignKey(d => d.DimSubTypeId)
                .HasConstraintName("dimmst_ibfk_4");

            entity.HasOne(d => d.StyleCodeNavigation).WithMany()
                .HasForeignKey(d => d.StyleCode)
                .HasConstraintName("dimmst_ibfk_1");
        });

        modelBuilder.Entity<DimQltyMst>(entity =>
        {
            entity.HasKey(e => e.DimQltyId).HasName("PRIMARY");

            entity.ToTable("DimQltyMst");

            entity.Property(e => e.DimQltyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimQlty_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimQlty)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<DimQltySubMst>(entity =>
        {
            entity.HasKey(e => e.DimSubTypeId).HasName("PRIMARY");

            entity.ToTable("DimQltySubMst");

            entity.Property(e => e.DimSubTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DimSubType_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DimQlty)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<GoldKrtMst>(entity =>
        {
            entity.HasKey(e => e.GoldTypeId).HasName("PRIMARY");

            entity.ToTable("GoldKrtMst");

            entity.Property(e => e.GoldTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("GoldType_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.GoldCrt)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Gold_Crt");
        });

        modelBuilder.Entity<Inquiry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Inquiry");

            entity.HasIndex(e => e.UserId, "userID");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Comment)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Contact)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("EmailID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("userID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.User).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inquiry_ibfk_1");
        });

        modelBuilder.Entity<ItemMst>(entity =>
        {
            entity.HasKey(e => e.StyleCode).HasName("PRIMARY");

            entity.ToTable("ItemMst");

            entity.HasIndex(e => e.BrandId, "Brand_ID");

            entity.HasIndex(e => e.CatId, "Cat_ID");

            entity.HasIndex(e => e.CertifyId, "Certify_ID");

            entity.HasIndex(e => e.GoldTypeId, "GoldType_ID");

            entity.HasIndex(e => e.ProdId, "Prod_ID");

            entity.Property(e => e.StyleCode)
                .HasMaxLength(50)
                .HasColumnName("Style_Code");
            entity.Property(e => e.BrandId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Brand_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CatId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Cat_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CertifyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Certify_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.GoldAmt)
                .HasPrecision(10, 2)
                .HasColumnName("Gold_Amt");
            entity.Property(e => e.GoldMaking)
                .HasPrecision(10, 2)
                .HasColumnName("Gold_Making");
            entity.Property(e => e.GoldRate)
                .HasPrecision(10, 2)
                .HasColumnName("Gold_Rate");
            entity.Property(e => e.GoldTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("GoldType_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.GoldWt)
                .HasPrecision(10, 3)
                .HasColumnName("Gold_Wt");
            entity.Property(e => e.Mrp)
                .HasPrecision(10, 2)
                .HasColumnName("MRP");
            entity.Property(e => e.NetGold)
                .HasPrecision(10, 3)
                .HasColumnName("Net_Gold");
            entity.Property(e => e.OtherMaking)
                .HasPrecision(10, 2)
                .HasColumnName("Other_Making");
            entity.Property(e => e.Pairs).HasPrecision(3);
            entity.Property(e => e.ProdId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Prod_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProdQuality)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Prod_Quality");
            entity.Property(e => e.Quantity).HasPrecision(18);
            entity.Property(e => e.StoneMaking)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Making");
            entity.Property(e => e.StoneWt)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Wt");
            entity.Property(e => e.TotGrossWt)
                .HasPrecision(10, 3)
                .HasColumnName("Tot_Gross_Wt");
            entity.Property(e => e.TotMaking)
                .HasPrecision(10, 2)
                .HasColumnName("Tot_Making");
            entity.Property(e => e.Wstg).HasPrecision(10, 3);
            entity.Property(e => e.WstgPer)
                .HasPrecision(10, 3)
                .HasColumnName("Wstg_Per");

            entity.HasOne(d => d.Brand).WithMany(p => p.ItemMsts)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("itemmst_ibfk_1");

            entity.HasOne(d => d.Cat).WithMany(p => p.ItemMsts)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("itemmst_ibfk_2");

            entity.HasOne(d => d.Certify).WithMany(p => p.ItemMsts)
                .HasForeignKey(d => d.CertifyId)
                .HasConstraintName("itemmst_ibfk_3");

            entity.HasOne(d => d.GoldType).WithMany(p => p.ItemMsts)
                .HasForeignKey(d => d.GoldTypeId)
                .HasConstraintName("itemmst_ibfk_5");

            entity.HasOne(d => d.Prod).WithMany(p => p.ItemMsts)
                .HasForeignKey(d => d.ProdId)
                .HasConstraintName("itemmst_ibfk_4");
        });

        modelBuilder.Entity<JewelTypeMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("JewelTypeMst");

            entity.HasIndex(e => e.ItemId, "Item_ID");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Item_ID");
            entity.Property(e => e.JewelleryType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Jewellery_Type");

            entity.HasOne(d => d.Item).WithMany(p => p.JewelTypeMsts)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jeweltypemst_ibfk_1");
        });

        modelBuilder.Entity<ProdMst>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PRIMARY");

            entity.ToTable("ProdMst");

            entity.Property(e => e.ProdId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Prod_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProdType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Prod_Type");
        });

        modelBuilder.Entity<StoneMst>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StoneMst");

            entity.HasIndex(e => e.StoneQltyId, "StoneQlty_ID");

            entity.HasIndex(e => e.StyleCode, "Style_Code");

            entity.Property(e => e.StoneAmt)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Amt");
            entity.Property(e => e.StoneGm)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Gm");
            entity.Property(e => e.StonePcs)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Pcs");
            entity.Property(e => e.StoneQltyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("StoneQlty_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.StoneRate)
                .HasPrecision(10, 2)
                .HasColumnName("Stone_Rate");
            entity.Property(e => e.StyleCode)
                .HasMaxLength(50)
                .HasColumnName("Style_Code");

            entity.HasOne(d => d.StoneQlty).WithMany()
                .HasForeignKey(d => d.StoneQltyId)
                .HasConstraintName("stonemst_ibfk_2");

            entity.HasOne(d => d.StyleCodeNavigation).WithMany()
                .HasForeignKey(d => d.StyleCode)
                .HasConstraintName("stonemst_ibfk_1");
        });

        modelBuilder.Entity<StoneQltyMst>(entity =>
        {
            entity.HasKey(e => e.StoneQltyId).HasName("PRIMARY");

            entity.ToTable("StoneQltyMst");

            entity.Property(e => e.StoneQltyId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("StoneQlty_ID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.StoneQlty).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRegMst>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("UserRegMst");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("userID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Cdate)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("cdate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Dob)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("dob")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("emailID");
            entity.Property(e => e.MobNo)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("mobNo");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserFname)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("userFname");
            entity.Property(e => e.UserLname)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("userLname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
