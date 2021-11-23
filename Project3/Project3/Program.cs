using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

//Program by Tyler Ryan
namespace Project3
{
    class Program
    {
        static void Main(string[] args)
        {
            //declaring and intializing variables and the Orders list
            List<Orders> order = new List<Orders>();
            List<Menu> menu = new List<Menu>();
            List<String> lines = new List<String>();
            int admin;
            int edit;
            string burgerMessage;
            string fryMessage;
            string shakeMessage;
            string packageMessage;
            string userLoop = "Y";

            int frontend = loginMethod();

            while (userLoop == "Y")
            {
                if (frontend == 1)
                {
                    WriteLine("Are you updating an existing item or adding a new one? Press 1 for editing, 2 for adding, and 3 to exit");
                    admin = Convert.ToInt32(ReadLine());
                    if (admin == 1)
                    {
                        administratorUpdate(ref menu);
                    }
                    else if (admin == 2)
                    {
                        administratorAdd(ref menu);
                    }
                    else
                    {
                        WriteLine("Exiting Program...");
                        break;
                    }
                }
                else
                {
                    orderMethod(ref order);
                    WriteLine("Is there another person in your party that wishes to order? 'Y' for yes and 'N' for no");
                    userLoop = ReadLine();
                    while (userLoop != "N" && userLoop != "Y")
                    {
                        WriteLine("That was an incorrect input, please input 'Y' to place another order or 'N' if you are done with your order");
                        userLoop = ReadLine();
                    }
                   
                }
            }
            try
            {
                using (TextWriter tw = new StreamWriter("SavedList.txt"))
                {
                    foreach (Orders s in order)
                        tw.WriteLine("\nName: " + s.customerName + ", Burgers: " + s.burger + ", Fries: " + s.fries 
                            + ", Shakes: " + s.shakes + ", Package: " + s.package);
                }
            }
            catch (Exception i)
            {
                WriteLine("Whoops that was a mistake", i);
            }


            WriteLine("Is the order correct? 1 for yes and 2 for no");
            edit = Convert.ToInt32(ReadLine());

            if(edit == 2)
            {
                administartorOverride(ref order);
            }
            
            //cycling through each of the orders that have been givin, allow the corresponding message to display along with the amount they ordered and their total cost
            foreach (Orders i in order)
            {
                if (i.burger == 1)
                    burgerMessage = "Burger";
                else
                    burgerMessage = "Burgers";

                if (i.fries == 1)
                    fryMessage = "Fry";
                else
                    fryMessage = "Fries";

                if (i.shakes == 1)
                    shakeMessage = "Shake";
                else
                    shakeMessage = "Shakes";

                if (i.package == 1)
                    packageMessage = "Full Package";
                else
                    packageMessage = "Full Packages";


                WriteLine($"\n{i.customerName} has ordered: {i.burger} {burgerMessage} {i.fries} {fryMessage} {i.shakes} {shakeMessage} {i.package} {packageMessage}" +
                    $"\n{i.customerName} has a total of which includes tax: ${i.total.ToString("0.00")}");
            }

            ReadKey();
        }

