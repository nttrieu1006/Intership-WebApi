using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("LichDay")]
    public class LichDay : Entity<long>
    {
        public virtual GiaoVien GiaoVien { set; get; }
        public virtual Lop Lop { set; get; }

        public DateTime? TGBatDau { get; set; }
        public DateTime? TgKetThuc { get; set; }
    }
}