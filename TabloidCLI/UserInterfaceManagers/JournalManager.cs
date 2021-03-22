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
            Console.WriteLine("---------------------------");
            Console.WriteLine("Journal Menu");
            Console.WriteLine("---------------------------");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Edit  Journal Entries");
            Console.WriteLine(" 3) Add Journal Entry");
            Console.WriteLine(" 4) Delete Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "3":
                    Add();
                    return this;
                case "2":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
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
       
        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal entry would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Here is the current entry: ");
            {
                Console.WriteLine($"{journalToEdit.Content}");

            }
            Console.WriteLine("Press Enter to escape without editing or change your content now");
                
                
                
                string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }
            
            _journalRepository.Update(journalToEdit);
        }
       
        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal entry  would you like to remove?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }

    }
}

