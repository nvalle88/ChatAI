using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Models.Database;

public partial class AgenteAiContext : DbContext
{
    public AgenteAiContext()
    {
    }

    public AgenteAiContext(DbContextOptions<AgenteAiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiEndpoint> ApiEndpoints { get; set; }

    public virtual DbSet<ApiHeader> ApiHeaders { get; set; }

    public virtual DbSet<ApiProvider> ApiProviders { get; set; }

    public virtual DbSet<AssistantConfig> AssistantConfigs { get; set; }

    public virtual DbSet<AssistantTool> AssistantTools { get; set; }

    public virtual DbSet<AssistantVectorStore> AssistantVectorStores { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<VectorStore> VectorStores { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiEndpoint>(entity =>
        {
            entity.HasKey(e => new { e.AssistantConfigId, e.TypeName });

            entity.HasIndex(e => e.AssistantConfigId, "idx_ApiEndpoints_Assistant");

            entity.Property(e => e.AssistantConfigId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AdditionalInstructions)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ApiVersion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PathTemplate)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.AssistantConfig).WithMany(p => p.ApiEndpoints)
                .HasForeignKey(d => d.AssistantConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiEndpoints_AssistantConfigs");

            entity.HasOne(d => d.TypeNameNavigation).WithMany(p => p.ApiEndpoints)
                .HasForeignKey(d => d.TypeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiEndpoints_Type");
        });

        modelBuilder.Entity<ApiHeader>(entity =>
        {
            entity.HasKey(e => new { e.AssistantConfigId, e.Key });

            entity.HasIndex(e => e.AssistantConfigId, "idx_ApiHeaders_Assistant");

            entity.Property(e => e.AssistantConfigId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.AssistantConfig).WithMany(p => p.ApiHeaders)
                .HasForeignKey(d => d.AssistantConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiHeaders_AssistantConfigs");
        });

        modelBuilder.Entity<ApiProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__ApiProvi__B54C687D84410D20");

            entity.Property(e => e.ProviderId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BaseUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AssistantConfig>(entity =>
        {
            entity.HasKey(e => e.AssistantId).HasName("PK__Assistan__3756F730973DC97D");

            entity.HasIndex(e => e.ProviderId, "idx_AssistantConfigs_Provider");

            entity.Property(e => e.AssistantId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Instructions).IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProviderId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Temperature)
                .HasDefaultValue(1.00m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TopP)
                .HasDefaultValue(1.00m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Provider).WithMany(p => p.AssistantConfigs)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssistantConfigs_Providers");
        });

        modelBuilder.Entity<AssistantTool>(entity =>
        {
            entity.HasKey(e => new { e.AssistantConfigId, e.ToolCode });

            entity.HasIndex(e => e.ToolCode, "idx_AssistantTools_Tool");

            entity.Property(e => e.AssistantConfigId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ToolCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssistantConfig).WithMany(p => p.AssistantTools)
                .HasForeignKey(d => d.AssistantConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssistantTools_AssistantConfigs");

            entity.HasOne(d => d.ToolCodeNavigation).WithMany(p => p.AssistantTools)
                .HasForeignKey(d => d.ToolCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssistantTools_Tools");
        });

        modelBuilder.Entity<AssistantVectorStore>(entity =>
        {
            entity.HasKey(e => new { e.AssistantConfigId, e.VectorStoreId });

            entity.HasIndex(e => e.VectorStoreId, "idx_AssistantVectorStores_Vector");

            entity.Property(e => e.AssistantConfigId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VectorStoreId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssistantConfig).WithMany(p => p.AssistantVectorStores)
                .HasForeignKey(d => d.AssistantConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssistantVectorStores_AssistantConfigs");

            entity.HasOne(d => d.VectorStore).WithMany(p => p.AssistantVectorStores)
                .HasForeignKey(d => d.VectorStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssistantVectorStores_VectorStores");
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Tools__A25C5AA60D351F0A");

            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeName).HasName("PK__Type__D4E7DFA9997EC1A3");

            entity.ToTable("Type");

            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<VectorStore>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("PK__VectorSt__821FB018F8E0730A");

            entity.Property(e => e.Identifier)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
