using System.Collections.Generic;


namespace Order_CLI.Models
{
    class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public Product(string name)
        {
            Name = name;
        }
    }
}
