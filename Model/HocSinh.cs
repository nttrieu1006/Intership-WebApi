using System.ComponentModel.DataAnnotations;

namespace Model
{
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