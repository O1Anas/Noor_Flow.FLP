import json
import re

# Function to normalize "text" key into "m" key (minified version of "n")
def normalize(text):
    if not text:
        return ""

    # Remove occurrences of "وٰ" before normalizing
    text = re.sub("وٰ", "", text)

    # Apply normalization: remove diacritics, normalize Arabic characters, remove spaces
    text = re.sub(r"[\u0621\u0622\u0623\u0625\u0626\u0627\u0670-\u0675\u064B-\u0652\u06D6-\u06DC\u06DF-\u06E8\u06EA-\u06ED]|\s|[^ء-ي]", "", text)

    return text

# Transform and rename fields
transformed_data = []
for surah in data:
    transformed_surah = {
        "s": surah["id"],
        "n": surah["name"],
        "l": surah["transliteration"],
        "p": surah["type"],
        "v": surah["total_verses"],
        "x": []
    }
    
    for verse in surah["verses"]:
        transformed_surah["x"].append({
            "a": verse["id"],
            "t": verse["text"],
            "m": normalize(verse["text"])
        })
    
    transformed_data.append(transformed_surah)

# Save the modified JSON in a minified format
output_file = "/mnt/data/normalized_quran_minified.json"
with open(output_file, "w", encoding="utf-8") as file:
    json.dump(transformed_data, file, ensure_ascii=False, separators=(',', ':'))

output_file