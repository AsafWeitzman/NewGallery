using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Paint
    {
        public int PaintID { get; set; }

        [Display(Name = "Paint Name")]
        public string Paintname { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        public string CreateDate { get; set; }

        public string Size { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name="Price")]
        public int Price { get; set; }

        public string Type { get; set; }

        public int ArtistID { get; set; }
        public Artist Artist { get; set; }
        public string artistname { get; set; }
        [Display(Name = "The Paint")]
        public string ImgUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}