using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }
        public int User1stID { get; set; }
        public int User2ndID { get; set; }
        public DateTime LastMessage { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
