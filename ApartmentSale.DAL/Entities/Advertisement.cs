namespace ApartmentSale.DAL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Advertisement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Advertisement()
        {
            InterestedUsers = new HashSet<User>();
        }

        [Key]
        public int AdvertisementId { get; set; }
        // Review OB: This field and several others are not required, but i must specify them in UI app.
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Square { get; set; }

        [Required]
        public string Adress { get; set; }

        public int RoomsCount { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> InterestedUsers { get; set; }
    }
}
