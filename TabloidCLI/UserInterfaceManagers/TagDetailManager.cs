using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class TagDetailManager: IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _tagId;

        public TagDetailManager(IUserInterfaceManager parentUI, string connectionString, int authorId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _tagId = authorId;
        }

        public IUserInterfaceManager Execute()
        {
            Tag tag = _tagRepository.Get(_tagId);
            Console.WriteLine($"{tag.Name} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Tag tag = _tagRepository.Get(_tagId);
            Console.WriteLine($"Name: {tag.Name}");
            Console.WriteLine();
        }

        

    }
}
