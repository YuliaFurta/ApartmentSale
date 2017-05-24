namespace ApartmentSale.DAL
{
    // Review OB: This using and several others are unnecessary.
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AppSaleContext : DbContext
    {
        public AppSaleContext()
            : base("name=AppSaleContext")
        {
        }

        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .HasMany(e => e.InterestedUsers)
                .WithMany(e => e.LikeAdvertisements)
                .Map(m => m.ToTable("UserAdvertisement").MapLeftKey("AdvertisementId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SurName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MyAdvertisements)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}