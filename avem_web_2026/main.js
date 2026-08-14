/**
 * TỆP SCRIPT DÙNG CHUNG CHO TOÀN BỘ DỰ ÁN AVEM
 */

// 1. HÀM TẢI HEADER VÀ FOOTER TỰ ĐỘNG
async function loadComponents() {
    try {
        const headerPlaceholder = document.getElementById('header-placeholder');
        if (headerPlaceholder) {
            // Thêm ?v=... để chống lưu cache của trình duyệt
            const headerRes = await fetch('header.html?v=' + new Date().getTime());
            headerPlaceholder.innerHTML = await headerRes.text();

            // ==========================================
            // LOGIC MỞ MENU HAMBURGER TRÊN MOBILE
            // ==========================================
            const mobileBtn = document.getElementById('mobile-menu-btn');
            const mainNav = document.querySelector('.main-nav');
            
            if (mobileBtn && mainNav) {
                mobileBtn.addEventListener('click', function() {
                    mainNav.classList.toggle('open-mobile');
                    
                    // Đổi icon từ 3 gạch sang dấu X
                    const icon = mobileBtn.querySelector('i');
                    if (mainNav.classList.contains('open-mobile')) {
                        icon.classList.remove('fa-bars');
                        icon.classList.add('fa-xmark');
                    } else {
                        icon.classList.remove('fa-xmark');
                        icon.classList.add('fa-bars');
                    }
                });
            }

            // ==========================================
            // LOGIC BẬT/TẮT TÌM KIẾM TRÊN MOBILE
            // ==========================================
            const searchToggle = document.getElementById('mobile-search-toggle');
            const mobileSearchBar = document.getElementById('mobile-search-bar');
            
            if (searchToggle && mobileSearchBar) {
                searchToggle.addEventListener('click', function() {
                    if (mobileSearchBar.style.display === 'none' || mobileSearchBar.style.display === '') {
                        // Mở thanh tìm kiếm
                        mobileSearchBar.style.display = 'block';
                        // Đổi icon kính lúp thành dấu X cho xịn sò
                        searchToggle.classList.remove('fa-magnifying-glass');
                        searchToggle.classList.add('fa-xmark');
                    } else {
                        // Đóng thanh tìm kiếm
                        mobileSearchBar.style.display = 'none';
                        // Trả lại icon kính lúp
                        searchToggle.classList.remove('fa-xmark');
                        searchToggle.classList.add('fa-magnifying-glass');
                    }
                });
            }

            // ==========================================
            // FIX LỖI "CƯỚP CÒ" CHUYỂN TRANG CỦA MENU XỔ XUỐNG
            // ==========================================
            const dropdownParents = document.querySelectorAll('.main-nav > ul > li');
            
            dropdownParents.forEach(li => {
                const link = li.querySelector('a.nav-link');
                const dropdown = li.querySelector('.dropdown');
                
                // Nếu mục này có menu xổ xuống bên trong
                if (dropdown && link) {
                    link.addEventListener('click', function(e) {
                        // Chỉ can thiệp khi đang xem trên điện thoại
                        if (window.innerWidth <= 768) {
                            // Nếu menu con chưa mở thì chặn chuyển trang
                            if (!li.classList.contains('is-open')) {
                                e.preventDefault(); 
                                
                                // Tắt các menu khác đang mở cho gọn màn hình
                                dropdownParents.forEach(item => {
                                    item.classList.remove('is-open');
                                    const drop = item.querySelector('.dropdown');
                                    if(drop) drop.style.display = 'none';
                                });
                                
                                // Đánh dấu đã mở và hiện cái menu con này lên
                                li.classList.add('is-open');
                                dropdown.style.display = 'block';
                            }
                            // Nếu bấm lần 2 (nghĩa là đã mở menu ra xem rồi) -> Thì cho phép chuyển trang bình thường!
                        }
                    });
                }
            });
            
            // ==========================================
            // LOGIC SCROLL ĐỔI MÀU NỀN HEADER 
            // ==========================================
            const headerElement = document.querySelector('.transparent-header');
            if (headerElement) {
                window.addEventListener('scroll', function() {
                    if (window.scrollY > 50) {
                        headerElement.classList.add('scrolled');
                    } else {
                        headerElement.classList.remove('scrolled');
                    }
                });
            }
        }

        const footerPlaceholder = document.getElementById('footer-placeholder');
        if (footerPlaceholder) {
            const footerRes = await fetch('footer.html?v=' + new Date().getTime());
            footerPlaceholder.innerHTML = await footerRes.text();
        }

        // Bật sáng menu hiện tại
        setActiveMenu();

        // GỌI HÀM NÀY ĐỂ KÍCH HOẠT TỪ ĐIỂN NGAY SAU KHI LOAD HEADER XONG
        checkSavedLanguage(); 

    } catch (error) {
        console.error('Lỗi khi tải giao diện chung:', error);
    }
}

