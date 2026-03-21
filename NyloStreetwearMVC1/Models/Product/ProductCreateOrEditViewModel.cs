namespace NyloStreetwearMVC1.Models.Product
{
    public class ProductCreateOrEditViewModel
    {
        public int Id { get; set; } // For Edit, will be 0 for Create
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();   
    }
}
