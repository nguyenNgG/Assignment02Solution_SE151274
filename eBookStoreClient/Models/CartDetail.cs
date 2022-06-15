using BusinessObject;

namespace eBookStoreClient.Models
{
    public class CartDetail
    {
        public Author Author { get; set; }
        public int AuthorOrder { get; set; }
        public decimal RoyaltyPercentage { get; set; }
    }
}
