using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.Data;

public partial class NotedbContext : DbContext
{
    public NotedbContext()
    {
    }

    public NotedbContext(DbContextOptions<NotedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }
    public virtual DbSet<User> Users { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NOTES__3214EC27B2507350");
            entity.ToTable("NOTES");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .HasColumnName("SUBJECT");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("TEXT");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTES_TO_USERS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC27BC05B398");
            entity.ToTable("USERS");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(512)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
