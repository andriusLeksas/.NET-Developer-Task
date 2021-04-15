using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperTask
{
    //Book structure
    class Book
    {
        public string name;
        public string author;
        public string category;
        public string language;
        public string publicationDate;
        public string isbn;

        public string personTakingBook;
        public int periodInDays;
        public bool isBookTaken;
        public DateTime whenBookisTaken;

        public Book(string name, string author, string category,
            string language, string publicationDate, string isbn)
        {
            this.name = name;
            this.author = author;
            this.category = category;
            this.language = language;
            this.publicationDate = publicationDate;
            this.isbn = isbn;
            personTakingBook = "";
        }

        //Period is entered with date 2021-05-15
        public void TakeBook(string personTakingBook, DateTime whenBookisTaken, int periodInDays)
        {
            this.personTakingBook = personTakingBook;
            if (periodInDays <= 60)
            {
                this.periodInDays = periodInDays;
                Console.WriteLine($"You have taken {name} book congratz! Do not" +
                    $" forget to return it in {periodInDays} days");
                isBookTaken = true;
                this.whenBookisTaken = whenBookisTaken;
            }
            else
            {
                Console.WriteLine("You can not take a book for more than 2 months :))");
            }
        }

        public void returnBook()
        {
            if((DateTime.Now - whenBookisTaken).TotalDays > periodInDays)
            {
                Console.WriteLine("You insolent fool you are late\n");
                isBookTaken = false;
                personTakingBook = "";
            }
            else
            {
                Console.WriteLine("Thank you for returning book in time\n");
            }
            
        }

        //Prints formated book information
        public void bookInfo()
        {
            Console.WriteLine($"Name-{this.name}, Author-{this.author}, Category-{this.category}, Language-{this.language}, PublicationDate-{this.publicationDate}, ISBN-{this.isbn}");
        }
    }
}
