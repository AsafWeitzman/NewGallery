using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public string SentBy { get; set; }

        public DateTime Posted { get; set; }

        public Paint Paint { get; set; }
    }
}