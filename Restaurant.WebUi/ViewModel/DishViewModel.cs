namespace Restaurant.WebUi.ViewModel
{
    public class DishViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    public bool IsSignature { get; set; }
    public List<IngredientViewModel> Ingredients { get; set; }
}

}
