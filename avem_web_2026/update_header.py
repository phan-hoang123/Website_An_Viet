import re

with open('global.css', 'r', encoding='utf-8') as f:
    content = f.read()

new_header_css = '''
/* =========================================================
   HEADER TRONG SUỐT (TRANSPARENT HEADER) - THIẾT KẾ PREMIUM
   ========================================================= */
.transparent-header {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 100;
  background: linear-gradient(to bottom, rgba(0,0,0,0.6) 0%, rgba(0,0,0,0) 100%);
  padding: 12px 5%;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
}

.header-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  max-width: 1400px;
  margin: 0 auto;
  gap: 30px;
}

/* ---------------- TRÁI: LOGO AVEM ---------------- */
.header-left .logo { text-decoration: none; display: flex; flex-direction: column; }
.header-left .brand-name {
  font-size: 2.1rem;
  font-weight: 800;
  letter-spacing: -0.5px;
  color: #ffffff;
  margin: 0;
  line-height: 1;
}
.header-left .brand-slogan {
  color: var(--gold, #d4af37);
  font-size: 0.75rem;
  font-weight: 700;
  letter-spacing: 1.5px;
  margin-top: 4px;
  text-transform: uppercase;
}

/* ---------------- GIỮA: MENU ĐIỀU HƯỚNG ---------------- */
.header-center ul {
  display: flex;
  gap: 40px;
  align-items: center;
  list-style: none;
  margin: 0;
  padding: 0;
}
.header-center .nav-link {
  color: #ffffff;
  font-weight: 500;
  text-transform: uppercase;
  text-decoration: none;
  font-size: 0.85rem;
  letter-spacing: 1px;
  position: relative;
  padding: 8px 0;
  transition: all 0.3s;
}
.header-center .nav-link::after {
  content: "";
  position: absolute;
  bottom: 0;
  left: 50%;
  width: 0;
  height: 1px;
  background: var(--gold, #d4af37);
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  transform: translateX(-50%);
}
.header-center .nav-link:hover::after { width: 100%; }
.header-center .nav-link:hover { color: #ffffff; text-shadow: 0 0 10px rgba(255,255,255,0.3); }

/* Dropdown Menu */
.header-center li { position: relative; }
.header-center .dropdown {
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%) translateY(20px);
  background: rgba(255, 255, 255, 0.98);
  backdrop-filter: blur(12px);
  padding: 10px;
  min-width: 220px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.1);
  border-radius: 12px;
  border: 1px solid rgba(0,0,0,0.05);
  display: flex;
  flex-direction: column;
  gap: 2px;
  opacity: 0;
  visibility: hidden;
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
}
.header-center li:hover .dropdown {
  opacity: 1;
  visibility: visible;
  transform: translateX(-50%) translateY(0);
}
.header-center .dropdown a {
  color: #333333;
  padding: 12px 16px;
  display: block;
  font-weight: 500;
  font-size: 0.9rem;
  border-radius: 6px;
  transition: all 0.2s;
  text-decoration: none;
}
.header-center .dropdown a:hover {
  background: rgba(0, 0, 0, 0.04);
  color: var(--primary-color, #900000);
  transform: translateX(4px);
}

/* ---------------- PHẢI: HOTLINE, NGÔN NGỮ, TÌM KIẾM ---------------- */
.header-right {
  display: flex;
  align-items: center;
  gap: 25px;
}

.header-hotline { color: #ffffff; font-weight: 600; font-size: 0.95rem; display: flex; align-items: center; gap: 8px; letter-spacing: 0.5px; }

.header-lang {
  position: relative;
  border-left: 1px solid rgba(255, 255, 255, 0.2);
  padding-left: 20px;
}
.lang-toggle { color: #ffffff; font-weight: 600; font-size: 0.9rem; cursor: pointer; display: flex; align-items: center; gap: 5px; }
.lang-dropdown-menu { 
  display: none; 
  position: absolute; 
  top: 150%; 
  right: 0; 
  background: #ffffff; 
  padding: 10px 0; 
  border-radius: 8px; 
  min-width: 130px; 
  box-shadow: 0 10px 25px rgba(0,0,0,0.1); 
  z-index: 9999; 
  list-style: none; 
}
.lang-dropdown-menu a { color: #333; padding: 10px 15px; display: block; text-decoration: none; font-weight: 500; font-size: 0.9rem; transition: 0.2s;}
.lang-dropdown-menu a:hover { color: var(--primary-color); background: rgba(0,0,0,0.03); }

/* Thanh Tìm kiếm Thiết kế Tối giản */
.header-search form {
  display: flex;
  align-items: center;
  background: transparent;
  border-bottom: 1px solid rgba(255, 255, 255, 0.4);
  padding: 4px 0;
  transition: border-color 0.3s;
}
.header-search form:focus-within { border-bottom-color: var(--gold, #d4af37); }
.header-search input { 
  border: none; 
  background: transparent; 
  outline: none; 
  font-size: 0.85rem; 
  width: 130px; 
  color: #ffffff; 
  padding-left: 5px;
}
.header-search input::placeholder { color: rgba(255, 255, 255, 0.6); }
.header-search button { 
  background: transparent; 
  color: #ffffff; 
  border: none; 
  cursor: pointer; 
  display: flex; 
  align-items: center; 
  justify-content: center; 
  transition: 0.3s; 
  font-size: 0.9rem;
  padding: 0 5px;
}
.header-search button:hover { color: var(--gold, #d4af37); }

/* Các nút ẩn trên Desktop */
.mobile-only { display: none; }
.mobile-search-bar { display: none; }

/* Responsive */
@media (max-width: 1250px) {
  .header-container { gap: 15px; }
  .header-center ul { gap: 20px; }
  .header-search input { width: 100px; }
}
@media (max-width: 992px) {
  .header-center, .header-search, .header-hotline { display: none; }
  .mobile-only { display: flex; color: #ffffff; font-size: 1.4rem; cursor: pointer; align-items: center; justify-content: center; width: 40px; height: 40px;}
  .header-lang { border-left: none; padding-left: 0; }
  .header-right { gap: 15px; }
}
'''

pattern = re.compile(r'/\* =========================================================\s*HEADER TRONG SUỐT \(TRANSPARENT HEADER\) - Đặt trong global\.css\s*========================================================= \*/.*?@media \(max-width: 992px\) \{.*?\n}', re.DOTALL)

if pattern.search(content):
    content = pattern.sub(new_header_css.strip(), content)
    with open('global.css', 'w', encoding='utf-8') as f:
        f.write(content)
    print("SUCCESS")
else:
    print("PATTERN NOT FOUND")
