namespace ShoppingCartAPI.ResponseModels
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }


    }
}
