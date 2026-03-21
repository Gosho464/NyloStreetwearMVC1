namespace NyloStreetwearMVC1.Data.Entities
{
    /// <summary>
    /// Category entity representing a product category in the store, with properties for Id, Name, and a navigation property for its associated products through the ProductCategory join entity.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category, which serves as the primary key in the database. This property is of type int and is required for each category entity to ensure that it can be uniquely identified and referenced in relationships with products.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the category, which is a required field and cannot be null or empty. This property is of type string and represents the name of the category that will be displayed to customers when browsing products. It is important for this property to be descriptive and unique to help customers easily identify and differentiate between different categories in the store.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        // Navigation property for many-to-many relationship with Product
        /// <summary>
        /// Gets or sets the collection of ProductCategory entities that represent the many-to-many relationship between categories and products. This allows a category to be associated with multiple products, and a product to be associated with multiple categories. The collection is initialized to an empty list to avoid null reference issues when adding products to a category.
        /// </summary>
        public ICollection<ProductCategory>? ProductCategories { get; set; } =
            new List<ProductCategory>();
    }
}
