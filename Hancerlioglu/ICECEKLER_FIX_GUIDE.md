# Ýçecekler Sayfasý Sorun Giderme Kýlavuzu

## ?? Tespit Edilen Sorunlar

### 1. **Soru Ýþareti (?) Problemleri**

#### Problem:
- Fiyatlarda "45 ?" gibi görüntü
- Baþlýklarda "Ýçecekler ?" 
- Icon'lar yerine "?" karakterleri

#### Kök Neden:
1. **Encoding Sorunu**: Dosya UTF-8 BOM formatýnda deðil
2. **Browser Cache**: Eski versiyonlar cache'de kalmýþ
3. **Font Awesome**: Düzgün yüklenememiþ olabilir

---

## ? Çözüm Adýmlarý

### Adým 1: Browser Cache Temizleme

#### **Manuel Yöntem:**
1. `Ctrl + Shift + Delete` tuþlarýna basýn
2. "Önbelleðe alýnan resimler ve dosyalar" seçeneðini iþaretleyin
3. "Verileri temizle" butonuna týklayýn
4. Sayfayý `Ctrl + F5` ile yeniden yükleyin

#### **Otomatik Yöntem:**
Yeni eklenen `cache-buster.js` scripti otomatik olarak cache'i temizleyecek.

```javascript
// wwwroot/js/cache-buster.js
const APP_VERSION = '1.0.1'; // Bu sayýyý artýrýn
```

---

### Adým 2: Encoding Kontrolü

#### **PowerShell ile Kontrol:**
```powershell
cd Hancerlioglu
Get-Content -Path "Views\Home\Icecekler.cshtml" -Encoding UTF8 | Select-String "?"
```

#### **Visual Studio'da Düzeltme:**
1. `Icecekler.cshtml` dosyasýný açýn
2. Üstteki menüden: **File ? Advanced Save Options**
3. **Encoding:** `Unicode (UTF-8 with signature) - Codepage 65001`
4. Dosyayý kaydedin

---

### Adým 3: Font Awesome Kontrolü

#### **_Layout.cshtml'de Kontrol:**
```html
<!-- Bu satýrýn olduðundan emin olun -->
<link rel="preload" 
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" 
      as="style" 
      onload="this.onload=null;this.rel='stylesheet'">
```

#### **Test:**
Browser console'da çalýþtýrýn:
```javascript
console.log(getComputedStyle(document.querySelector('.fa-coffee'), ':before').content);
```

Eðer `""` (boþ) dönerse Font Awesome yüklenmemiþtir.

---

## ?? Hýzlý Düzeltme Scripti

Aþaðýdaki PowerShell scriptini çalýþtýrýn:

```powershell
# UTF-8 BOM ile yeniden kaydet
$file = "Hancerlioglu\Views\Home\Icecekler.cshtml"
$content = Get-Content -Path $file -Raw -Encoding UTF8
$utf8BOM = New-Object System.Text.UTF8Encoding $true
[System.IO.File]::WriteAllText($file, $content, $utf8BOM)

Write-Host "? Encoding düzeltildi!" -ForegroundColor Green
```

---

## ?? Checklist

### **Sayfayý Test Etmeden Önce:**

- [ ] Browser cache temizlendi
- [ ] Sayfa `Ctrl + F5` ile yenilendi
- [ ] `Icecekler.cshtml` UTF-8 BOM formatýnda
- [ ] Font Awesome CDN linki doðru
- [ ] `cache-buster.js` yükleniyor
- [ ] Console'da hata yok

### **Kontrol Edilecek Noktalar:**

```javascript
// Browser Console'da çalýþtýrýn
console.log('=== Ýçecekler Sayfa Kontrolü ===');
console.log('Font Awesome:', typeof FontAwesome !== 'undefined' ? '?' : '?');
console.log('jQuery:', typeof jQuery !== 'undefined' ? '?' : '?');
console.log('Bootstrap:', typeof bootstrap !== 'undefined' ? '?' : '?');

// TL simgesi kontrolü
const prices = document.querySelectorAll('.drink-price');
console.log(`Fiyat elementleri: ${prices.length} adet`);
prices.forEach((el, i) => {
    console.log(`Fiyat ${i+1}: "${el.textContent}"`);
});

// Icon kontrolü
const icons = document.querySelectorAll('.drink-icon');
console.log(`Icon elementleri: ${icons.length} adet`);
icons.forEach((el, i) => {
    const style = window.getComputedStyle(el, ':before');
    console.log(`Icon ${i+1}:`, style.content !== '""' ? '?' : '?');
});
```

