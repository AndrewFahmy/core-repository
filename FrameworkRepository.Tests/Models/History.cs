using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace FrameworkRepository.Tests.Models
{
    public class History
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public DateTime CreationDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
