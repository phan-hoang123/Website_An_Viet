using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        // Tiêm IWebHostEnvironment để lấy đường dẫn ổ cứng của Server
        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("image")]
        [Authorize] // Chỉ Admin có thẻ Token mới được phép upload ảnh
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Kiểm tra xem có file nào được gửi lên không
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Không tìm thấy file ảnh hợp lệ!" });

            // Xác định thư mục lưu ảnh (sẽ tự động tạo thư mục wwwroot/uploads nếu chưa có)
            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Đổi tên file để tránh trùng lặp (Thêm mã ngẫu nhiên Guid vào trước tên gốc)
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Copy file từ luồng mạng vào thẳng ổ cứng Server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Tạo đường dẫn URL đầy đủ để trả về cho Frontend hiển thị
            var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{uniqueFileName}";

            return Ok(new { url = imageUrl, message = "Upload ảnh thành công!" });
        }
    }
}