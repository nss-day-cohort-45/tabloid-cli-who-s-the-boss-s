using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;
        private object _authorRepository;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List ABlogsuthors");
            Console.WriteLine(" 2) Blogs Details");
            Console.WriteLine(" 3) Add Blogs");
            Console.WriteLine(" 4) Edit Blogs");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
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
            List<Blog> authors = _authorRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Author author = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Author");
            Author author = new Author();

            Console.Write("First Name: ");
            author.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            author.LastName = Console.ReadLine();

            Console.Write("Bio: ");
            author.Bio = Console.ReadLine();

            _blogRepository.Insert(author);
        }

        private void Edit()
        {
            Author blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New first name (blank to leave unchanged: ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                blogToEdit.FirstName = firstName;
            }
            Console.Write("New last name (blank to leave unchanged: ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                blogToEdit.LastName = lastName;
            }
            Console.Write("New bio (blank to leave unchanged: ");
            string bio = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bio))
            {
                blogToEdit.Bio = bio;
            }

            _blogRepository.Update(blogToEdit);
        }

        private void Remove()
        {
            Author blogToDelete = Choose("Which blog would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
