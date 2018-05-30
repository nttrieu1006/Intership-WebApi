using Model;
using QLSV.ViewModel;
using System.Linq;
using System.Web.Http;

namespace QLSV.Controllers
{
    public class GiaoVienController : ApiController
    {
        private ApiDbContext db;
        private ErrorModel err;

        public GiaoVienController()
        {
            db = new ApiDbContext();
            err = new ErrorModel();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = db.GiaoViens.Select(x => new GiaoVienModel
            {
                Id = x.Id,
                MaGV = x.MAGV,
                HoTen = x.HoTen,
                NTNS = x.NTNS,
                TrinhDo = x.TrinhDo
            });
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetById(long id)
        {
            IHttpActionResult httpActionResult;
            GiaoVien giaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == id);
            if (giaoVien == null)
            {
                err.Add("Không tìm thấy giáo viên");
                httpActionResult = Ok(err);
            }
            else
            {
                httpActionResult = Ok(new GiaoVienModel(giaoVien));
            }
            return httpActionResult;
        }

        [HttpPost]
        public IHttpActionResult Create(CreateGVModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.MaGV))
            {
                err.Add("Mã giáo viên không được để trống");
            }

            if (string.IsNullOrEmpty(model.HoTen))
            {
                err.Add("Họ tên giáo viên không được để trống");
            }

            if (err.errors.Count == 0)
            {
                GiaoVien giaoVien = new GiaoVien();
                giaoVien.MAGV = model.MaGV;
                giaoVien.HoTen = model.HoTen;
                giaoVien.NTNS = model.NTNS;
                giaoVien.TrinhDo = model.TrinhDo;

                giaoVien = db.GiaoViens.Add(giaoVien);

                db.SaveChanges();
                GiaoVienModel vienModel = new GiaoVienModel(giaoVien);
                httpActionResult = Ok(vienModel);
            }
            else
            {
                httpActionResult = Ok(err);
            }

            return httpActionResult;
        }

        [HttpPut]
        public IHttpActionResult Update(UpdateGVModel model)
        {
            IHttpActionResult httpActionResult;
            GiaoVien giaoVien = db.GiaoViens.FirstOrDefault(x => x.Id == model.Id);
            if (giaoVien == null)
            {
                err.Add("Không tìm thấy giáo viên");
                httpActionResult = Ok(err);
            }
            else
            {
                giaoVien.MAGV = model.MaGV ?? model.MaGV;
                giaoVien.HoTen = model.HoTen ?? model.HoTen;
                giaoVien.NTNS = model.NTNS ?? model.NTNS;
                giaoVien.TrinhDo = model.TrinhDo ?? model.TrinhDo;

                db.Entry(giaoVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new GiaoVienModel(giaoVien));
            }
            return httpActionResult;
        }
    }
}