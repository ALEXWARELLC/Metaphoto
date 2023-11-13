using System;

namespace Altex.Licensing;

public class ProductManager
{
    public static readonly ProductManager Instance;

    static ProductManager()
    {
        Instance = new ProductManager();
    }

    public string GetProduct(ProductID Product)
    {
        switch (Product)
        {
            case ProductID.None:
                return "Unknown";
            case ProductID.Metaphoto:
                return "Metaphoto";
            default:
                return "Unknown";
        }
    }
}

public enum ProductID
{
    None = 0,
    Metaphoto = 1
}