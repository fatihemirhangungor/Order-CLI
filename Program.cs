using Order_CLI.Models;
using System;
using System.Collections.Generic;

namespace Order_CLI
{
    class Program
    {
        /*
         
        Some English - Turkish Words : 

                    Basket - Sepet

                    Purchase - Satın Almak
         
        */

        //Products to purchase
        static List<Product> products = new() { new Product("Apple"), new Product("Banana"), new Product("Orange") };
        
        //User basket
        static List<Product> basket = new List<Product>();

        //User Inventory
        static List<Product> inventory = new List<Product>();

        //List of users
        static List<Person> people = new List<Person>() { new Person("12345", "Arvato", "Supply Chain Solutions", "1234567890", basket) };

        //Boolean value to control existance of the user
        static bool userExist = false;

        //Main method
        static void Main(string[] args)
        {
            //Welcome Message
            Console.Write("Welcome to Shopping, Please enter your id : ");

            //Read user id
            string Tc = Console.ReadLine();

            //Check if user is exist
            foreach(Person person in people)
            {
                //Check if "people" list contains user's TC
                if (person.Tc == Tc)
                {
                    //If exist, assign "userExist" == true
                    userExist = true;

                    //Welcome User
                    Console.WriteLine("--------------------------------------------------------------------\n");
                    Console.WriteLine("Welcome " + person.Name + " " + person.Surname + "\n");
                    Console.WriteLine("--------------------------------------------------------------------\n");
                }
            }

            //If user is not exist, then create one
            if (!userExist)
            {
                //Create a user
                Person newPerson = CreateUser();

                //Set userExist to true, because we just created one
                userExist = true;

                //Welcome the user
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("Your account successfully created, Welcome to shopping " + newPerson.Name + " " + newPerson.Surname + "\n");
                Console.WriteLine("--------------------------------------------------------------------\n");
            }

            //Inform the user about operations
            Console.WriteLine("Select the operation you want to perform : \n");

            //Endless loop for shopping until the person quits
            while (true)
            {
                //Listing operations
                Console.WriteLine("1 - Buy Product\n" +
                                  "2 - List Inventory\n" +
                                  "3 - Quit");

                //Get user's input
                string option = Console.ReadLine();

                //Switch statement for user's input
                switch (option)
                {
                    //Buy Product case
                    case "1":

                        //Informing the user
                        Console.WriteLine("Select the products you want to buy, products will be added to your basket.\n\n" +
                                          "Press 'b' to buy all items in your basket\n" +
                                          "Press 'r' to remove item from your basket\n" +
                                          "Press 'l' to see your basket\n" +
                                          "Press 'q' to leave your basket\n");

                        //Infinite loop to add products into basket, until user presses "q"
                        while (true)
                        {
                            //Print products in products list
                            Console.WriteLine("--------------------------------------------------------------------\n");
                            Console.WriteLine("Products : \n");
                            for (int i = 0; i < products.Count; i++) { Console.WriteLine((i+1) + " - " + products[i].Name + "\n"); }
                            Console.WriteLine("--------------------------------------------------------------------\n");

                            //Reading user's input
                            string product_option = Console.ReadLine();

                            //Leave basket if input == 'q'
                            if (product_option == "q") 
                            {
                                break;
                            }

                            //Buy products in basket
                            else if (product_option == "b") 
                            {
                                //Control if basket is empty
                                if (basket.Count < 1)
                                {
                                    Console.WriteLine("--------------------------------------------------------------------\n");
                                    Console.WriteLine("Your basket is empty, add some products\n");
                                    Console.WriteLine("--------------------------------------------------------------------\n");

                                    //Go to beginning of this loop
                                    continue;
                                }

                                //Basket is not empty
                                else
                                {
                                    //Buy everything in the basket and add it to the user's inventory
                                    Purchase();

                                    //Break this while loop to go back the beginning
                                    break;
                                }
                            }

                            //Remove product from basket
                            else if (product_option == "r")
                            {
                                //Control if basket is empty
                                if (basket.Count < 1)
                                {
                                    Console.WriteLine("--------------------------------------------------------------------\n");
                                    Console.WriteLine("Your basket is empty, there is nothing to remove, add some products\n");
                                    Console.WriteLine("--------------------------------------------------------------------\n");

                                    //Go to beginning of this while loop
                                    continue;
                                }

                                //Basket is not empty
                                else
                                {
                                    //Remove the item
                                    Remove_Item();

                                    //Go to beginning of this while loop
                                    continue;
                                }
                            }

                            //Print products in the Basket
                            else if (product_option == "l")
                            {
                                //Print Basket
                                Print_Basket();

                                //Go to beginning of this while loop
                                continue;
                            }

                            //If user chose any product to buy (user chose 1-2-3), continue from here
                            else
                            {
                                //So, user just selected a product to buy, then we ask the quantity
                                Console.Write("Quantity : ");
                                int quantity_option = Convert.ToInt32(Console.ReadLine());

                                //We got the product and it's quantity, send those as parameter and add the product into basket
                                Add_To_Basket(product_option, quantity_option);
                            }
                        }

                        break;


                    case "2":

                        //Print items in the inventory
                        Print_Inventory();

                        break;

                    case "3":

                        //If user selects this case, the application will close
                        //Print a GoodBye message
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        Console.WriteLine("Thank you for shopping, have a nice day!\n");
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        return;
                    
                    default:

                        //If user enters invalid input
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        Console.WriteLine("Invalid operation\n");
                        Console.WriteLine("--------------------------------------------------------------------\n");
                        break;
                }
            }
        }

