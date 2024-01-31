using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AdminMessages
    {
        [Key]
        public int MessageID { get; set; }

        [StringLength(25)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string UserMail { get; set; }

        [StringLength(50)]
        public string Header { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
