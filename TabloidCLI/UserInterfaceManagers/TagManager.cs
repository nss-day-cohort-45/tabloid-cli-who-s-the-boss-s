using System;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
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

        private void List()
        {
            List<Tag> tags = _tagRepository.GetAll();
            foreach ( Tag tag in tags)
            {
                Console.WriteLine($"Tag title: {tag.Name}");
            }
        }

        private void Add()
        {
            throw new NotImplementedException();
        }

        private void Edit()
        {
            Console.WriteLine("Which tag would you like to edit? ");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag= tags[i];
                Console.WriteLine($"{i + 1}) {tag.Name} ");
            }
            Console.Write("> ");

            bool indexCanParse = Int32.TryParse(Console.ReadLine(), out int indexToEdit);
            while(!indexCanParse)
            {
                Console.WriteLine("That was an invalid selection. Try again.");
                Console.Write("> ");

                indexCanParse = Int32.TryParse(Console.ReadLine(), out  indexToEdit);

            }

            Tag tagToEdit = tags[indexToEdit - 1];

            Console.WriteLine("New tag name:");
            string newName = Console.ReadLine();
            if (newName == "" || newName == null)
            {

            }
            else
            {
                tagToEdit.Name = newName;
            };

            _tagRepository.Update(tagToEdit);
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
