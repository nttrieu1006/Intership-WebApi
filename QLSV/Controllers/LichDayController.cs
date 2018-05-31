using Model;
using QLSV.ViewModel;
using System;
using System.Linq;
using System.Web.Http;

namespace QLSV.Controllers
{
    public class LichDayController : ApiController
    {
        private ApiDbContext db;
        private ErrorModel err;

        public LichDayController()
        {
            db = new ApiDbContext();
            err = new ErrorModel();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = db.LichDays.Select(x => new LichDayModel()
            {
                Id = x.Id,
                TenGV = x.GiaoVien.HoTen,
                TenLop = x.Lop.TenLop,
                TGBatDau = x.TGBatDau,
                TGKetThuc = x.TgKetThuc
            });
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetByGvId(long id)
        {
            var list = db.LichDays.Where(x => x.GiaoVien.Id == id).Select(x => new LichDayModel()
            {
                Id = x.Id,
                TenGV = x.GiaoVien.HoTen,
                TenLop = x.Lop.TenLop,
                TGBatDau = x.TGBatDau,
                TGKetThuc = x.TgKetThuc
            });
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetByLopId(long id)
        {
            var list = db.LichDays.Where(x => x.Lop.Id == id).Select(x => new LichDayModel()
            {
                Id = x.Id,
                TenGV = x.GiaoVien.HoTen,
                TenLop = x.Lop.TenLop,
                TGBatDau = x.TGBatDau,
                TGKetThuc = x.TgKetThuc
            });
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult Create(CreateLichDayModel model)
        {
            IHttpActionResult httpActionResult;
            Lop lop = db.Lops.FirstOrDefault(x => x.Id == model.LopId);
            if (lop == null)
            {
                err.Add("Lớp không tồn tại");
            }
            GiaoVien giaovien = db.GiaoViens.FirstOrDefault(x => x.Id == model.GvId);
            if (giaovien == null)
            {
                err.Add("Giáo viên không tồn tại");
            }
            if (model.TGBatDau.HasValue && model.TGBatDau < DateTime.Now)
            {
                err.Add("Thời gian bắt đầu phải lớn hơn ngày hiện tại");
            }
            if (model.TGKetThuc.HasValue && model.TGKetThuc < model.TGBatDau)
            {
                err.Add("Thời gian kết thúc phải lớn hơn bắt đầu");
            }
            if (err.errors.Count == 0)
            {
                LichDay lichDay = new LichDay();
                lichDay.GiaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == model.GvId);
                lichDay.Lop = db.Lops.FirstOrDefault(x => x.Id == model.LopId);
                lichDay.TGBatDau = model.TGBatDau;
                lichDay.TgKetThuc = model.TGKetThuc;

                lichDay = db.LichDays.Add(lichDay);
                db.SaveChanges();
                LichDayModel viewmodel = new LichDayModel(lichDay);
                httpActionResult = Ok(viewmodel);
            }
            else
            {
                httpActionResult = Ok(err);
            }
            return httpActionResult;
        }

        [HttpPut]
        public IHttpActionResult Update(UpdateLichDayModel model)
        {
            IHttpActionResult httpActionResult;
            LichDay lichDay = db.LichDays.FirstOrDefault(x => x.Id == model.Id);
            if (lichDay == null)
            {
                err.Add("Không tìm thấy");
            }
            if(model.TGBatDau.HasValue && model.TGBatDau < DateTime.Now)
            {
                err.Add("Thời gian bắt đầu phải lớn hơn ngày hiện tại");
            }
            if(model.TGKetThuc.HasValue && model.TGKetThuc < model.TGBatDau)
            {
                err.Add("Thời gian kết thúc phải lớn hơn bắt đầu");
            }
            if(err.errors.Count == 0)
            {
                lichDay.GiaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == model.GvId) ?? db.GiaoViens.FirstOrDefault(x => x.Id == model.GvId);
                lichDay.Lop = db.Lops.FirstOrDefault(x => x.Id == model.LopId) ?? db.Lops.FirstOrDefault(x => x.Id == model.LopId);
                lichDay.TGBatDau = model.TGBatDau ?? model.TGBatDau;
                lichDay.TgKetThuc = model.TGKetThuc ?? model.TGKetThuc;

                db.Entry(lichDay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new LichDayModel(lichDay));
            }
            else
            {
                httpActionResult = Ok(err);
            }
            return httpActionResult;
        }
    }
}