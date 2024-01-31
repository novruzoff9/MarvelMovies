using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ChatMessage
    {
        [Key]
        public int MessageID { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public bool Seen { get; set; }
        public int WriterID { get; set; }

        public int ChatID { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
