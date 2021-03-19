using System;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogRepository
    {
        private string connectionString;

        public BlogRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void Update(Author blogToEdit)
        {
            throw new NotImplementedException();
        }

        internal void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}