using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Log.Models
{
    public partial class TblRaza
    {
        public TblRaza()
        {
            TblMascotas = new HashSet<TblMascotas>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRaza { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TblMascotas> TblMascotas { get; set; }
    }
}
