import re

# 1. Update pages.css
with open('pages.css', 'r', encoding='utf-8') as f:
    pages_css = f.read()

# Thay thế hero-nexus
pattern_hero = re.compile(r'\.hero-nexus\s*\{[^}]*\}', re.DOTALL)
new_hero = '''
.hero-nexus {
  position: relative;
  max-width: 1440px;
  width: calc(100% - 30px);
  margin: 15px auto 40px;
  border-radius: 24px;
  aspect-ratio: 2.2 / 1;
  min-height: 500px;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  padding: 100px 5% 0;
  background: url('https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?q=80&w=2070') center/cover no-repeat;
  color: #ffffff;
  overflow: hidden;
  box-shadow: 0 20px 40px rgba(0,0,0,0.2);
}
'''.strip()
pages_css = pattern_hero.sub(new_hero, pages_css, count=1)

# Thay thế hero-nexus::before (thêm border-radius)
pattern_before = re.compile(r'\.hero-nexus::before\s*\{[^}]*\}', re.DOTALL)
new_before = '''
.hero-nexus::before {
  content: "";
  position: absolute;
  inset: 0;
  background: linear-gradient(45deg, rgba(10,10,10,0.8), rgba(10,10,10,0.3), rgba(40,10,10,0.6));
  background-size: 200% 200%;
  animation: gradientMove 8s ease infinite;
  z-index: 1;
  border-radius: 24px;
}
'''.strip()
pages_css = pattern_before.sub(new_before, pages_css, count=1)

with open('pages.css', 'w', encoding='utf-8') as f:
    f.write(pages_css)


# 2. Update global.css
with open('global.css', 'r', encoding='utf-8') as f:
    global_css = f.read()

pattern_header = re.compile(r'\.transparent-header\s*\{[^}]*\}', re.DOTALL)
new_header = '''
.transparent-header {
  position: absolute;
  top: 15px;
  left: 0;
  right: 0;
  margin: 0 auto;
  max-width: 1440px;
  width: calc(100% - 30px);
  z-index: 100;
  background: linear-gradient(to bottom, rgba(0,0,0,0.6) 0%, rgba(0,0,0,0) 100%);
  padding: 12px 5%;
  border-radius: 24px 24px 0 0;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
}
'''.strip()
global_css = pattern_header.sub(new_header, global_css, count=1)

with open('global.css', 'w', encoding='utf-8') as f:
    f.write(global_css)

print("SUCCESS")