---

## ?? Detaylý Hata Ayýklama

### **Sorun Devam Ediyorsa:**

#### 1. Network Tab Kontrolü:
- F12 ? Network tab
- Sayfayý yenileyin
- `font-awesome` arayýn
- Status: **200** olmalý

#### 2. Console Hatalarý:
```
? CORS error ? CDN engellenmiþ
? 404 Not Found ? Dosya yolu yanlýþ
? Failed to load ? Ýnternet baðlantýsý
```

#### 3. Encoding Doðrulama:
```powershell
# Dosya encoding'ini göster
$path = "Hancerlioglu\Views\Home\Icecekler.cshtml"
$bytes = [System.IO.File]::ReadAllBytes($path)
if ($bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
    Write-Host "? UTF-8 BOM" -ForegroundColor Green
} else {
    Write-Host "? Encoding sorunu!" -ForegroundColor Red
}
```

---

## ?? Kesin Çözüm (Son Çare)

Eðer hiçbir þey iþe yaramazsa:

### **1. Dosyayý Yeniden Oluþtur:**
```powershell
# Backup al
Copy-Item "Hancerlioglu\Views\Home\Icecekler.cshtml" `
          "Hancerlioglu\Views\Home\Icecekler.cshtml.backup"

# Yeni dosya oluþtur (UTF-8 BOM ile)
New-Item -Path "Hancerlioglu\Views\Home\Icecekler.cshtml" -ItemType File -Force
# Ýçeriði yapýþtýr ve kaydet
```

### **2. IIS Express'i Yeniden Baþlat:**
```powershell
# IIS Express'i durdur
Stop-Process -Name "iisexpress" -Force -ErrorAction SilentlyContinue

# Projeyi yeniden çalýþtýr
dotnet run --project Hancerlioglu\Hancerlioglu.csproj
```

### **3. Temp Dosyalarýný Temizle:**
```powershell
# Obj ve bin klasörlerini sil
Remove-Item -Path "Hancerlioglu\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "Hancerlioglu\obj" -Recurse -Force -ErrorAction SilentlyContinue

# Yeniden build
dotnet build Hancerlioglu\Hancerlioglu.csproj
```

---

## ?? Hala Çalýþmýyorsa

### **Debug Modu:**
`Icecekler.cshtml` dosyasýnýn en altýna ekleyin:

```html
<script>
document.addEventListener('DOMContentLoaded', function() {
  console.log('=== DEBUG INFO ===');
    console.log('Page Title:', document.title);
    console.log('Font Awesome Loaded:', !!window.FontAwesome);
    
    const testIcon = document.querySelector('.fa-coffee');
    if (testIcon) {
      const computed = window.getComputedStyle(testIcon, ':before');
        console.log('Coffee Icon Content:', computed.content);
        console.log('Font Family:', computed.fontFamily);
    }
    
    const prices = Array.from(document.querySelectorAll('.drink-price'));
    console.log('Prices:', prices.map(el => el.textContent));
});
</script>
```

Bu bilgileri screenshot ile paylaþýn.

---

## ? Baþarý Kriterleri

Sayfa düzgün çalýþýyorsa:

1. ? Tüm fiyatlar "XX ?" formatýnda
2. ? Icon'lar görünüyor (? ?? ??)
3. ? Türkçe karakterler doðru (Ý, Ç, Þ, Ð, Ü, Ö)
4. ? Dark mode çalýþýyor
5. ? Responsive tasarým aktif
6. ? Console'da hata yok

---

**Son Güncelleme:** 2025-01-XX  
**Durum:** ? Çözüldü / ? Ýþlemde / ? Sorun Devam Ediyor
