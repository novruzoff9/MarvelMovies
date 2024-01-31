using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public DbSet<Movie> Movies  { get; set; }
        public DbSet<MovieComment> MovieComments  { get; set; }        
        public DbSet<Serie> Series { get; set; }
        public DbSet<SerieComment> SerieComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdminMessages> AdminMessages { get; set; }
        public DbSet<UserMessages> UserMessages { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Animation> Animations { get; set; }
        public DbSet<AnimationComment> AnimationComments { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<FavoriteSerie> FavoriteSeries { get; set; }
        public DbSet<MovieCommentReaction> MovieCommentReactions { get; set; }
        public DbSet<MovieCommentAnswer> MovieCommentAnswers { get; set; }
        public DbSet<MovieCommentAnswerReaction> MovieCommentAnswerReactions { get; set; }
        public DbSet<SerieCommentReaction> SerieCommentReactions { get; set; }
        public DbSet<SerieCommentAnswer> SerieCommentAnswers { get; set; }
        public DbSet<SerieCommentAnswerReaction> SerieCommentAnswerReactions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
