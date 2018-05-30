using Model;
using QLSV.ViewModel;
using System.Linq;
using System.Web.Http;

namespace QLSV.Controllers
{
    public class HocSinhController : ApiController
    {
        private ApiDbContext db;
        private ErrorModel err;

        public HocSinhController()
        {
            db = new ApiDbContext();
            err = new ErrorModel();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = db.HocSinhs.Select(x => new HocSinhModel()
            {
                Id = x.Id,
                MaHS = x.MaHS,
                HoTen = x.HoTen,
                NTNS = x.NTNS,
                Lop = x.Lop.TenLop
            });
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetById(long id)
        {
            IHttpActionResult httpActionResult;
            HocSinh hs = db.HocSinhs.FirstOrDefault(x => x.Id == id);
            if (hs == null)
            {
                err.Add("Không tìm thấy học sinh yêu cầu");
                httpActionResult = Ok(err);
            }
            else
            {
                httpActionResult = Ok(new HocSinhModel(hs));
            }
            return httpActionResult;
        }

        [HttpGet]
        public IHttpActionResult GetByClass(long id)
        {
            var list = db.HocSinhs.Where(x => x.Lop.Id == id).Select(x => new HocSinhModel()
            {
                Id = x.Id,
                MaHS = x.MaHS,
                HoTen = x.HoTen,
                NTNS = x.NTNS,
                Lop = x.Lop.TenLop
            });
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult Create(CreateHSModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.MaHS))
            {
                err.Add("Mã hs là trường bắt buộc");
            }
            if (string.IsNullOrEmpty(model.HoTen))
            {
                err.Add("Tên hs là trường bắt buộc");
            }
            Lop lop = db.Lops.FirstOrDefault(x => x.Id == model.Lop);
            if (lop == null)
            {
                err.Add("Lớp không tồn tại");
            }
            if (err.errors.Count == 0)
            {
                HocSinh hs = new HocSinh();
                hs.MaHS = model.MaHS;
                hs.HoTen = model.HoTen;
                hs.Lop = db.Lops.FirstOrDefault(x => x.Id == model.Lop);
                hs.NTNS = model.NTNS;

                hs = db.HocSinhs.Add(hs);
                db.SaveChanges();

                HocSinhModel viewmodel = new HocSinhModel(hs);
                httpActionResult = Ok(viewmodel);
            }
            else
            {
                httpActionResult = Ok(err);
            }
            return httpActionResult;
        }

        [HttpPut]
        public IHttpActionResult Update(UpdateHSModel model)
        {
            IHttpActionResult httpActionResult;
            HocSinh hs = db.HocSinhs.FirstOrDefault(x => x.Id == model.Id);
            if (hs == null)
            {
                err.Add("Không tìm thấy học sinh");
                httpActionResult = Ok(err);
            }
            else
            {
                hs.MaHS = model.MaHS ?? model.MaHS;
                hs.HoTen = model.HoTen ?? model.HoTen;
                hs.Lop = db.Lops.FirstOrDefault(x => x.Id == model.Lop) ?? db.Lops.FirstOrDefault(x => x.Id == model.Lop);
                hs.NTNS = model.NTNS ?? model.NTNS;

                db.Entry(hs).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new HocSinhModel(hs));
            }
            return httpActionResult;
        }
    }
}