// 2. HÀM KIỂM TRA ĐỊA CHỈ ĐỂ BẬT SÁNG NÚT MENU
function setActiveMenu() {
    const currentPath = window.location.pathname.split('/').pop() || 'index.html';
    const allLinks = document.querySelectorAll('.main-nav a');
    const tinTucGroup = ['chitiet-tintuc.html'];

    allLinks.forEach(link => {
        const linkPath = link.getAttribute('href');
        link.classList.remove('active-link');

        if (currentPath === linkPath) {
            link.classList.add('active-link');
            const parentDropdown = link.closest('.dropdown');
            if (parentDropdown) {
                const parentLink = parentDropdown.previousElementSibling;
                if (parentLink) {
                    parentLink.classList.add('active-link');
                }
            }
        } 
        else if (tinTucGroup.includes(currentPath) && linkPath === 'tintuc.html') {
            link.classList.add('active-link');
        }
    });
}

// 3. HIỆU ỨNG SCROLL REVEAL BẰNG INTERSECTION OBSERVER
function initScrollReveal() {
    const reveals = document.querySelectorAll(".reveal");
    const revealOptions = {
        threshold: 0.15,
        rootMargin: "0px 0px -50px 0px"
    };

    const revealOnScroll = new IntersectionObserver(function (entries, observer) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("active");
                observer.unobserve(entry.target); 
            }
        });
    }, revealOptions);

    reveals.forEach(reveal => {
        revealOnScroll.observe(reveal);
    });
}

// KHỞI CHẠY TẤT CẢ KHI LOAD WEB
document.addEventListener("DOMContentLoaded", function () {
    loadComponents();
    initScrollReveal();
});

// ==================================================
// 4. XỬ LÝ FORM LIÊN HỆ (GỬI VỀ C#)
// ==================================================
const contactForm = document.getElementById('contactForm');
if (contactForm) {
    contactForm.addEventListener('submit', async function(e) {
        e.preventDefault(); 

        const btnSubmit = document.getElementById('btnSubmitContact');
        btnSubmit.innerText = "Đang gửi...";
        btnSubmit.disabled = true;

        const formData = {
            fullName: document.getElementById('cusName').value,
            companyName: document.getElementById('cusCompany').value,
            phone: document.getElementById('cusPhone').value,
            email: document.getElementById('cusEmail').value,
            serviceType: document.getElementById('cusService').value,
            message: document.getElementById('cusMessage').value
        };

        try {
            const response = await fetch("https://localhost:7287/api/contacts", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                alert("🎉 Tuyệt vời! Yêu cầu của bạn đã được gửi tới AVEM.");
                contactForm.reset(); 
            } else {
                const err = await response.text();
                alert("Có lỗi xảy ra: " + err);
            }
        } catch (error) {
            console.error("Lỗi:", error);
            alert("Lỗi kết nối máy chủ! Vui lòng thử lại sau.");
        } finally {
            btnSubmit.innerText = "Gửi Thông Tin Cho AVEM";
            btnSubmit.disabled = false;
        }
    });
}

