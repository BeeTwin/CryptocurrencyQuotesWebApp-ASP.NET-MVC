using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CryptocurrencyQuotesLibrary.Models.DbModels
{
    public class Cryptocurrency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Symbol { get; set; }

        [Required]
        [Column(TypeName = "decimal(30,15)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(30,15)")]
        public decimal PercentChange1h { get; set; }

        [Required]
        [Column(TypeName = "decimal(30,15)")]
        public decimal PercentChange24h { get; set; }

        [Required]
        [Column(TypeName = "decimal(30,15)")]
        public decimal MarketCap { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
