using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SerieCommentAnswer
    {
        [Key]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public bool AnswerStatus { get; set; }
        public DateTime AnswerDate { get; set; }
        public bool Spoiler { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }

        public int SerieCommentID { get; set; }
        public virtual SerieComment SerieComment { get; set; }

        public int UserId { get; set; }

        public ICollection<SerieCommentAnswerReaction> SerieCommentAnswerReactions { get; set; }
    }
}
