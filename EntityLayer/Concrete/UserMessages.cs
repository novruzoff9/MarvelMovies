using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class UserMessages
    {
        [Key]
        public int MessageID { get; set; }

        [StringLength(200)]
        public string SenderMail { get; set; }

        [StringLength(200)]
        public string ReceiverMail { get; set; }

        [StringLength(50)]
        public string MessageHeader { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(10)]
        public string RecieverStatus { get; set; }

        [StringLength(10)]
        public string SenderStatus { get; set; }
    }
}
