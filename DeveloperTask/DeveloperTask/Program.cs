using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeveloperTask
{
    class Program
    {     
        static void Main(string[] args)
        {

            var myJsonString = File.ReadAllText("data.json");
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(myJsonString).ToList();

            bool run = true;
            Console.WriteLine("Welcome to the Visma's book library");

            while (run) {
                Console.WriteLine("\nChoose what would you like to do:");
                Console.WriteLine(" 1 Add a book\n 2 Take a book\n 3 Delete a book\n 4 Return a book\n 5 Show all books\n 6 Exit");
                int userInput = Convert.ToInt32(Console.ReadLine());

                if(userInput == 5)
                {
                    Console.WriteLine("All the books:");
                    foreach(var b in booksList)
                    {
                        b.bookInfo();
                    }
                }
                else if(userInput == 1)
                {
                    Console.WriteLine("\nEnter books information as shown in 1 line:");
                    Console.WriteLine("Name,Author,Category,Language,PublicationDate,ISBN");
                    string bookToAdd = Console.ReadLine();
                    string[] data = bookToAdd.Split(',');
                    Book book = new Book(data[0], data[1], data[2], data[3], data[4], data[5]);
                    booksList.Add(book);
                    Console.WriteLine("You succesfully added a book: \n ");
                    book.bookInfo();
                }
                else if(userInput == 2)
                {

                    Console.WriteLine("Choose if you want to filter the books:");
                    Console.WriteLine(" 0 Show all available books\n 1 By author\n 2 By category\n 3 By Language 4\n 5 By name\n 6 By books available");
                    int Input = Convert.ToInt32(Console.ReadLine());
                    if(Input == 0) displayBooks(booksList, Input, " ");
                        else {
                            Console.WriteLine("Please enter the value you are looking for:");
                            string filterValue = Console.ReadLine();
                            displayBooks(booksList, Input, filterValue);
                        }
                    Console.WriteLine("Do you want to take a book:");
                    Console.WriteLine(" 1 Yes\n 2 No");
                    Input = Convert.ToInt32(Console.ReadLine());
                    if(Input == 1) {
                        Console.WriteLine("Enter the name of the book you want to take");
                        string bookToTake = Console.ReadLine();
                        Console.WriteLine("Please enter your name:");
                        string name = Console.ReadLine();

                        if (canUserTakeBook(name, booksList)){
                            Console.WriteLine("Enter the date when the book is taken:");
                            DateTime date = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Enter the period for taking the book in days:");
                            int days = Convert.ToInt32(Console.ReadLine());
                            foreach (var b in booksList)
                            {
                                if (b.name.Equals(bookToTake)) b.TakeBook(name, date, days);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have already taken 3 books thats the limit sorry");
                        }
                    }
                    
                }
                else if(userInput == 3)
                {
                    Console.WriteLine("Enter the name of the book you want to delete:");
                    string bookDelete = Console.ReadLine();
                    int counter = 0;
                    foreach (var b in booksList)
                    {
                        if (b.name.Equals(bookDelete)) break;
                        counter++;
                    }
                    booksList.RemoveAt(counter);
                }
                else if (userInput == 4)
                {
                    Console.WriteLine("Enter the name of the book you want to return:");
                    string bookToReturn = Console.ReadLine();
                    foreach (var b in booksList.Reverse<Book>())
                    {
                        if (b.name.Equals(bookToReturn)) b.returnBook();
                    }
                }
                else
                {
                    break;
                }
            }

        }

        public static bool canUserTakeBook(string name, List<Book> books)         
        {
            int counter = 0;
            foreach (var b in books)
            {
                if (b.personTakingBook.Equals(name)) counter++;
            }
            if(counter >= 3)
            {
                return false;
            }
            else {
                return true;
            }
           
        }
        static void displayBooks(List<Book> books, int parameter, string filterValue)
        {
            List<Book> filtered = new List<Book>();
            switch (parameter){
                case 0: 
                    break;
                case 1 :
                    filtered = (List<Book>)books.Where(b => b.author.Equals(filterValue)).ToList();
                    break;
                case 2:
                    filtered = (List<Book>)books.Where(b => b.category.Equals(filterValue)).ToList();
                    break;
                case 3:
                    filtered = (List<Book>)books.Where(b => b.language.Equals(filterValue)).ToList();
                    break;
                case 4:
                    filtered = (List<Book>)books.Where(b => b.isbn.Equals(filterValue)).ToList();
                    break;
                case 5:
                    filtered = (List<Book>)books.Where(b => b.name.Equals(filterValue)).ToList();
                    break;
                case 6:
                    filtered = (List<Book>)books.Where(b => b.isBookTaken.Equals(false)).ToList();
                    break;
            }
            if(parameter == 0)
            {
                foreach(Book b in books)
                {
                    if(!b.isBookTaken) b.bookInfo();
                }
            }
            else if(filtered.Count() <= 0)
            {
                Console.WriteLine("Could not found anything related sorry :(");
            }else{
                foreach (Book b in filtered)
                {
                    b.bookInfo();
                }
            }        

        }
    }
}
