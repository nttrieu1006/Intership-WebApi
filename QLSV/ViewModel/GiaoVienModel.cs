using Model;

namespace QLSV.ViewModel
{
    public class GiaoVienModel : IUser
    {
        public long Id { get; set; }
        public string MaGV { get; set; }
        public string HoTen { get; set; }
        public string NTNS { get; set; }
        public string TrinhDo { get; set; }

        public GiaoVienModel()
        {
        }

        public GiaoVienModel(GiaoVien giaoVien)
        {
            Id = giaoVien.Id;
            MaGV = giaoVien.MAGV;
            HoTen = giaoVien.HoTen;
            NTNS = giaoVien.NTNS;
            TrinhDo = giaoVien.TrinhDo;
        }
    }

    public class CreateGVModel : IUser
    {
        public string MaGV { get; set; }
        public string HoTen { get; set; }
        public string NTNS { get; set; }
        public string TrinhDo { get; set; }
    }

    public class UpdateGVModel : CreateGVModel
    {
        public long Id { get; set; }
    }
}