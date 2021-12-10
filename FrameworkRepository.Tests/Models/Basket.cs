using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkRepository.Tests.Models
{
    public class Basket
    {
        public int UserId { get; set; }
        
        public int ProductId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
