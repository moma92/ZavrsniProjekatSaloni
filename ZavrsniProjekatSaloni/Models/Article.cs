//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZavrsniProjekatSaloni.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.Purchases = new HashSet<Purchase>();
        }
    
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Sifra")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Ime artikla")]
        public string ArticleName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Zemlja proizvodnje")]
        public string ProductionCountry { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Godina proizvodnje")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Cena u RSD")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Kolicina u skladistu")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Salon")]
        public int SalonId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Kategorija")]
        public int CategoryId { get; set; }


        [Display(Name = "Slika")]
        public byte[] Image { get; set; } 
    
        public virtual Category Category { get; set; }
        public virtual Salon Salon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
