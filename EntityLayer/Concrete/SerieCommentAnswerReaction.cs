﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SerieCommentAnswerReaction
    {
        [Key]
        public int ReactionID { get; set; }

        [StringLength(10)]
        public string Reaction { get; set; }

        public int AnswerID { get; set; }
        public virtual SerieCommentAnswer SerieCommentAnswer { get; set; }

        public int UserID { get; set; }
    }
}