        static void Remove_Item()
        {
            //Print basket
            Console.WriteLine("Select the product you want to remove : \n");
            Console.WriteLine("--------------------------------------------------------------------\n");
            for (int i = 0; i < basket.Count; i++) { Console.WriteLine((i + 1) + " - " + basket[i].Name + "\n"); }
            Console.WriteLine("--------------------------------------------------------------------\n");

            //Read user's input
            string remove_option = Console.ReadLine();

            //Give feedback that the item user selected is removed from the basket
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("Following item removed from the basket : " + basket[int.Parse(remove_option) - 1].Name + "\n");
            Console.WriteLine("--------------------------------------------------------------------\n");

            //Remove the product user selected
            basket.RemoveAt(int.Parse(remove_option) - 1);
        }

        static void Print_Basket()
        {
            //Check first if the basket is empty
            if (basket.Count < 1)
            {
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("Your basket is empty, add some products\n");
                Console.WriteLine("--------------------------------------------------------------------\n");
            }

            //Basket is not empty
            else
            {
                //Print products in the Basket
                Console.WriteLine("Your basket has the following items : ");
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("    Product\t\tQuantity\n");
                for (int i = 0; i < basket.Count; i++) { Console.WriteLine((i + 1) + " - " + basket[i].Name + "\t\t" + basket[i].Quantity + "\n"); }
                Console.WriteLine("--------------------------------------------------------------------\n");
            }
        }

        static void Add_To_Basket(string product_option, int quantity_option)
        {
            //Create a new product with given inputs and add it to the basket
            Product newProduct = new Product(products[int.Parse(product_option) - 1].Name, quantity_option);
            basket.Add(newProduct);

            //Give feedback about the product user bought
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("\t" + newProduct.Quantity + " " + newProduct.Name + " successfully added to your basket" + "\n");
            Console.WriteLine("--------------------------------------------------------------------\n");

            //Inform user again
            Console.WriteLine("     Buy - 'b'       Remove - 'r'       Basket - 'l'       Leave - 'q'\n");

            //Show the basket for more info
            Print_Basket();

            //Console.WriteLine("--------------------------------------------------------------------\n");
            //Console.WriteLine("Your current basket has the following products : \n");
            //for (int i = 0; i < basket.Count; i++) { Console.WriteLine((i + 1) + " - " + basket[i].Name + "\n"); }
            //Console.WriteLine("--------------------------------------------------------------------\n");
        }

        static void Print_Inventory()
        {
            //If user's basket is empty, give feedback that it is empty
            if (inventory.Count < 1)
            {
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("Your inventory is empty, add some products\n");
                Console.WriteLine("--------------------------------------------------------------------\n");
            }

            //Inventory is not empty, list the items in inventory
            else
            {
                Console.WriteLine("Your inventory has the following items : \n");
                Console.WriteLine("--------------------------------------------------------------------\n");
                Console.WriteLine("    Product\t\tQuantity\n");
                for (int i = 0; i < inventory.Count; i++) { Console.WriteLine((i + 1) + " - " + inventory[i].Name + "\t\t" + inventory[i].Quantity + "\n"); }
                Console.WriteLine("--------------------------------------------------------------------\n");
            }
        }

        static void Purchase()
        {
            //Buy all the products in the basket and add it to the user's inventory
            inventory.AddRange(basket);

            //Give feedback to user that purchase is successfull
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("Purchase is successfull, following products added to your inventory : \n");
            Console.WriteLine("--------------------------------------------------------------------\n");
            Console.WriteLine("    Product\t\tQuantity\n");
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine((i + 1) + " - " + inventory[i].Name + "\t\t" + inventory[i].Quantity + "\n");
            }
            Console.WriteLine("--------------------------------------------------------------------\n");

            //Empty basket
            basket.Clear();
        }

        static Person CreateUser()
        {
            Console.WriteLine("Oops! Looks like you don't have an account. Please create one.");
            Console.Write("Tc : ");
            string tc = Console.ReadLine();
            while (true)
            {
                if (TcControl(tc) == false)
                {
                    tc = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            Console.Write("Name : ");
            string name = Console.ReadLine();
            Console.Write("Surname : ");
            string surname = Console.ReadLine();
            Console.Write("Phone : ");
            string phone = Console.ReadLine();
            List<Product> personal_products = new List<Product>();
            Person newPerson = new Person(tc, name, surname, phone, personal_products);
            people.Add(newPerson);
            return newPerson;
        }

        static bool TcControl(string tc)
        {
            if (tc.Length != 5)
            {
                Console.WriteLine("Tc must be 5 characters");
                return false;
            }
            return true;
        }
    }
}
