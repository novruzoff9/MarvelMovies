using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        public string BlogName { get; set; }
        public string BlogImage { get; set; }
        public string BlogText { get; set; }
        public DateTime BlogDate { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
