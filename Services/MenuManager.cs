using AddressBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Services
{
    internal interface IMenuManager // Interface för alla metoder 
    {
        public void MenuOptions(); // För alla våra menyval 
        public void AddContact(); // För att lägga till en kontakt
        public void RemoveContact(); // För att ta bort en kontakt 
        public void SearchContact(); // För att söka upp en specifik kontakt 
        public void ShowAllContact(); // Listar upp alla våra kontakter

        public void UpdateContact(string id);  // Uppdatera en kontakt


    }
    internal class MenuManager : IMenuManager  // Ny class MenuManager från vårat "kontrakt" interface IMenuManager
    {
        private List<Contact> _contacts = new(); // privat lista instansieras
        private string _filePath = $@"C:\AngelicaSkola\Inlamning\Addressbook.json"; // Sökväg till json fil 
        private IFileManager _fileManager = new FileManager(); //så vi kan läsa in vår lista från filemanager read
        public void MenuOptions()
        {
            _contacts = JsonConvert.DeserializeObject<List<Contact>>(_fileManager.Read(_filePath)); //Läser in vår lista från vår json fil
            Console.Clear();
            Console.WriteLine("****** ADDRESSBOOK MENU ****** ");
            Console.WriteLine("1. Add new contact");
            Console.WriteLine("2. List all contacts");
            Console.WriteLine("3. Search contact");
            Console.WriteLine("4. Remove contact");
            Console.WriteLine("5. Quit program");
            Console.Write("Choose one option (1-6): ");
            var option = Console.ReadLine(); // Option för att läsa in användarens val

            switch (option) // en switch baserat på användarens val 
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ShowAllContact();
                    break;
                case "3":
                    SearchContact();
                    break;
                case "4":
                    RemoveContact();
                    break;
                case "5":
                    Environment.Exit(0); // avslutar programmet 
                    break;

                default:
                    Console.WriteLine("Invalid option"); // om användaren inte väljer mellan 1-5 skrivs denna text ut 
                    break;
            }
        }
        public void AddContact()
        {
            var contact = new Contact(); //deklarerar en ny kontakt
            Console.Clear();
            Console.WriteLine("***** ADD NEW CONTACT *****");
            Console.Write("Firstname: "); contact.FirstName = Console.ReadLine(); // läser in användarens förnamn till contact.Firstname
            Console.Write("Lastname: "); contact.LastName = Console.ReadLine();
            Console.Write("Phonenumber: "); contact.PhoneNumber = int.Parse(Console.ReadLine()); // int.Parse för att läsa in siffror 
            _contacts.Add(contact); //lägger till kontakten i listan _contacts 
            _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts)); // sparar till vår json fil 
            Console.WriteLine("New contact added, press any button to continue");
        }
        public void UpdateContact(string id) // läser in användarens val från "var id" 
        {

            var contact = _contacts.FirstOrDefault(x => x.Id == new Guid(id)); // söker fram kontakten baserat på id 
            Console.Clear();
            Console.WriteLine("***** UPDATE CONTACT *****");
            Console.Write("Enter ID on contact you want to update");
            var index = _contacts.IndexOf(contact); //letar fram vilket index kontakten ligger på 

            Console.WriteLine("\n1. Update firstname"); //användaren får ett val om vad den vill uppdatera 
            Console.WriteLine("2. Update lastname");
            Console.WriteLine("3. Update phonenumber");
            var option = Console.ReadLine(); //läser in användarens val 

            switch (option)
            {
                case "1":

                    Console.WriteLine("Enter new Firstname: "); var NewFirstName = Console.ReadLine(); //läser in ett nytt förnamn 
                    contact.FirstName = NewFirstName; // uppdaterar Firstnamne till NewFirstname 
                    _contacts[index] = contact; //Tar fram kontakten på rätt index 
                    _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts)); //sparar till listan igen 
                    Console.WriteLine("Firstname updated, press any button to continue");
                    break;
                case "2":
                    Console.WriteLine("Enter new Lastname: "); var NewlastName = Console.ReadLine();
                    contact.LastName = NewlastName;
                    _contacts[index] = contact;
                    _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts));
                    Console.WriteLine("Lastname updated, press any button to continue");
                    break;
                case "3":
                    Console.WriteLine("Enter new phonenumber: "); int Phone = int.Parse(Console.ReadLine());
                    contact.PhoneNumber = Phone;
                    _contacts[index] = contact;
                    _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts));
                    Console.WriteLine("Phonenumber updated, press any button to continue");
                    break;
                default:
                    Console.WriteLine("Invalid option"); //Om användaren knappar in ogiltligt val 
                    Console.ReadKey();
                    break;
            }


        }
        public void ShowAllContact()
        {
            
            Console.Clear(); //clear för att få bort allt som visades tidigare
            Console.WriteLine("***** CONTACTLIST *****");

            foreach (var contact in _contacts) //går igenom hela vår lista _contacts 
                Console.WriteLine($"ID:\t{contact.Id} \n Name:\t{contact.FirstName} - {contact.LastName} \n Phonenumber:\t {contact.PhoneNumber}"); //skriver ut all info om kontakterna 
            Console.WriteLine("---------------------------------------");


            Console.WriteLine();
            Console.Write("Do you want to update any of the contacts? y/n: "); // frågar om vi vill uppdatera någon av kontakterna 
            var option = Console.ReadLine(); // läser in användarens val 
            if (option.ToLower() == "y") // om användaren trycker y tar den oss vidare annars tillbaka till menyn 
            {
                Console.Write("Enter ID of the contact you want to update: ");
                var id = Console.ReadLine(); // läser in id som användaren valde 
                if (!string.IsNullOrEmpty(id))
                    UpdateContact(id); // tar oss till metoden UpdateContact 
            }




        }
        public void SearchContact()
        {
            Console.Clear();
            Console.WriteLine("***** SEARCH CONTACT *****");
            Console.WriteLine("Enter name on contact: ");
            var SearchContact = Console.ReadLine(); // läser in användarens söknamn 
            try
            {
                foreach (Contact contact in _contacts) // går igenom alla kontakter i vår lista _contacts
                {
                    if (SearchContact.ToLower() == contact.FirstName.ToLower()) //Letar efter en kontakts förnamn matchar sökningen
                    {
                        Console.Clear();
                        Console.WriteLine("Hittade: ");
                        Console.WriteLine($"Name: {contact.FirstName} - {contact.LastName} \nPhonenumber: {contact.PhoneNumber}"); //skriver ut Förnamn, Efternamn och Telefonnummer
                        Console.WriteLine("Press any button to continue");
                        Console.ReadKey();
                       
                    }
                    
                }
            }
            catch
            {
               
            }

        }
        public void RemoveContact()
        {
         
            Console.Clear();
            Console.WriteLine("***** CONTACTLIST *****");

            foreach (var contact in _contacts) //skriver ut alla kontakter i _contacts
                Console.WriteLine($"ID:\t{contact.Id} \n Name:\t{contact.FirstName} - {contact.LastName} \n Phonenumber:\t {contact.PhoneNumber}"); //skriver ut id, förnamn, efternamn och telefonnummer
            Console.WriteLine("---------------------------------------");
            try //en try så att ifall användaren knappar in ett id som inte finns krashar inte programmer och texten "invalid id" skrivs ut 
            {
                Console.Write("\n\n\nEnter the id of the contact you want to remove: ");
                Guid id = Guid.Parse(Console.ReadLine()); //läser in det id användaren knappar in
                _contacts = _contacts.Where(x => x.Id != id).ToList(); //gör en lista utan det id vi knappade in 
                _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts)); //sparar listan till vår json fil utan det id vi ville ta bort  

                Console.WriteLine("Contact removed, press any button to continue");

            }
            catch // om id användaren knappade in inte finns kommer denna text 
            {
                Console.WriteLine("Invalid ID, press any button to continue");
                Console.ReadKey();

            }

        }
        
    }
}
