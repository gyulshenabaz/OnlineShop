using System;

// A model representing a product
public class Product
{
    private string name;

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name must be not empty.");
            }
            name = value;
        }
    }

    private string barcode;

    public string Barcode
    {
        get { return barcode; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Barcode must not be empty.");
            }
            barcode = value;
        }
    }
    
    private double price;

    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price must be positive.");
            }
            price = value;
        }
    }
    
    public Product(string name, string barcode, double price)
    {
        this.Name = name;
        this.Barcode = barcode;
        this.Price = price;
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode() + Barcode.GetHashCode();
    }
    
    public override bool Equals(object? obj)
    {
        var other = obj as Product;
        if (other == null) return false;

        return this.Name.Equals(other.Name) && this.Barcode.Equals(other.Barcode);
    }

    public override string ToString()
    {
        return $"Product {this.Name}";
    }
}