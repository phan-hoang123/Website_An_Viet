using System;
using System.ComponentModel.DataAnnotations;

namespace AvemCMS.Api.Models
{
    public class CaseStudy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } // Tiêu đề dự án

        [Required]
        [MaxLength(255)]
        public string Slug { get; set; } // Đường dẫn thân thiện (VD: tai-cau-truc-tap-doan)

        public string CategoryName { get; set; } // Danh mục (VD: Cố vấn quản trị & Vận hành)

        public string Excerpt { get; set; } // Mô tả ngắn trên Banner

        public string Content { get; set; } // Nội dung chính (Bối cảnh, Giải pháp - Nhập từ CKEditor)

        public string CoverImageUrl { get; set; } // Ảnh minh họa giữa bài

        // --- CỘT TRÁI (THÔNG TIN METADATA) ---
        public string ClientName { get; set; } // Tên khách hàng
        public string Industry { get; set; } // Lĩnh vực
        public string ServicesProvided { get; set; } // Dịch vụ cung cấp
        public string Duration { get; set; } // Thời gian triển khai

        // --- KHỐI KẾT QUẢ (METRICS) ---
        public string Metric1Value { get; set; } // VD: "+35%"
        public string Metric1Label { get; set; } // VD: "Hiệu Suất Vận Hành"

        public string Metric2Value { get; set; }
        public string Metric2Label { get; set; }

        public string Metric3Value { get; set; }
        public string Metric3Label { get; set; }

        public string Conclusion { get; set; } // Lời bình chốt hạ cuối bài

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // THÊM VÀO DƯỚI ĐÂY: Các trường Tiếng Trung
        public string? Title_CN { get; set; }
        public string? CategoryName_CN { get; set; }
        public string? Excerpt_CN { get; set; }
        public string? Content_CN { get; set; }
        public string? Conclusion_CN { get; set; }
    }
}