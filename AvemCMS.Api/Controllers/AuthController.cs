using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AvemCMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        // Tiêm IConfiguration để đọc cái SecretKey trong file appsettings.json
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1. KIỂM TRA MẬT KHẨU
            // Tạm thời set cứng tài khoản ở đây cho nhanh (sau này nếu thích bạn có thể tạo bảng Users trong DB sau)
            if (request.Username == "admin" && request.Password == "avem@2026")
            {
                // 2. NẾU ĐÚNG PASS -> BẮT ĐẦU ĐÚC TOKEN
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                // Gắn mác cho cái thẻ từ (Khẳng định thẻ này của Admin)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                    new Claim("QuyenHan", "QuanTriVien")
                };

                // Thiết lập hạn sử dụng của thẻ từ (Ví dụ: 120 phút)
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                // Trả cái mã dài ngoằng đó về cho Frontend
                return Ok(new { message = "Đăng nhập thành công!", token = stringToken });
            }

            // Nếu sai Pass thì đuổi thẳng cổ
            return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu!" });
        }
    }

    // Class phụ để hứng dữ liệu gửi lên từ Frontend
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}