// ĐỔ DỮ LIỆU BÀI VIẾT (API)
document.addEventListener("DOMContentLoaded", async function() {
    const titleElement = document.getElementById('api-title');
    if (!titleElement) return;

    const urlParams = new URLSearchParams(window.location.search);
    const articleSlug = urlParams.get('slug'); 

    if (!articleSlug) {
        titleElement.innerText = "Lỗi: Không tìm thấy đường dẫn bài viết!";
        return;
    }

    try {
        const response = await fetch(`https://localhost:7287/api/Articles/${articleSlug}`);
        
        if (response.ok) {
            const article = await response.json();
            
            titleElement.innerText = article.title;
            document.getElementById('api-excerpt').innerText = article.excerpt;
            document.getElementById('api-content').innerHTML = article.content;
            
            const coverImg = document.getElementById('api-cover');
            if (article.thumbnailUrl) {
                coverImg.src = article.thumbnailUrl;
                coverImg.style.display = 'block';
            } else {
                coverImg.style.display = 'none'; 
            }

            const catElement = document.getElementById('api-category');
            if (catElement) {
                const categories = {
                    1: "Phân tích Thị trường",
                    2: "Pháp lý & Thuế",
                    3: "Quản trị Doanh nghiệp",
                    4: "Bản tin nội bộ AVEM"
                };
                catElement.innerText = categories[article.categoryId] || "Tiêu Điểm";
            }
            
            const dateElement = document.getElementById('api-date');
            if (dateElement) {
                const rawDate = article.createdAt || article.createdDate || article.date; 
                if (rawDate) {
                    const dateObj = new Date(rawDate);
                    dateElement.innerText = `${dateObj.getDate()}/${dateObj.getMonth() + 1}/${dateObj.getFullYear()}`;
                } else {
                    dateElement.innerText = "Mới cập nhật";
                }
            }
            
        } else {
            titleElement.innerText = "Bài viết không tồn tại hoặc đã bị xóa!";
            document.getElementById('api-content').innerHTML = "";
        }
    } catch (error) {
        console.error("Lỗi:", error);
        titleElement.innerText = "Lỗi kết nối đến máy chủ!";
    }
});

// ==================================================
// 5. HỆ THỐNG ĐA NGÔN NGỮ (LOCAL DICTIONARY)
// ==================================================

// Hàm thực thi việc thay chữ
function applyLanguage(langCode) {
    // Check xem dictionary.js đã được tải vào trang chưa
    if (typeof translations === 'undefined') {
        console.error("Lỗi: Không tìm thấy kho từ vựng. Đảm bảo đã nhúng dictionary.js trước main.js");
        return;
    }
    
    const dict = translations[langCode];
    if (!dict) return;

    // Quét toàn bộ HTML tìm thẻ data-i18n và tráo chữ
    document.querySelectorAll('[data-i18n]').forEach(element => {
        const key = element.getAttribute('data-i18n');
        if (dict[key]) {
            // Nếu là ô input thì đổi placeholder, ngược lại thì đổi chữ bên trong
            if (element.tagName === 'INPUT' && element.hasAttribute('placeholder')) {
                element.setAttribute('placeholder', dict[key]);
            } else {
                element.innerText = dict[key];
            }
        }
    });

    // Sửa lại nhãn ngôn ngữ trên góc phải Header
    const textMap = { 'vi': 'VN', 'zh-CN': 'CN' };
    const langTextEl = document.getElementById('current-lang-text');
    if (langTextEl) langTextEl.innerText = textMap[langCode] || 'VN';

    // Lưu lựa chọn vào bộ nhớ trình duyệt
    localStorage.setItem('avem_lang', langCode);
}

// Tự động khôi phục ngôn ngữ đã chọn khi tải lại trang
function checkSavedLanguage() {
    const savedLang = localStorage.getItem('avem_lang') || 'vi';
    applyLanguage(savedLang);
}

// Logic đóng mở Dropdown và bấm chọn
document.addEventListener('click', function(e) {
    const btnLang = e.target.closest('#btn-lang');
    const langDropdown = document.getElementById('lang-dropdown');

    // Mở menu khi bấm chữ VN
    if (btnLang) {
        if (langDropdown) {
            langDropdown.style.display = langDropdown.style.display === 'block' ? 'none' : 'block';
        }
        return;
    }

    // Khi chọn một ngôn ngữ bất kỳ trong danh sách
    const langOpt = e.target.closest('.lang-opt');
    if (langOpt) {
        e.preventDefault();
        const langCode = langOpt.getAttribute('data-lang');
        applyLanguage(langCode);
        
        if (langDropdown) langDropdown.style.display = 'none';
        return;
    }

    // Bấm ra ngoài nền thì đóng menu
    if (langDropdown && langDropdown.style.display === 'block') {
        langDropdown.style.display = 'none';
    }
});

