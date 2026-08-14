
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
