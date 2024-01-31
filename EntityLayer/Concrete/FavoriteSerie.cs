using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FavoriteSerie
    {
        [Key]
        public int ID { get; set; }

        public int SerieID { get; set; }
        public virtual Serie Movie { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
