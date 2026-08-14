import re

with open(r'd:\WebAnViet\avem_web_2026\pages.css', 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(
    r'\.hero-nexus \{[^}]*\}',
    '.hero-nexus {\n        position: relative;\n        min-height: 100vh;\n        display: flex;\n        align-items: center;\n        justify-content: center;\n        text-align: center;\n        padding: 120px 5% 0;\n        background: url(\'https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?q=80&w=2070\') center/cover no-repeat;\n        color: #ffffff;\n        overflow: hidden;\n      }',
    content
)

content = re.sub(
    r'\.hero-nexus::before \{[^}]*\}',
    '.hero-nexus::before {\n        content: "";\n        position: absolute;\n        inset: 0;\n        background: linear-gradient(rgba(10, 10, 10, 0.4), rgba(10, 10, 10, 0.8));\n        z-index: 1;\n      }',
    content
)

with open(r'd:\WebAnViet\avem_web_2026\pages.css', 'w', encoding='utf-8') as f:
    f.write(content)
