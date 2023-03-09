#pragma warning disable IDE0073

using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkRepository.Tests.Models
{
    internal class Basket
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }


        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
