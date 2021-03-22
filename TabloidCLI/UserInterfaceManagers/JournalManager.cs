using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 3) Add Journal Entry");
           // Console.WriteLine(" 2) Author Details");
           // Console.WriteLine(" 4) Edit Author");
            //Console.WriteLine(" 5) Remove Author");
            //Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
               case "2":
                    Journal journal = Choose();
                    if (journal == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new JournalDetailManager(this, _connectionString, journal.Id);
                    }
                case "3":
                    Add();
                    return this;
                //case "4":
                  //  Edit();
                    //return this;
                //case "5":
                  //  Remove();
                    //return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        public void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine($"Title: {journal.Title} ");
                Console.WriteLine($"Date of entry: {journal.CreateDateTime}");
                Console.WriteLine($"Journal Entry: {journal.Content}");
            }
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Journal Entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();
            Console.WriteLine($"Your entry has been saved.");
            Console.Write("Press any key to continue");
            Console.ReadKey();


            DateTime dateTime =  DateTime.Now;
            journal.CreateDateTime = dateTime;

       

            _journalRepository.Insert(journal);
        }
/// <summary>
/// end at edit
/// </summary>
     
    }
}

