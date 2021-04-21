using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Primer1.Models
{
    [Table("Automobil")]
    public class Automobil
    {
        public int AutomobilId { get; set; }

        [Required(ErrorMessage = "Unesite marku")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Marka")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Unesite model")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Unesite godiste automobila")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:M/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Godiste")]
        public DateTime Godiste { get; set; }

        [Required(ErrorMessage = "Unesite zapreminu motora")]
        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "ZapreminaMotora")]
        public decimal ZapreminaMotora { get; set; }

        [Required(ErrorMessage = "Unesite snagu automobila")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Snaga")]
        public string Snaga { get; set; }

        [Required(ErrorMessage = "Unesite gorivo automobila")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Gorivo")]
        public string Gorivo { get; set; }

        [Required(ErrorMessage = "Unesite karoseriju automobila")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Karoserija")]

        public string Karoserija { get; set; }

        [Display(Name = "Fotografija")]
        [MaxLength]
        public byte[] FajlSlike
        {
            get; set;
        }

        [StringLength(20)]
        public string TipFajla
        {
            get; set;
        }

        [Required(ErrorMessage = "Unesite opis")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Opis")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Unesite cenu")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena")]
        public decimal Cena { get; set; }
        [Required(ErrorMessage = "Unesite broj telefona")]
        [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
        [Display(Name = "Kontakt")]
        public string Kontakt { get; set; }

    }
}
