using AvemCMS.Api.Data;
using AvemCMS.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AvemDbContext _context;

        public ArticlesController(AvemDbContext context)
        {
            _context = context;
        }

        // GET: api/articles (API lấy danh sách tất cả bài viết đã xuất bản)
        [HttpGet]
        public async Task<IActionResult> GetPublishedArticles()
        {
            var articles = await _context.Articles
                .Where(a => a.Status == "Published")
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new {
                    a.Id,
                    a.Title,
                    a.Slug,
                    a.CategoryId,
                    a.Excerpt,
                    a.ThumbnailUrl,
                    a.CreatedAt
                })
                .ToListAsync();

            return Ok(articles);
        }

        // POST: api/articles (API Thêm bài viết mới từ trang Admin)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateArticle([FromBody] Article newArticle)
        {
            // Tự động gán thời gian tạo là thời điểm hiện tại
            newArticle.CreatedAt = DateTime.Now;

            // Nếu không truyền status, mặc định là Draft (Bản nháp)
            if (string.IsNullOrEmpty(newArticle.Status))
            {
                newArticle.Status = "Draft";
            }

            // Thêm dữ liệu vào bộ nhớ tạm của C#
            _context.Articles.Add(newArticle);

            // Lưu chính thức xuống Database MySQL
            await _context.SaveChangesAsync();

            // Trả về thông báo thành công và dữ liệu vừa tạo
            return Ok(new { message = "Thêm bài viết thành công!", data = newArticle });
        }

        // GET: api/articles/{slug} (API lấy chi tiết 1 bài viết để đọc)
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetArticleBySlug(string slug)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Slug == slug && a.Status == "Published");

            if (article == null) return NotFound("Bài viết không tồn tại hoặc đã bị ẩn.");

            return Ok(article);
        }
    }
}