using PizzaOrderingApplication.Models;
using System;
using System.Linq;

namespace PizzaOrderingApplication
{
    class Program
    {
        readonly pizzaContext context;
        public int TotalPrice;
        public Program()
        {
            context = new pizzaContext();
            TotalPrice = 0;
        }
        static int GetNumberInput()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Invalid Entry !.enter number again");
            }
            return num;
        }
        static string GetStringInput()
        {
            string input = Console.ReadLine();
            while (input.Length == 0)
            {
                Console.WriteLine("Field cannot be empty!. Enter Again");
                input = Console.ReadLine();
            }
            return input;
        }
        void LoginMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Pizza Ordering System");
                Console.WriteLine("---------------------\n");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit\n");
                Console.WriteLine("Please select a option");
                choice = GetNumberInput();
                switch (choice)
                {
                    case 1:
                        UserLogin();
                        break;
                    case 2:
                        UserRegistration();
                        break;
                    case 3:
                        Console.WriteLine("You entered exit!!");
                        break;
                    default:
                        Console.WriteLine("Invalid option!. Please try Again");
                        LoginMenu();
                        break;
                }
            } while (choice != 3);
        }
        void UserRegistration()
        {
            UserLoginDetail user = new();
            Console.WriteLine("Enter your Name");
            user.UserName = GetStringInput();
            Console.WriteLine("Enter your Email(That considered as UserName)");
            user.UserMail = GetStringInput();
            Console.WriteLine("Enter your Password");
            user.UserPassword = GetStringInput();
            Console.WriteLine("Reenter Password");
            string password = GetStringInput();
            Console.WriteLine("Enter your Address(with door no and street");
            user.UserAddress = GetStringInput();
            Console.WriteLine("Enter your phone");
            user.UserPhone = GetStringInput();
            if (user.UserPassword == password)
            {
                context.UserLoginDetails.Add(user);
                context.SaveChanges();
                Console.WriteLine("\n User Account Created !!\n");
            }
            else
            {
                Console.WriteLine("\nPassword Does not match");
                Console.WriteLine("User Account Not Created");
            }
        }
        void UserLogin()
        {
            string userMail, password;
            Console.WriteLine("Please enter your Mail");
            userMail = GetStringInput();
            Console.WriteLine("Please enter your password");
            password = GetStringInput();
            int userId = 0;
            foreach (var item in context.UserLoginDetails)
            {
                if (item.UserMail == userMail)
                {
                    userId = item.UserId;
                    break;
                }
            }
            if (userId != 0)
            {
                var user = context.UserLoginDetails.Find(userId);
                if (user.UserPassword == password)
                {
                    Console.WriteLine("Login Successful!\n");
                    TakeOrder(userId);
                }
                else
                {
                    Console.WriteLine("Incorrect Password!");
                }
            }
            else
            {
                Console.WriteLine("Email not present in the database");
                Console.WriteLine("Create a new one use Register option");
            }
        }
        void AllPizzaDetails()
        {
            
            Console.WriteLine("The following are the pizza that are available for ordering\n");
            Console.WriteLine("Number\tName\t\tPrice\tType ");
            foreach (var item in context.PizzaDetails)
            {
                Console.WriteLine(item.PizzaNumber+"\t"+item.PizzaName+"\t$"+item.PizzaPrice+"\t"+item.PizzaType);
            }

        }
        void TakeOrder(int userId)
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Welcome to the Online PIZZA");
            Console.WriteLine("---------------------------\n");
            TotalPrice = 0;
            Order order = new();
            order.UserId = userId;
            context.Orders.Add(order);
            context.SaveChanges();
            int orderId = order.OrderId;
            OrderPizza(orderId);
            AskAnotherPizza(orderId);
            OrderSummary(orderId);
        }
        void AskAnotherPizza(int orderId)
        {
            string more;
            Console.WriteLine("Do you want to select another pizza for this order?(y/n)");
            more = GetStringInput();
            if (more == "Y" || more == "y")
            {
                OrderPizza(orderId);
                AskAnotherPizza(orderId);
            }
            else if(more == "n" || more == "N")
            {
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n'");
                AskAnotherPizza(orderId);
            }
        }
        void OrderPizza(int orderId)
        {
            AllPizzaDetails();
            Console.WriteLine("\nEnter the Pizza of your choice");
            int pizzaNumber;
            do
            {
                pizzaNumber = GetNumberInput();
                var pizza = context.PizzaDetails.Find(pizzaNumber);
                if (pizza != null)
                {
                    Console.WriteLine("You have selected "+pizza.PizzaName+" for $"+pizza.PizzaPrice);
                    TotalPrice += (int)pizza.PizzaPrice;
                }
                else
                {
                    Console.WriteLine("Please enter correct pizza number");
                }
            } while (pizzaNumber == 0 || pizzaNumber > 5);
            OrderDetail orderDetail = new();
            orderDetail.OrderId = orderId;
            orderDetail.PizzaNumber = pizzaNumber;
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
            int itemNumber = orderDetail.ItemNumber;
            AskTopping(itemNumber);
           
        }
        void AskTopping(int itemNumber)
        {
            Console.WriteLine("Do u want extra toppings? (y/n)");
            string ans = GetStringInput();
            if (ans == "Y" || ans == "y")
            {
                OrderTopping(itemNumber);
                AskExtraTopping(itemNumber);
            }
            else if (ans == "N" || ans == "n")
            {
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Please select 'y' or 'n'");
                AskTopping(itemNumber);
            }

        }
        void AskExtraTopping(int itemNumber)
        {
            string more;
            Console.WriteLine("Do you want one more toppings?(y/n)");
            more = GetStringInput();
            if (more == "Y" || more == "y")
            {
                OrderTopping(itemNumber);
                AskExtraTopping(itemNumber);
            }
            else if (more == "N" || more == "n")
            {
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n'");
                AskExtraTopping(itemNumber);
            }
        }
        void OrderTopping(int itemNumber)
        {
            PrintAllToppings();
            Console.WriteLine("\nSelect the Topping");
            int toppingNumber;
            do
            {
                toppingNumber = GetNumberInput();
                var topping = context.Toppings.Find(toppingNumber);
                if (topping != null)
                {
                    TotalPrice += (int)topping.ToppingPrice;
                    Console.WriteLine("You have selected " + topping.ToppingName + " for $" + topping.ToppingPrice+" so total $"+TotalPrice);
                }
                else
                {
                    Console.WriteLine("Please enter correct topping number");
                }
            } while (toppingNumber == 0 || toppingNumber > 5);
            OrderItemDetail orderItemDetail = new();
            orderItemDetail.ItemNumber = itemNumber;
            orderItemDetail.ToppingNumber = toppingNumber;
            context.OrderItemDetails.Add(orderItemDetail);
            context.SaveChanges();
        }
        void PrintAllToppings()
        {
            Console.WriteLine("The folowing are the toppings");
            Console.WriteLine("Number\tName\t\tPrice\t");
            foreach (var item in context.Toppings)
            {
                Console.WriteLine(item.ToppingNumber + "\t" + item.ToppingName + "\t\t$" + item.ToppingPrice);
            }
        }
        void OrderSummary(int orderId)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Your Order Summary");
            Console.WriteLine("------------------");
            var items = context.OrderDetails.Where(e => e.OrderId == orderId).ToList();
            int i = 0;
            foreach (var item in items)
            {
                i++;
                int pizzaNumber = (int)item.PizzaNumber;
                int itemNumber = (int)item.ItemNumber;
                Console.WriteLine("\nPizza " + i);
                var pizza = context.PizzaDetails.Find(pizzaNumber);
                Console.WriteLine("\n"+pizza.PizzaNumber + "\t" + pizza.PizzaName + "\t$" + pizza.PizzaPrice + "\t" + pizza.PizzaType);
                var toppings = context.OrderItemDetails.Where(e => e.ItemNumber == itemNumber).ToList();
                Console.WriteLine("\nToppings");
                if (toppings.Count != 0)
                {
                    foreach (var topping in toppings)
                    {
                        int toppingNumber = (int)topping.ToppingNumber;
                        var topp = context.Toppings.Find(toppingNumber);
                        Console.WriteLine("\n" + topp.ToppingNumber + "\t" + topp.ToppingName + "\t" + topp.ToppingPrice);
                    }
                }
                else
                {
                    Console.WriteLine("\nNothing");
                }
            }
            OrderAmount(orderId);
            OrderConfirmation(orderId);
            
        }
        void OrderAmount(int orderId)
        {
            Console.WriteLine("\n--------------------------");
            Console.WriteLine("Total Price $"+TotalPrice);
            int DelivaryCost = 0;
            if (TotalPrice < 25)
            {
                DelivaryCost = 5;
                Console.WriteLine("Delivary Cost $"+DelivaryCost);
            }
            else
            {
                Console.WriteLine("Delivary Cost $" + DelivaryCost);
            }
            Console.WriteLine("--------------------------\n");
            var order = context.Orders.Find(orderId);
            order.TotalAmount = TotalPrice;
            order.DelivaryCharges = DelivaryCost;
            context.Orders.Update(order);
            context.SaveChanges();
        }
        void OrderConfirmation(int orderId)
        {
            var order = context.Orders.Find(orderId);
            Console.WriteLine("\nPlease confirm your order y/n?");
            string confirm = GetStringInput();
            if (confirm == "Y" || confirm == "y")
            {
                order.Status = "Confirmed";
                Console.WriteLine("\nThe Order will be delivered to Address");
                Console.WriteLine("-------------------------");
                int userId = (int)order.UserId;
                var user = context.UserLoginDetails.Find(userId);
                Console.WriteLine(user.UserAddress);
                Console.WriteLine("\nPlease pay on delivery");
                Console.WriteLine("\nThank you!!!!\n");
            }
            else if (confirm == "N" || confirm == "n")
            {
                Console.WriteLine("Are you sure you want to cancel the order 'y'/'n'");
                string finalConfirm = GetStringInput();
                if (finalConfirm == "Y" || finalConfirm == "y")
                {
                    order.Status = "Canceled";
                    Console.WriteLine("Order cancelled");
                }
                else
                    OrderConfirmation(orderId);
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n'");
                OrderConfirmation(orderId);
            }
            context.Orders.Update(order);
            context.SaveChanges();
        }
        static void Main()
        {
            new Program().LoginMenu();
            Console.ReadKey();
        }
    }
}
