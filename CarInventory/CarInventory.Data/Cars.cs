using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInventory.Data
{
    public class Cars: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get => base.Id; set => base.Id = value; }

        [Required]
        [MaxLength(250)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(250)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool New { get; set; }
    }
}
