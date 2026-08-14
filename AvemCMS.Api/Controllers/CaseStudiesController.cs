using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AvemCMS.Api.Data;
using AvemCMS.Api.Models;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseStudiesController : ControllerBase
    {
        private readonly AvemDbContext _context;

        public CaseStudiesController(AvemDbContext context)
        {
            _context = context;
        }

        // 1. API Lấy danh sách TẤT CẢ Case Study (Dành cho trang duan.html)
        // GET: api/casestudies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseStudy>>> GetCaseStudies()
        {
            // Lấy danh sách và sắp xếp bài mới nhất lên đầu
            return await _context.CaseStudies
                                 .OrderByDescending(c => c.CreatedAt)
                                 .ToListAsync();
        }

        // 2. API Lấy CHI TIẾT 1 Case Study dựa vào Slug (Dành cho trang casestudy.html)
        // GET: api/casestudies/tai-cau-truc-tap-doan
        [HttpGet("{slug}")]
        public async Task<ActionResult<CaseStudy>> GetCaseStudy(string slug)
        {
            var caseStudy = await _context.CaseStudies
                                          .FirstOrDefaultAsync(c => c.Slug == slug);

            if (caseStudy == null)
            {
                return NotFound(new { message = "Không tìm thấy dự án!" });
            }

            return caseStudy;
        }

        // 3. API TẠO MỚI CASE STUDY (Dành cho trang Admin)
        // POST: api/casestudies
        [HttpPost]
        public async Task<ActionResult<CaseStudy>> PostCaseStudy(CaseStudy caseStudy)
        {
            // Tự động gán thời gian tạo là lúc ấn nút đăng
            caseStudy.CreatedAt = DateTime.Now;

            // Thêm vào Database và lưu lại
            _context.CaseStudies.Add(caseStudy);
            await _context.SaveChangesAsync();

            // Trả về kết quả báo thành công
            return CreatedAtAction("GetCaseStudy", new { slug = caseStudy.Slug }, caseStudy);
        }
    }
}