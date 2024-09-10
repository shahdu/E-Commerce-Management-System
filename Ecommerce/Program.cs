
public record Product(string Id, string Name, string Category, double Price, bool IsAvailable);
public record Customer(string Id, string Name, string Email);
public record Order(string OrderId, string CustmerId, string CustomerNamae, DateTime Date, List<Product> NameAndPriceProduct, double totalPrice);

class ECommerceManager
{

    List<Product> productList = new List<Product>();
    List<Customer> customerList = new List<Customer>();
    List<Order> orderList = new List<Order>();

    public void AddProduct(Product product)
    {
        productList.Add(product);
    }
    public void RemoveProduct(Product product)
    {
        productList.Remove(product);
    }

    public void DisplayProduct()
    {
        foreach (var item in productList)
        {
            Console.WriteLine($"{item}");

        }
    }
    public void SearchProductByName(string name)
    {
        Product foundProduct = productList.FirstOrDefault(product => product.Name == name);
        Console.WriteLine($"{foundProduct}");
    }
    public void SearchProductByCategory(string category)
    {
        Product foundProduct = productList.FirstOrDefault(product => product.Category == category);
        Console.WriteLine($"{foundProduct}");

    }
    public void SearchProductByPrice(double price)
    {
        Product foundProduct = productList.FirstOrDefault(product => product.Price == price);

        Console.WriteLine($"{foundProduct}");
    }

    public void AddCustomer(Customer customer)
    {
        customerList.Add(customer);
    }

    public void RemoveCustomer(Customer customer)
    {
        customerList.Remove(customer);
    }
    public void SearchCustomerByName(string name)
    {
        Customer foundCustomer = customerList.FirstOrDefault(customer => customer.Name == name);
        Console.WriteLine($"{foundCustomer}");
    }

    public void PlaceOrder(string customerId, params string[] productIds)
    {

        Random random = new Random();
        int randomId = random.Next(1, 101);
        string randomIdString = randomId.ToString();
        double totalPrice = 0;
        List<Product> allProdect = new List<Product>();
        Customer customerInList = customerList.FirstOrDefault(customer => customer.Id == customerId);
        if (customerInList != null)
        {

            foreach (var item in productIds)
            {
                Product prudectInList = productList.FirstOrDefault(product => product.Id == item);
                if (prudectInList.IsAvailable)
                {
                    totalPrice += prudectInList.Price;

                    Product updateProduct = prudectInList with { IsAvailable = false };
                    productList.Remove(prudectInList);
                    productList.Add(updateProduct);
                    allProdect.Add(prudectInList);
                }
                else
                {
                    Console.WriteLine($"the product not Available");
                }
            }
            Order order = new Order(randomIdString, customerInList.Id, customerInList.Name, DateTime.Now, allProdect, totalPrice);
            orderList.Add(order);
        }
        else
        {
            Console.WriteLine($"the Custmer is not in te list , you must added first ");

        }
    }

    public void DisplayOrders()
    {
        Console.WriteLine($"Displaying all orders:");
        foreach (var item in orderList)
        {
            Console.WriteLine($"Order ID: {item.OrderId} Customer: {item.CustomerNamae} Total:  {item.totalPrice}, Date :{item.Date} ");
            foreach (var i in item.NameAndPriceProduct)
            {
                Console.WriteLine($"{i.Name}({i.Price})");

            }
        }
    }

}

class App
{
    public static void Main(string[] args)
    {

        ECommerceManager e = new ECommerceManager();
        Product product1 = new Product("P001", "Laptop", "Electronics", 999.99, true);
        Product product2 = new Product("P002", "Smartphone", "Electronics", 699.99, true);
        Product product3 = new Product("P003", "Tablet", "Electronics", 399.99, true);
        Product product4 = new Product("P004", "Headphones", "Accessories", 149.99, true);
        Product product5 = new Product("P005", "Smartwatch", "Accessories", 199.99, true);
        Product product6 = new Product("P006", "Coffee Maker", "Appliances", 89.99, true);
        Product product7 = new Product("P007", "Blender", "Appliances", 59.99, true);
        Product product8 = new Product("P008", "Toaster", "Appliances", 29.99, false);
        Product product9 = new Product("P009", "Office Chair", "Furniture", 129.99, true);
        Product product10 = new Product("P010", "Desk Lamp", "Furniture", 39.99, true);

        Customer c1 = new Customer("1111", "shahd", "shahadjdd@gmail.com");
        Customer c2 = new Customer("22222", "nora", "nora@gmail.com");

        e.AddProduct(product1);
        e.AddProduct(product2);
        e.AddProduct(product3);
        e.AddProduct(product4);
        e.AddProduct(product5);
        e.AddProduct(product6);
        e.AddProduct(product7);
        e.AddProduct(product8);
        e.AddProduct(product9);
        e.AddProduct(product10);

        e.AddCustomer(c1);
        e.AddCustomer(c2);

        e.DisplayProduct();

        e.PlaceOrder(c1.Id, product1.Id, product2.Id);// Order for Shahd with Laptop and Smartphone
        e.PlaceOrder(c2.Id, product3.Id, product4.Id); // Order for Nora with Tablet and Headphones
        e.DisplayOrders();


    }
}
