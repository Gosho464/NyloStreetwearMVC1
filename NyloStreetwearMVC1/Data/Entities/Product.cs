namespace NyloStreetwearMVC1.Data.Entities
{
    /// <summary>
    /// Product entity representing an item in the store, with properties for Id, Title, Description, Price, ImageUrl, and a navigation property for its categories.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product, which serves as the primary key in the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the product, which is a required field and cannot be null or empty. It represents the name of the product that will be displayed to customers.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product, which provides additional details about the product. This field is optional and can be left empty if no description is provided.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product, which is a decimal value representing the cost of the product. This field is required and should be a positive number.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>   
        /// Gets or sets the URL of the product's image, which is a string representing the location of the product's image. This field is optional and can be left empty if no image is provided.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        // Navigation property for many-to-many relationship with Category

        /// <summary>
        /// Gets or sets the collection of ProductCategory entities that represent the many-to-many relationship between products and categories. This allows a product to be associated with multiple categories, and a category to be associated with multiple products. The collection is initialized to an empty list to avoid null reference issues when adding categories to a product.
        /// </summary>
        public ICollection<ProductCategory>? ProductCategories { get; set; } =
            new List<ProductCategory>();
    }
}
