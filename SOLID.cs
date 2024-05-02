////////////////////////////////////////////////////////////////////////////////////////////////
// SRP Violation
////////////////////////////////////////////////////////////////////////////////////////////////

// The ECommerceSystem class is violating the Single Responsibility Principle (SRP) by
// handling multiple responsibilities such as managing products, placing orders, processing
// payments, and sending order confirmation emails within the same class.



public class ECommerceSystem
{
    // Products and orders are being managed within the same class, which violates SRP.

    // AddProduct method is responsible for adding products to the system, which is
    // a separate concern from managing orders and payments.

    // PlaceOrder method is responsible for placing orders, processing payments,
    // and sending order confirmation emails, which are separate responsibilities.

    // ProcessCreditCardPayment and ProcessPayPalPayment methods are responsible for
    // processing payments, which is a separate concern from managing products and orders.

    // SendOrderConfirmationEmail method is responsible for sending order confirmation emails,
    // which is a separate concern from managing products and orders.

    // Overall, the ECommerceSystem class has multiple reasons to change, violating the
    // Single Responsibility Principle (SRP).
    private List<Product> products = new List<Product>();
    private List<Order> orders = new List<Order>();
    public void AddProduct(string name, decimal price, int quantity)
        {
            products.Add(new Product { Name = name, Price = price, Quantity =
            quantity });
        }
    public void PlaceOrder(string customerName, List<int> productIds, string
    paymentMethod)
        {
            decimal totalCost = 0;
            List<Product> orderedProducts = new List<Product>();
            foreach (int productId in productIds)
        {
        Product product = products.Find(p => p.Id == productId);
        if (product != null && product.Quantity > 0)
            {
                orderedProducts.Add(product);
                totalCost += product.Price;
                product.Quantity--;
            }
        }
    if (orderedProducts.Count > 0)
        {
            if (paymentMethod == "CreditCard")
                {
                    ProcessCreditCardPayment(totalCost);
                }
            else if (paymentMethod == "PayPal")
                {
                    ProcessPayPalPayment(totalCost);
                }
                    Order order = new Order
                {
                    CustomerName = customerName,
                    Products = orderedProducts,
                    TotalCost = totalCost
                };
            orders.Add(order);
            SendOrderConfirmationEmail(order);
        }
    }
    private void ProcessCreditCardPayment(decimal amount)
    {
        // Process credit card payment
        Console.WriteLine($"Processing credit card payment of ${amount}");
    }
    private void ProcessPayPalPayment(decimal amount)
    {
        // Process PayPal payment
        Console.WriteLine($"Processing PayPal payment of ${amount}");
    }
    private void SendOrderConfirmationEmail(Order order)
    {
        string message = $"Order confirmation for {order.CustomerName}:\n";
        message += $"Total Cost: ${order.TotalCost}\n";
        message += "Products:\n";
        foreach (Product product in order.Products)
            {
            message += $"- {product.Name} (${product.Price})\n";
            }
        // Send email
        Console.WriteLine(message);
    }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
public class Order
{
    public string CustomerName { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalCost { get; set; }
}









////////////////////////////////////////////////////////////////////////////////////////////////
//  Identification and Resolving
////////////////////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////////////////////
//Identification:
////////////////////////////////////////////////////////////////////////////////////////////////

// The code contains a class named ECommerceSystem that handles multiple responsibilities
// such as managing products, placing orders, processing payments, and sending order
// confirmation emails.

// The class contains methods like AddProduct and PlaceOrder that handle different
// concerns within the same class.




////////////////////////////////////////////////////////////////////////////////////////////////
//  Resolving:
////////////////////////////////////////////////////////////////////////////////////////////////


// To resolve the SRP violation, we need to refactor the code to separate the different
// responsibilities into separate classes or components.

// Identify the distinct responsibilities such as managing products, handling orders,
// processing payments, and sending notifications.

// Create separate classes/interfaces for each responsibility and refactor the code
// to delegate the responsibilities to these classes/interfaces.

// Use dependency injection to inject dependencies into the classes where necessary,
// allowing for better modularity, testability, and maintainability.

// Following these steps will result in a more maintainable and loosely coupled system
// that adheres to the Single Responsibility Principle (SRP).





////////////////////////////////////////////////////////////////////////////////////////////////
//  Models:
////////////////////////////////////////////////////////////////////////////////////////////////


// Define the Product and Order classes to represent product and order entities.

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Order
{
    public string CustomerName { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalCost { get; set; }
}

public class Notification : INotification
{
    void SendOrderConfirmationEmail(Order order)
    {
            string message = $"Order confirmation for {order.CustomerName}:\n";
            message += $"Total Cost: ${order.TotalCost}\n";
            message += "Products:\n";
            foreach (Product product in order.Products)
                {
                message += $"- {product.Name} (${product.Price})\n";
                }
            // Send email
            Console.WriteLine(message);
        }
}



////////////////////////////////////////////////////////////////////////////////////////////////
//  Interfaces:
////////////////////////////////////////////////////////////////////////////////////////////////

// Define interfaces for managing products, orders, payments, and notifications.

// Use interfaces to define contracts for each responsibility, enabling loose coupling
// and easier substitution of implementations.


public interface IUnitOfWork
{
}

public interface IProductRepositpry
{
    public void AddProduct(string name, decimal price, int quantity);
    public void GetProductById(int id)
    
}

public interface IOrderRepositpry
{
     public void PlaceOrder(string customerName, List<int> productIds, string
    paymentMethod);
}


public interface IPaymentMethod
{
    public void ProcessPayment(decimal amount);
}

public interface INotification 
{
     void SendOrderConfirmationEmail(Order order);
        
}
////////////////////////////////////////////////////////////////////////////////////////////////
//  Payment Methods classes:
////////////////////////////////////////////////////////////////////////////////////////////////

// Define separate classes for processing credit card and PayPal payments,
// each implementing the IPaymentMethod interface.



public class CreditCard : IPaymentMethod
{
    public void ProcessPayment(decimal amount);
        {
            // Process credit card payment
            Console.WriteLine( $"Processing credit card payment of ${amount}" );
        }
}

public class PayPal : IPaymentMethod
{
    public void ProcessPayment(decimal amount);
    {
            // Process PayPal payment
            Console.WriteLine($"Processing PayPal payment of ${amount}");
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////
//  UnitOfWork:
////////////////////////////////////////////////////////////////////////////////////////////////

// Define a UnitOfWork class to manage the interaction between different repositories
// and to track changes made to entities.

public class UnitOfWork : IUnitOfWork
{
    private List<Product> _products ;
    private List<Order> _orders ;

    public List<Product> Products { get()
            {
               return _products ?? _products= new List<Product>();
            }
    }

    public List<Order> Orders { get()
            {
               return _orders ?? _orders= new List<Order>();
            }
    }
}


////////////////////////////////////////////////////////////////////////////////////////////////
//  Repositpries:
////////////////////////////////////////////////////////////////////////////////////////////////

// Create separate repositories for managing products and orders, each responsible
// for interacting with the data store and performing CRUD operations on entities.

// Use dependency injection to inject dependencies such as the UnitOfWork and other
// necessary services into the repositories.

// Refactor the PlaceOrder method to delegate payment processing and notification
// sending to separate services, adhering to the Single Responsibility Principle (SRP).

// Use dependency injection to inject dependencies such as the PaymentService and
// Notification service into the OrderRepository.

// Refactor the SendOrderConfirmationEmail method to be part of a separate Notification
// service that handles sending notifications, adhering to the Single Responsibility
// Principle (SRP).

// Use dependency injection to inject the Notification service into the OrderRepository.

public class PaymentService
{
    private IPaymentMethod _paymentMethod;

    public PaymentService (IPaymentMethod paymentMethod)
    {
        this.paymentMethod = _paymentMethod;
    }

    public void PaymentProcess (decimal amount)
    {
        this._paymentMethod.ProcessPayment(amount);
    }
}

public class ProductRepositpry : IProductRepositpry
{
    IUnitOfWork _unitOfWork ;
    public ProductRepositpry(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    
    public void AddProduct(string name, decimal price, int quantity)
    {
            _unitOfWork.Products.Add(new Product { Name = name, Price = price, Quantity =
            quantity });
    }
    public void GetProductById(int id)
    {
        _unitOfWork.Products.FirstOrDefault(p => p.Id == productId);
    }

}

public class OrderRepositpry : IOrderRepositpry
{
    IUnitOfWork _unitOfWork ;
    IProductRepositpry _productRepositpry;
    PaymentService _paymentService ;
    INotification _notification ;
    public OrderRepositpry(IUnitOfWork unitOfWork ,IProductRepositpry productRepositpry,
    PaymentService paymentService , INotification notification )
    {
        this._unitOfWork = unitOfWork;
        this._productRepositpry= productRepositpry;
        this._paymentService = paymentService;
        this._notification = notification;
    }
     public void PlaceOrder(string customerName, List<int> productIds, string paymentMethod)
        {
            decimal totalCost = 0;
            List<Product> orderedProducts = new List<Product>();
            foreach (int productId in productIds)
                {
                    Product product = productRepositpry.GetProductById(productId);

                        if (product != null && product.Quantity > 0)
                            {
                                orderedProducts.Add(product);
                                totalCost += product.Price;
                                product.Quantity--;
                            }
                }
            if (orderedProducts.Count > 0)
                {
                    PaymentService.PaymentProcess(totalCost);
                    Order order = new Order
                        {
                            CustomerName = customerName,
                            Products = orderedProducts,
                            TotalCost = totalCost
                        };
                    _unitOfWork.Orders.Add(order);
                    SendOrderConfirmationEmail(order);
                }
    }
}