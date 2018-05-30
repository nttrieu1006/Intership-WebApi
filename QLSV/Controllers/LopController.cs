using Model;
using QLSV.ViewModel;
using System.Linq;
using System.Web.Http;

namespace QLSV.Controllers
{
    public class LopController : ApiController
    {
        private ApiDbContext db;
        private ErrorModel err;

        public LopController()
        {
            db = new ApiDbContext();
            err = new ErrorModel();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = db.Lops.Select(x => new LopModel
            {
                MaLop = x.MaLop,
                TenLop = x.TenLop,
                Id = x.Id,
                GVChuNhiem = x.GiaoVien.Id
            });
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetById(long id)
        {
            IHttpActionResult httpActionResult;
            Lop lop = db.Lops.FirstOrDefault(x => x.Id == id);
            if (lop == null)
            {
                err.Add("Không tìm thấy lớp yêu cầu");
                httpActionResult = Ok(err);
            }
            else
            {
                httpActionResult = Ok(new LopModel(lop));
            }
            return httpActionResult;
        }

        [HttpPost]
        public IHttpActionResult Create(CreateLopModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.MaLop))
            {
                err.Add("Mã lớp là trường bắt buộc");
            }

            if (string.IsNullOrEmpty(model.TenLop))
            {
                err.Add("Tên lớp là trường bắt buộc");
            }
            var GVCN = db.GiaoViens.FirstOrDefault(x => x.Id == model.GVChuNhiem);

            if (GVCN == null)
            {
                err.Add("Giáo viên không tồn tại");
            }
            if (err.errors.Count == 0)
            {
                Lop lop = new Lop();
                lop.MaLop = model.MaLop;
                lop.TenLop = model.TenLop;
                lop.GiaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == model.GVChuNhiem);
                lop = db.Lops.Add(lop);
                db.SaveChanges();
                LopModel viewmodel = new LopModel(lop);
                httpActionResult = Ok(viewmodel);
            }
            else
            {
                httpActionResult = Ok(err);
            }

            return httpActionResult;
        }

        [HttpPut]
        public IHttpActionResult Update(UpdateLopModel model)
        {
            IHttpActionResult httpActionResult;
            Lop lop = db.Lops.FirstOrDefault(x => x.Id == model.Id);
            if (lop == null)
            {
                err.Add("Không tìm thấy");
                httpActionResult = Ok(err);
            }
            else
            {
                lop.MaLop = model.MaLop ?? model.MaLop;
                lop.TenLop = model.TenLop ?? model.TenLop;
                lop.GiaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == model.GVChuNhiem) ?? db.GiaoViens.FirstOrDefault(x => x.Id == model.GVChuNhiem);
                db.Entry(lop).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new LopModel(lop));
            }
            return httpActionResult;
        }
    }
}