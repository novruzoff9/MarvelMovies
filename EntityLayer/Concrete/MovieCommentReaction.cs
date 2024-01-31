using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class MovieCommentReaction
    {
        [Key]
        public int ReactionID { get; set; }

        [StringLength(10)]
        public string Reaction { get; set; }        

        public int MovieCommentID { get; set; }
        public virtual MovieComment MovieComment { get; set; }

        public int UserId { get; set; }
    }
}