// Thêm hiệu ứng Parallax cho section cs-hero
document.addEventListener("scroll", function() {
    const hero = document.querySelector('.cs-hero');
    if (hero) {
        let scrollPosition = window.pageYOffset;
        // Đẩy background trượt xuống dưới với tốc độ bằng 50% tốc độ cuộn chuột
        hero.style.backgroundPositionY = (scrollPosition * 0.5) + "px";
    }
});
// ==========================================
// HIỆU ỨNG GÕ CHỮ (TYPEWRITER)
// ==========================================
function initTypewriterEffect() {
    const typewriterElement = document.getElementById('typewriter-slogan');
    if (!typewriterElement) return;

    const phrases = ["Quản Trị Xuất Sắc", "Giải Pháp Tối Ưu", "Đồng Hành Bền Vững"];
    let phraseIndex = 0;
    let charIndex = 0;
    let isDeleting = false;
    let typeSpeed = 100;

    function type() {
        const currentPhrase = phrases[phraseIndex];
        
        if (isDeleting) {
            typewriterElement.textContent = currentPhrase.substring(0, charIndex - 1);
            charIndex--;
            typeSpeed = 50;
        } else {
            typewriterElement.textContent = currentPhrase.substring(0, charIndex + 1);
            charIndex++;
            typeSpeed = 100;
        }

        if (!isDeleting && charIndex === currentPhrase.length) {
            typeSpeed = 2000;
            isDeleting = true;
        } else if (isDeleting && charIndex === 0) {
            isDeleting = false;
            phraseIndex = (phraseIndex + 1) % phrases.length;
            typeSpeed = 500;
        }

        setTimeout(type, typeSpeed);
    }
    setTimeout(type, 1000);
}

// ==========================================
// CHUYỂN TRANG MƯỢT MÀ (PAGE TRANSITIONS)
// ==========================================
function initPageTransitions() {
    document.addEventListener('click', function(e) {
        const link = e.target.closest('a');
        if (!link) return;
        const href = link.getAttribute('href');
        if (!href || href.startsWith('#') || href.startsWith('javascript:') || link.getAttribute('target') === '_blank' || link.classList.contains('open-service-modal')) {
            return;
        }
        e.preventDefault();
        document.body.classList.add('page-fade-out');
        setTimeout(() => {
            window.location.href = href;
        }, 400);
    });

    window.addEventListener('pageshow', function(e) {
        if (document.body.classList.contains('page-fade-out')) {
            document.body.classList.remove('page-fade-out');
        }
    });
}

document.addEventListener('DOMContentLoaded', () => {
    initTypewriterEffect();
    initPageTransitions();
});


// ==========================================
// HIỆU ỨNG GÕ CHỮ (TYPING EFFECT)
// ==========================================
document.addEventListener('DOMContentLoaded', () => {
    const typewriterEl = document.getElementById('typewriter-slogan');
    if (typewriterEl) {
        const slogans = [
            "Vững Bước Thành Công",
            "Nâng Tầm Doanh Nghiệp",
            "Kiến Tạo Giá Trị"
        ];
        let currentSloganIndex = 0;
        let currentCharIndex = 0;
        let isDeleting = false;
        
        typewriterEl.innerHTML = '<span id="type-text"></span><span class="type-cursor">|</span>';
        const typeText = document.getElementById('type-text');
        
        function type() {
            const currentSlogan = slogans[currentSloganIndex];
            
            if (isDeleting) {
                typeText.textContent = currentSlogan.substring(0, currentCharIndex - 1);
                currentCharIndex--;
            } else {
                typeText.textContent = currentSlogan.substring(0, currentCharIndex + 1);
                currentCharIndex++;
            }
            
            let typingSpeed = isDeleting ? 40 : 80;
            
            if (!isDeleting && currentCharIndex === currentSlogan.length) {
                typingSpeed = 2000;
                isDeleting = true;
            } else if (isDeleting && currentCharIndex === 0) {
                isDeleting = false;
                currentSloganIndex = (currentSloganIndex + 1) % slogans.length;
                typingSpeed = 500;
            }
            
            setTimeout(type, typingSpeed);
        }
        
        setTimeout(type, 1000);
    }
});
