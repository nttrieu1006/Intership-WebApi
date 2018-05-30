using Model;

namespace QLSV.ViewModel
{
    public class HocSinhModel : IUser
    {
        public long Id { get; set; }
        public string MaHS { get; set; }
        public string HoTen { get; set; }
        public string NTNS { get; set; }
        public string Lop { get; set; }

        public HocSinhModel()
        {
        }

        public HocSinhModel(HocSinh hocsinh)
        {
            this.Id = hocsinh.Id;
            HoTen = hocsinh.HoTen;
            MaHS = hocsinh.MaHS;
            NTNS = hocsinh.NTNS;
            Lop = hocsinh.Lop.TenLop;
        }
    }

    public class CreateHSModel : IUser
    {
        public string MaHS { get; set; }
        public string HoTen { get; set; }
        public string NTNS { get; set; }
        public long Lop { get; set; }
    }

    public class UpdateHSModel : CreateHSModel
    {
        public long Id { get; set; }
    }
}