using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AnimationComment
    {
        [Key]
        public int MovieCommentID { get; set; }
        public string MovieCommentText { get; set; }
        public bool CommentStatus { get; set; }
        public DateTime CommentDate { get; set; }

        public int ID { get; set; }
        public virtual Animation Animation { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
