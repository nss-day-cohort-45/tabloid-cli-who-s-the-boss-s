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
        private BlogRepository _blogRepository;
        private int _postId;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);

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

            Console.WriteLine("URL of post: ");
            post.Url = Console.ReadLine();

            Console.WriteLine("Publication Date (MM/DD/YYYY)");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Who is the author of post: ");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                post.Author = authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");

            }



            Console.WriteLine("Choose an associated blog post: ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string blogInput = Console.ReadLine();
            try
            {
                int choice = int.Parse(blogInput);
                post.Blog = blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");

            }


            _postRepository.Insert(post);


        }

        private void Edit()
        {
            Console.WriteLine("Which post would you like to edit?");
            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            int postToEditIndex = Int32.Parse(Console.ReadLine());

            Post postToEdit = posts[postToEditIndex - 1];

            // Here menu to get updates


            Console.WriteLine("Title of post: ");
            string newTitle = Console.ReadLine();
            if (newTitle == "" || newTitle == null)
            {
                
            } else
            {
                postToEdit.Title = newTitle;
            };

            Console.WriteLine("URL of post: ");
            string newURL = Console.ReadLine();
            if (newURL == null || newURL == "")
            {
               
            }
            else
            {
                postToEdit.Url = newURL;
            };

            Console.WriteLine("Publication Date (MM/DD/YYYY)");
            DateTime newDate = DateTime.Parse(Console.ReadLine());
            if (newDate == null)
            {
                
            }
            else
            {
                postToEdit.PublishDateTime = newDate;
            };

            Console.WriteLine("Who is the author of post: ");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                postToEdit.Author = authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");

            }



            Console.WriteLine("Choose an associated blog post: ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string blogInput = Console.ReadLine();
            try
            {
                int choice = int.Parse(blogInput);
                postToEdit.Blog = blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");

            }



            _postRepository.Update(postToEdit);
        }


        private void Remove()
        {
            Console.WriteLine("Which post would you like to delete?");
            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            int postToDelete = Int32.Parse(Console.ReadLine());


            _postRepository.Delete(posts[postToDelete - 1].Id);
        }
    }
    
}
