using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    public class PostTag
    {
        public int id { get; set; }

        public Tag nameOfTag { get; set; }
        public Post Post { get; set; }

    }
}
