using CustomerAppBLL;
using CustomerAppEntity;
using System;

namespace CustomerAppUI
{
   class Program
    {
        static BLLFacade bllFacade = new BLLFacade();
        
        static void Main(string[] args)
        {
            var cust1 = new Customer()
            {
                FirstName = "Dave",
                LastName = "Tamo",
                Address = "Stormgade 200"
            };
            bllFacade.CustomerService.Create(cust1);


            string[] menuItems = {
                "Customer List.",
                "Add Customer.",
                "Delete Customer.",
                "Edit Customer.",
                "Exit."
            };

            

            var selection = ShowMenu(menuItems);

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        ListCustomers();
                        break;
                    case 2:
                        AddCustomers();
                        break;
                    case 3:
                        DeleteCustomer();
                        break;
                    case 4:
                        EditCustomer();
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            
            Console.WriteLine($"------------------------------------------" +
                $"\nSee you later and please come back again!\n");

            Console.ReadLine();
        }

        private static void EditCustomer()
        {
            var customer = FindCustomerById();
            if(customer != null)
            {
                Console.WriteLine("$FirstName: \n");
                customer.FirstName = Console.ReadLine();
                Console.WriteLine("$LastName: \n");
                customer.LastName = Console.ReadLine();
                Console.WriteLine("$Address: \n");
                customer.Address = Console.ReadLine();
				bllFacade.CustomerService.Update(customer);
			}
            else
            {
                Console.WriteLine($"\nWe cannot find this customer!\n");
            }

        }

       
        private static Customer FindCustomerById()
        {
            Console.WriteLine($"\nInsert customer's Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine($"\nPlease insert a number");
            }
            return bllFacade.CustomerService.Get(id);
        }

        private static void DeleteCustomer()
        {
            var customerFound = FindCustomerById();
            if(customerFound != null)
            {
                bllFacade.CustomerService.Delete(customerFound.Id);
            }

            var response = 
                customerFound == null ?
                "\nSorry but this customer was not found!" : "\nThis customer was deleted!";
            Console.WriteLine(response);
        }

        private static void AddCustomers()
        {
            Console.WriteLine($"\nFirstname: ");
            var firstName = Console.ReadLine();

            Console.WriteLine($"\nLastname: ");
            var lastName = Console.ReadLine();

            Console.WriteLine($"\nAddress: ");
            var address = Console.ReadLine();

            bllFacade.CustomerService.Create(new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            });
        }

        private static void ListCustomers()
        {
            Console.WriteLine("\nThe list of all current customers:");
            //Console.WriteLine("----------------------------------");
            foreach (var customer in bllFacade.CustomerService.GetAll())
            {
                Console.WriteLine(
                    $"\n----------------------------------\n" +
                    $"Id: [{customer.Id}] \nFull name: {customer.FirstName} " +
                                $"{customer.LastName} " +
                                $"\nAddress: {customer.Address} " +
                                $"\n----------------------------------\n");
            }
            Console.WriteLine("\n");

        }

        private static int ShowMenu(string[] menuItems)
        {

            Console.WriteLine($"\nMake your selection:\n" +
                              $"--------------------");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                || selection < 1
                || selection > 5)
            {
                Console.WriteLine($"\nSelect a number between 1-5\n");
            }

            return selection;
        }
    }
}