        static void administratorUpdate(ref List<Menu> n)
        {
            string userInput;
            string newPrice;
            List<String> lines = new List<String>();
            string location = @"C:\Users\Tyler\Desktop\School\Fall2021\ITP136\ITP136WK15Project3\Project3\Project3\bin\Debug\Menu.csv";
            var menuList = File.ReadLines("Menu.csv").Select(line => new Menu(line)).ToList();

            WriteLine("{0,1}{1,10}{2,8}", "ID", "NAME", "PRICE");
            foreach (Menu x in menuList)
            {
                WriteLine("{0,5}", $"{x.menuID}\t{x.menuName}\t{x.menuPrice}");
            }
            WriteLine("Which ID would you like to update the price of?");
            userInput = ReadLine();
            try
            {
                //cycle thorugh the csv and find the intended id location to edit the pricing on the desired item
                if (File.Exists(location))
                {
                    using (StreamReader reader = new StreamReader(location))
                    {
                        String line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(","))
                            {
                                String[] split = line.Split(',');

                                if (split[0].Contains(userInput))
                                {
                                    WriteLine("Please enter the new price:");
                                    newPrice = ReadLine();
                                    split[2] = newPrice;
                                    line = String.Join(",", split);
                                }
                            }
                            lines.Add(line);
                        }
                    }
                    using (StreamWriter writer = new StreamWriter(location, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
            }
            catch (Exception i)
            {
                WriteLine("Whoops that was a mistake", i);
            }
        }
        
        static void administratorAdd(ref List<Menu> n)
        {
            string[] newItem = new string[3];
            string location = @"C:\Users\Tyler\Desktop\School\Fall2021\ITP136\ITP136WK15Project3\Project3\Project3\bin\Debug\Menu.csv";

            WriteLine("First enter the ID, then name and lastly the price of the item in that order");
            for (int z = 0; z < newItem.Length; z++)
            {
                newItem[z] = ReadLine();
            }
            try
            {
                StreamWriter writer = new StreamWriter(location, true);
                writer.WriteLine(newItem[0] + "," + newItem[1] + "," + newItem[2]);
                writer.Dispose();
                WriteLine("Item added...");
            }
            catch (Exception i)
            {
                WriteLine(i);
            }
        }

        static void administartorOverride(ref List<Orders> i)
        {
            string location = @"C:\Users\Tyler\Desktop\School\Fall2021\ITP136\ITP136WK15Project3\Project3\Project3\bin\Debug\SavedList.csv";
            string nameUpdate;
            int userInput;
            int userEdit;
            List<Orders> collection = new List<Orders>();
            
            WriteLine("Customer Name, Burger Order, Fry Order, Shake Order, Package Order");
            foreach(Orders s in collection)
            {
                WriteLine($"{s.customerName}{s.burger}{s.fries}{s.shakes}{s.package}");
            }
            WriteLine("Which customer are you needing to edit");
            userInput = Convert.ToInt32(ReadLine());

            WriteLine("What is the new name");
            nameUpdate = ReadLine();
            var index = collection.FindIndex(c => c.customerName == nameUpdate);

            foreach (Orders s in collection)
            {
                WriteLine($"{s.customerName}{s.burger}{s.fries}{s.shakes}{s.package}");
            }


        }
        
        //method to determine if it is a customer or a admin attempting to login
        public static int loginMethod()
        {
            string customerID = "customer";
            string customerPass = "Love Burgers";
            string adminID = "admin";
            string adminPass = "password";
            string userCheck;
            string passCheck;
            string userLoop = "Y";
            int x = 0;

            while (userLoop == "Y")
            {
                WriteLine("Please enter your user name:");
                userCheck = ReadLine();

                if (userCheck == adminID)
                {
                    WriteLine("Please enter the admin password:");
                    passCheck = ReadLine();
                    if (passCheck == adminPass)
                        userLoop = "N";
                    x = 1;
                }
                else if (userCheck == customerID)
                {
                    WriteLine("Please enter the password:");
                    passCheck = ReadLine();
                    if (passCheck == customerPass)
                    {
                        userLoop = "N";
                        x = 2;
                    }
                    else
                    {
                        WriteLine("You have inputted incorrect credentials, please input correct credentials");
                        userLoop = ReadLine();
                    }

                }
            }
            return x;
        }

        //method to determine what is being ordered by the customer
        static void orderMethod(ref List<Orders> i)
        {
            double burgerPrice = 4.99;
            double fryPrice = 1.99;
            double shakePrice = 2.99;
            double packagePrice = 8.99;
            double VATAX = 1.053;
            double total;
            int orderType;
            string newOrder = "Y";
            string cName;

            WriteLine("Welcome to Tyler's Burgers, Fries and Shakes");
            WriteLine("\nWelcome Customer! \nPlease input your name for the order");
            cName = ReadLine();

            while (newOrder == "Y")
            {
                int burger = 0;
                int fries = 0;
                int shake = 0;
                int package = 0;

                //importing csv file
                var menuList = File.ReadLines("Menu.csv").Select(line => new Menu(line)).ToList();

                //taking in orders
                while (newOrder == "Y")
                {
                    WriteLine("Please select from our menu which item you wish to purchase then input how many you would like to purchase: ");
                    WriteLine("{0,1}{1,10}{2,8}", "ID", "NAME", "PRICE");
                    foreach (Menu x in menuList)
                    {
                        WriteLine("{0,5}", $"{x.menuID}\t{x.menuName}\t{x.menuPrice}");
                    }
                    orderType = Convert.ToInt32(ReadLine());
                    switch (orderType)
                    {
                        case 1:
                            burger++;
                            break;

                        case 2:
                            fries++;
                            break;

                        case 3:
                            shake++;
                            break;
                        case 4:
                            package++;
                            break;
                    }

                    WriteLine("\nWould you like to order additional food? Y/N");
                    newOrder = ReadLine();
                }

                //totaling up the cost with tax
                total = burger * burgerPrice + fries * fryPrice + shake * shakePrice + package * packagePrice;
                total = total * VATAX;

                //adding it to the Orders constructor
                i.Add(new Orders(cName, burger, fries, shake, package, total));
            }
        }
    }
}

