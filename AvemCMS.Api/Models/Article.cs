using System.ComponentModel.DataAnnotations.Schema;

namespace AvemCMS.Api.Models
{
    [Table("Articles")] // Ép C# map đúng vào bảng Articles
    public class Article
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("slug")]
        public string Slug { get; set; }

        [Column("excerpt")]
        public string Excerpt { get; set; }

        [Column("content")]
        public string Content { get; set; }

        // Đây là mấu chốt: Khai báo rõ ThumbnailUrl trong C# chính là thumbnail_url trong MySQL
        [Column("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // THÊM VÀO: Các trường Tiếng Trung
        public string? Title_CN { get; set; }
        public string? Excerpt_CN { get; set; }
        public string? Content_CN { get; set; }
    }
}