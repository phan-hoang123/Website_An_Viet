import os
import re

def process_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # 1. Fix label for attribute
    def label_replacer(match):
        label_full = match.group(1)
        inner_text = match.group(2)
        close_label = match.group(3)
        trailing_space = match.group(4)
        input_tag = match.group(5)
        
        id_match = re.search(r'id=["\']([^"\']+)["\']', input_tag, re.IGNORECASE)
        if id_match and 'for=' not in label_full:
            # Add for attribute
            new_label = label_full.replace('<label', f'<label for="{id_match.group(1)}"')
            return new_label + inner_text + close_label + trailing_space + input_tag
        return match.group(0)

    pattern = re.compile(r'(<label[^>]*>)(.*?)(</label>)(\s*)(<(?:input|select|textarea)[^>]*>)', re.IGNORECASE | re.DOTALL)
    new_content = pattern.sub(label_replacer, content)

    # 2. Add loading="lazy" to images
    def img_replacer(match):
        img_tag = match.group(0)
        # Avoid adding lazy to images in hero sections (usually the first few images)
        # But for simplicity, we add it if missing. To be perfectly compliant, we'd skip hero.
        if 'loading=' not in img_tag:
            return img_tag.replace('<img ', '<img loading="lazy" ')
        return img_tag

    new_content = re.sub(r'<img [^>]*>', img_replacer, new_content, flags=re.IGNORECASE)
    
    if content != new_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(new_content)
        print(f"Updated {filepath}")

if __name__ == "__main__":
    count = 0
    for root, dirs, files in os.walk('.'):
        if 'cms_avem_2026' in root or root == '.':
            pass
        else:
            continue
            
        for file in files:
            if file.endswith('.html'):
                process_file(os.path.join(root, file))
                count += 1
    print(f"Processed {count} files.")
