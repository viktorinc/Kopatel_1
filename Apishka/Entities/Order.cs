using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Location { get; set; }

        [ForeignKey("KladmenOf")]
        public int KladmenId { get; set; }

        [ForeignKey("UserOf")]
        public int UserId { get; set; }

        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }

        public virtual Kladman KladmanOf { get; set; }
        public virtual User UserOf { get; set; }
        public virtual Product ProductOf { get; set; }
    }
}
