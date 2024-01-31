using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(25)]
        public string UserName { get; set; }
        public string UserImage { get; set; }

        [StringLength(200)]
        public string UserMail { get; set; }

        [StringLength(200)]
        public string UserPassword { get; set; }
        public DateTime UserSignDate { get; set; }

        [StringLength(20)]
        public string UserRole { get; set; }

        public ICollection<MovieComment> Comments { get; set; }        
        public ICollection<SerieComment> SerieComments { get; set; }
        public ICollection<AnimationComment> AnimationComments { get; set; }        
        public ICollection<Favorites> Favorite { get; set; }
        public ICollection<FavoriteSerie> FavoriteSeries { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
