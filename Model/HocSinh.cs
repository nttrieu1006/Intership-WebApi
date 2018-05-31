using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("HocSinh")]
    public class HocSinh : Entity<long>, IUser
    {
        [MaxLength(256)]
        public string MaHS { get; set; }

        [MaxLength(256)]
        public string HoTen { get; set; }

        [MaxLength(256)]
        public string NTNS { get; set; }

        public virtual Lop Lop { get; set; }
    }
}