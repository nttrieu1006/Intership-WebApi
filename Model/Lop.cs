using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Lop : Entity<long>
    {
        [MaxLength(256)]
        public string MaLop { get; set; }

        [MaxLength(256)]
        public string TenLop { get; set; }

        public virtual GiaoVien GiaoVien { get; set; }
        public virtual ICollection<LichDay> DsLop { get; set; }
        public virtual ICollection<HocSinh> HocSinhs { get; set; }
    }
}