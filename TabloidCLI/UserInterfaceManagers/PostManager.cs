using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private TagRepository _tagRepository;
        private int _postId;
       

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
           
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Choose();
                   
                     return this;
                    
                case "3":
                    Add();
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"Post title: {post.Title}");
                Console.WriteLine($"Post URL: {post.Url}");
            }
        }

        private void Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($"{ i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Post chosenPost = posts[choice - 1];
                 Console.WriteLine(chosenPost.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                
            }

        }



        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.WriteLine("Title of post: ");
            post.Title = Console.ReadLine();

            Console.WriteLine("Author of post: ");
            post.Author = null;

            Console.WriteLine("URL of post: ");
            post.Url = Console.ReadLine();

            Console.WriteLine("Associated blog post: ");
            post.Blog = null;

            Console.WriteLine("Publication Date (MM/DD/YYYY)");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());
        }

        private void Edit()
        {
            Console.WriteLine("not implemented yet");
        }

        private void Remove()
        {
            Console.WriteLine("not implemented yet");
        }

    }
}
