Using [Normalization.py](Normalization.py)

# Normalized_quran.json
```json
[
    {
        "id": 1,
        "name": "الفاتحة",
        "transliteration": "Al-Fatihah",
        "type": "meccan",
        "total_verses": 7,
        "verses": [
            {
                "id": 1,
                "text": "بِسۡمِ ٱللَّهِ ٱلرَّحۡمَٰنِ ٱلرَّحِيمِ"
            }
        ]
    }
]
```
Becomes (but minified):
```json
[
    {
        "s":1,
        "n":"الفاتحة",
        "l":"Al-Fatihah",
        "p":"meccan",
        "v":7,
        "x":[
            {"a":1,
            "t":"بِسۡمِ ٱللَّهِ ٱلرَّحۡمَٰنِ ٱلرَّحِيمِ",
            "m":"بسمللهلرحمنلرحيم"
            }
        ]
    }
]
```

# Normalized_ayahs.json
```json
[
    {
        "surah_id": 1,
        "ayah_id": 1,
        "text": "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
        "surah_name": "الفاتحة"
    }
]
```
Becomes (but minified):
```json
[
    {
        "s":1,
        "a":1,
        "t":"بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
        "n":"الفاتحة",
        "m":"بسماللهالرحمنالرحيم"
    }
]
```
