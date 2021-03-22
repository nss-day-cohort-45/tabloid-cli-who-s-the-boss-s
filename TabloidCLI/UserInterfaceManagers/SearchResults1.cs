using System;
using System.Runtime.Serialization;

namespace TabloidCLI.UserInterfaceManagers
{
    [Serializable]
    internal class SearchResults : Exception
    {
        private MainMenuManager mainMenuManager;
        private string cONNECTION_STRING;

        public SearchResults()
        {
        }

        public SearchResults(string message) : base(message)
        {
        }

        public SearchResults(MainMenuManager mainMenuManager, string cONNECTION_STRING)
        {
            this.mainMenuManager = mainMenuManager;
            this.cONNECTION_STRING = cONNECTION_STRING;
        }

        public SearchResults(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchResults(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}