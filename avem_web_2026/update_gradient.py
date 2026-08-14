import re

with open('pages.css', 'r', encoding='utf-8') as f:
    content = f.read()

replacement = '''
.hero-nexus::before {
  content: "";
  position: absolute;
  inset: 0;
  background: linear-gradient(45deg, rgba(10,10,10,0.8), rgba(10,10,10,0.3), rgba(40,10,10,0.6));
  background-size: 200% 200%;
  animation: gradientMove 8s ease infinite;
  z-index: 1;
}

@keyframes gradientMove {
  0% { background-position: 0% 50%; }
  50% { background-position: 100% 50%; }
  100% { background-position: 0% 50%; }
}
'''
content = re.sub(r'\.hero-nexus::before\s*\{[^}]*\}', replacement.strip(), content)

with open('pages.css', 'w', encoding='utf-8') as f:
    f.write(content)
