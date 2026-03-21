namespace NyloStreetwearMVC1.Data.Entities
{
    /// <summary>
    /// ProductCategory entity representing the many-to-many relationship between products and categories, with properties for ProductId, Product, CategoryId, and Category. This join entity allows a product to be associated with multiple categories and a category to be associated with multiple products, facilitating the organization and categorization of products in the store.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product, which serves as a foreign key in the database. This property is of type int and is required for each ProductCategory entity to establish the relationship between a product and a category. It references the Id property of the Product entity, allowing for efficient querying and navigation between products and their associated categories.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the navigation property for the associated Product entity. This property allows for easy access to the details of the product that is linked to a specific category through this ProductCategory join entity. It is marked as non-nullable, indicating that every ProductCategory must be associated with a valid Product entity, ensuring data integrity in the many-to-many relationship between products and categories.
        /// </summary>
        public Product? Product { get; set; } = null!;
        /// <summary>
        /// Gets or sets the unique identifier for the category, which serves as a foreign key in the database. This property is of type int and is required for each ProductCategory entity to establish the relationship between a product and a category. It references the Id property of the Category entity, allowing for efficient querying and navigation between categories and their associated products.
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Gets or sets the navigation property for the associated Category entity. This property allows for easy access to the details of the category that is linked to a specific product through this ProductCategory join entity. It is marked as non-nullable, indicating that every ProductCategory must be associated with a valid Category entity, ensuring data integrity in the many-to-many relationship between products and categories.
        /// </summary>
        public Category? Category { get; set; } = null!;
    }
}
