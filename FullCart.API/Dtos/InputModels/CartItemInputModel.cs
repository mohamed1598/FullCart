namespace FullCart.API.Dtos.InputModels
{
    public class CartItemInputModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int CartId { get; set; }
    }
}