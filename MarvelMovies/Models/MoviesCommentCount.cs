using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelMovies.Models
{
    public class MoviesCommentCount
    {
        public string MovieName { get; set; }
        public int CommentCount { get; set; }
        public int MovieComment { get; set; }
        public int AnswerComment { get; set; }
    }
}