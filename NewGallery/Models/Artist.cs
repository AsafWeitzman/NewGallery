using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        public string Email { get; set; }
        
        [Display(Name = "Favorite Style")]

        public string FavoriteStyle { get; set; }

        public int Rate { get; set; }

        public virtual ICollection<Paint> Paints { get; set; }
    }
}