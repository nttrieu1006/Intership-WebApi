using Model;
using System;

namespace QLSV.ViewModel
{
    public class LichDayModel
    {
        public long Id { get; set; }
        public string TenGV { get; set; }
        public string TenLop { get; set; }
        public DateTime? TGBatDau { get; set; }
        public DateTime? TGKetThuc { get; set; }

        public LichDayModel()
        {
        }

        public LichDayModel(LichDay lichDay)
        {
            Id = lichDay.Id;
            TenGV = lichDay.GiaoVien.HoTen;
            TenLop = lichDay.Lop.TenLop;
            TGBatDau = lichDay.TGBatDau;
            TGKetThuc = lichDay.TgKetThuc;
        }
    }

    public class CreateLichDayModel
    {
        public long GvId { get; set; }
        public long LopId { get; set; }
        public DateTime? TGBatDau { get; set; }
        public DateTime? TGKetThuc { get; set; }
    }
    public class UpdateLichDayModel : CreateLichDayModel
    {
        public long Id { get; set; }
    }
}