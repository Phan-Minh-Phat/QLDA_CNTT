using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeTai4.Reponsitories.Repositories.Entities;

public partial class DeTai4Context : DbContext
{
    public DeTai4Context()
    {
    }

    public DeTai4Context(DbContextOptions<DeTai4Context> options)
        : base(options)
    {
    }
    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<CompanyInfo> CompanyInfos { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MaintenanceResult> MaintenanceResults { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6KVHB3N\\SQLEXPRESS;Initial Catalog=deTai4;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyInfo>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__CompanyI__2D971CAC8CABD8BC");

            entity.ToTable("CompanyInfo");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D89347B859");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserId, "IX_Customer_UserId");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User");
        });

        modelBuilder.Entity<Design>(entity =>
        {
            entity.HasKey(e => e.DesignId).HasName("PK__Design__32B8E15F8BFE7A9F");

            entity.ToTable("Design");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DesignName).HasMaxLength(100);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD649DF5D15");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.OrderId, "IX_Feedback_OrderId");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Feedback_Order");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__D796AAB50D581F7C");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.OrderId, "IX_Invoice_OrderId");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Invoice_Order");
        });

        modelBuilder.Entity<MaintenanceResult>(entity =>
        {
            entity.HasKey(e => e.MaintenanceResultId).HasName("PK__Maintena__B7A4A728F14F20C9");

            entity.ToTable("MaintenanceResult");

            entity.HasIndex(e => e.ServiceId, "IX_MaintenanceResult_ServiceId");

            entity.HasIndex(e => e.StaffId, "IX_MaintenanceResult_StaffId");

            entity.Property(e => e.ResultDate).HasColumnType("datetime");
            entity.Property(e => e.ResultDescription).HasMaxLength(255);

            entity.HasOne(d => d.Service).WithMany(p => p.MaintenanceResults)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_MaintenanceResult_Service");

            entity.HasOne(d => d.Staff).WithMany(p => p.MaintenanceResults)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_MaintenanceResult_Staff");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF0FD2C276");

            entity.ToTable("Order");

            entity.HasIndex(e => e.CustomerId, "IX_Order_CustomerId");

            entity.HasIndex(e => e.ServiceId, "IX_Order_ServiceId");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Service).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Order_Service");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABEF054011FD6");

            entity.ToTable("Project");

            entity.HasIndex(e => e.CustomerId, "IX_Project_CustomerId");

            entity.HasIndex(e => e.DesignId, "IX_Project_DesignId");

            entity.HasIndex(e => e.StaffId, "IX_Project_StaffId");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectName).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Project_Customer");

            entity.HasOne(d => d.Design).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("FK_Project_Design");

            entity.HasOne(d => d.Staff).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Project_Staff");

            entity.HasMany(d => d.StaffNavigation).WithMany(p => p.ProjectsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectStaff",
                    r => r.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProjectStaff_Staff"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProjectStaff_Project"),
                    j =>
                    {
                        j.HasKey("ProjectId", "StaffId").HasName("PK__ProjectS__1F77F4412B76BD16");
                        j.ToTable("ProjectStaff");
                        j.HasIndex(new[] { "StaffId" }, "IX_ProjectStaff_StaffId");
                    });
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42FCF048FAA1D");

            entity.ToTable("Promotion");

            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionName).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A796E379B");

            entity.ToTable("Service");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ServiceName).HasMaxLength(100);

            entity.HasMany(d => d.Staff).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceStaff",
                    r => r.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ServiceStaff_Staff"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ServiceStaff_Service"),
                    j =>
                    {
                        j.HasKey("ServiceId", "StaffId").HasName("PK__ServiceS__AC76FABB2C901DAC");
                        j.ToTable("ServiceStaff");
                        j.HasIndex(new[] { "StaffId" }, "IX_ServiceStaff_StaffId");
                    });
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB17C95B522F");

            entity.HasIndex(e => e.UserId, "IX_Staff_UserId");

            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C9BF7CD94");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E4C52321FE").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
