using Microsoft.AspNetCore.Mvc;
using AvemCMS.Api.Models;
using AvemCMS.Api.Data; // THÊM DÒNG NÀY ĐỂ C# TÌM THẤY FILE TRONG THƯ MỤC DATA
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // 👇 ĐÃ ĐỔI TÊN THÀNH AvemDbContext 👇
        private readonly AvemDbContext _context;

        // 👇 ĐÃ ĐỔI TÊN THÀNH AvemDbContext 👇
        public ContactsController(AvemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContact([FromBody] Contact contact)
        {
            // Kiểm tra xem khách có điền đủ thông tin bắt buộc không
            if (string.IsNullOrEmpty(contact.FullName) || string.IsNullOrEmpty(contact.Phone))
            {
                return BadRequest("Vui lòng điền đầy đủ Tên và Số điện thoại!");
            }

            // Lưu vào MySQL
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Gửi yêu cầu thành công! Chuyên gia AVEM sẽ liên hệ với bạn sớm nhất." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            // Lấy toàn bộ danh sách khách hàng, sắp xếp ngày mới nhất lên đầu tiên
            var contacts = await _context.Contacts
                                         .OrderByDescending(c => c.CreatedAt)
                                         .ToListAsync();
            return Ok(contacts);
        }
    }
}