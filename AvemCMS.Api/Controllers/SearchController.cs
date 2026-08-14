using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AvemCMS.Api.Data;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AvemDbContext _context;

        public SearchController(AvemDbContext context)
        {
            _context = context;
        }

        // GET: api/search?q=từ-khóa
        [HttpGet]
        public async Task<IActionResult> GlobalSearch([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Ok(new List<object>());
            }

            var keyword = q.ToLower();

            // ==========================================
            // 1. TÌM TRONG DATABASE (TIN TỨC & DỰ ÁN)
            // ==========================================
            var articles = await _context.Articles
                .Where(a => a.Title.ToLower().Contains(keyword) || a.Content.ToLower().Contains(keyword))
                .Select(a => new {
                    Title = a.Title,
                    Type = "Tin Tức",
                    Url = $"chitiet-tintuc.html?slug={a.Slug}",
                    Excerpt = a.Excerpt
                }).ToListAsync();

            var caseStudies = await _context.CaseStudies
                .Where(c => c.Title.ToLower().Contains(keyword) || c.Content.ToLower().Contains(keyword))
                .Select(c => new {
                    Title = c.Title,
                    Type = "Dự Án",
                    Url = $"casestudy.html?slug={c.Slug}",
                    Excerpt = c.Excerpt
                }).ToListAsync();


            // ==========================================
            // 2. TÌM TRONG CÁC TRANG HTML TĨNH (DỊCH VỤ, GIỚI THIỆU)
            // ==========================================
            // Sếp có thể bổ sung thêm các trang khác vào danh sách này sau
            var staticPages = new List<dynamic>
            {
                new { Title = "Kiểm toán độc lập", Type = "Dịch Vụ", Url = "kiemtoan.html", Content = "kiểm toán báo cáo tài chính, kiểm toán quyết toán, hệ thống kiểm soát nội bộ, cpa..." },
                new { Title = "Kế toán & Thuế", Type = "Dịch Vụ", Url = "ketoan.html", Content = "dịch vụ kế toán, khai báo thuế, báo cáo tài chính, sổ sách, kế toán trưởng..." },
                new { Title = "Xúc tiến đầu tư (FDI)", Type = "Dịch Vụ", Url = "xuctien.html", Content = "xúc tiến đầu tư fdi, vốn nước ngoài, thành lập công ty, giấy phép đầu tư..." },
                new { Title = "Pháp lý doanh nghiệp", Type = "Dịch Vụ", Url = "phaply.html", Content = "luật doanh nghiệp, tư vấn pháp lý, hợp đồng, giấy phép con, sáp nhập m&a..." },
                new { Title = "Cố vấn quản trị", Type = "Dịch Vụ", Url = "covanquantri.html", Content = "cố vấn quản trị, tái cấu trúc, kpi, hệ thống vận hành, chiến lược kinh doanh..." },
                new { Title = "Lịch sử & Tầm nhìn", Type = "Giới Thiệu", Url = "lichsu.html", Content = "lịch sử hình thành, tầm nhìn, sứ mệnh, giá trị cốt lõi, đội ngũ chuyên gia..." },
                new { Title = "Thư ngỏ từ Ban Giám Đốc", Type = "Giới Thiệu", Url = "thuNgo.html", Content = "thư ngỏ, ban giám đốc, cam kết đồng hành, giá trị..." }
            };

            // ==========================================
            // 2. TÌM TRONG CÁC TRANG HTML TĨNH (Đã ép kiểu string)
            // ==========================================
            var matchedStaticPages = staticPages
                .Where(p => ((string)p.Title).ToLower().Contains(keyword) || ((string)p.Content).ToLower().Contains(keyword))
                .Select(p => new {
                    Title = (string)p.Title,
                    Type = (string)p.Type,
                    Url = (string)p.Url,
                    Excerpt = "Nhấn vào đây để xem chi tiết thông tin dịch vụ..."
                }).ToList();


            // ==========================================
            // 3. GỘP TẤT CẢ LẠI VÀ TRẢ VỀ CHO WEB
            // ==========================================
            var combinedResults = articles
                .Concat(caseStudies)
                .Concat(matchedStaticPages)
                .ToList();

            return Ok(combinedResults);
        }
    }
}