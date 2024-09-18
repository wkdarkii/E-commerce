using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Models
{
    public class Main_Data
    {
        public int Id { get; set; }

        public string? SUrun_isim { get; set; }

        public string? Img { get; set; }

        public int Eski_Fiyat { get; set; }

        public int Yeni_Fiyat { get; set; }

        [NotMapped]  // EF tarafından haritalanmaması gerektiğini belirtiyor
        public IFilterMetadata? Filter { get; set; }
    }

}
