using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Serie
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Image { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string Producer { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        [StringLength(10)]
        public string IMDB { get; set; }

        [StringLength(20)]
        public string Universe { get; set; }
        public bool Status { get; set; }
        public bool Broadcast { get; set; }
        public string Trailer1 { get; set; }
        public string Trailer2 { get; set; }
        public string Trailer3 { get; set; }

        public ICollection<SerieComment> Comments { get; set; }
    }
}
