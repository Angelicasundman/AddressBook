// See https://aka.ms/new-console-template for more information
using AddressBook.Services;

IMenuManager menu = new MenuManager();  //Hämtar vår menu 

do //en loop som fortsätter tills vi avslutar programmer
{
    menu.MenuOptions(); // Kör vår metod MenuOptions
    Console.ReadKey(); //Användaren måste trycka på en tangent för att gå vidare
}
while (true);

