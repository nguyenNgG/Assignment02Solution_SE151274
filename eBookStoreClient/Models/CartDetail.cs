using BusinessObject;
using System.ComponentModel.DataAnnotations;

namespace eBookStoreClient.Models
{
    public class CartDetail
    {
        public Author Author { get; set; }

        [Required]
        public int AuthorOrder { get; set; }

        [Required]
        public decimal RoyaltyPercentage { get; set; }
    }
}
