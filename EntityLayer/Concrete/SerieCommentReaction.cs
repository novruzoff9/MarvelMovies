using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SerieCommentReaction
    {
        [Key]
        public int ReactionID { get; set; }

        [StringLength(10)]
        public string Reaction { get; set; }

        public int SerieCommentID { get; set; }
        public virtual SerieComment SerieComment { get; set; }

        public int UserId { get; set; }
    }
}
