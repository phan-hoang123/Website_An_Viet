import os

css_to_append = """

/* === RESPONSIVE HERO BANNER (THU NHỎ THEO MÀN HÌNH) === */
@media (max-width: 1200px) {
  .hero-nexus {
    min-height: 80vh;
  }
}

@media (max-width: 992px) {
  .hero-nexus {
    min-height: 70vh;
    padding-top: 100px;
  }
  .hero-nexus h1 {
    font-size: 2.5rem;
  }
}

@media (max-width: 768px) {
  .hero-nexus {
    min-height: 55vh;
    padding-top: 80px;
  }
  .hero-nexus h1 {
    font-size: 2rem;
  }
  .hero-nexus .lead {
    font-size: 1rem;
  }
}
"""

with open('pages.css', 'a', encoding='utf-8') as f:
    f.write(css_to_append)
