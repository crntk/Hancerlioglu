# ?? Instagram Feed Kurulum Rehberi

## ? Yapýlanlar

Anasayfanýza **Instagram Feed** bölümü eklendi! Müþteri yorumlarýndan hemen sonra görünecek.

### ?? Özellikler:
- ? Modern ve responsive tasarým
- ? Swiper carousel ile kaydýrýlabilir
- ? Instagram renk temasý (pembe gradyan)
- ? Mobil uyumlu
- ? "Takip Et" butonu
- ? Placeholder mesajý (post yoksa görünür)

---

## ?? Instagram Postlarýný Nasýl Ekleyebilirsiniz?

### **Yöntem 1: Manuel Post Ekleme (Önerilen - Ücretsiz)**

1. **Instagram'dan Post URL'lerini Alýn:**
   - Instagram'da paylaþmak istediðiniz postlarý açýn
   - Tarayýcýdaki URL'yi kopyalayýn
   - Örnek: `https://www.instagram.com/p/ABC123xyz/`

2. **AnaSayfa.cshtml dosyasýný açýn:**
   - Dosya yolu: `Hancerlioglu\Views\Home\AnaSayfa.cshtml`
   - En altta bulunan JavaScript kodunu bulun (satýr ~800)

3. **Post URL'lerini Ekleyin:**
   ```javascript
function loadInstagramPosts() {
       var instagramPosts = [
   'https://www.instagram.com/p/POST_ID_1/',
 'https://www.instagram.com/p/POST_ID_2/',
     'https://www.instagram.com/p/POST_ID_3/',
           'https://www.instagram.com/p/POST_ID_4/',
           'https://www.instagram.com/p/POST_ID_5/',
      'https://www.instagram.com/p/POST_ID_6/'
     ];
       // ... kodun devamý
   ```

4. **Kaydedin ve Sayfayý Yenileyin!**

---

## ?? Örnek Kullaným

```javascript
function loadInstagramPosts() {
    var instagramPosts = [
        'https://www.instagram.com/p/DDDXjabOsLU/',  // Baklava postu
        'https://www.instagram.com/p/ABC123xyz/',    // Künefe postu
        'https://www.instagram.com/p/XYZ789abc/'     // Kadayýf postu
    ];
 
    // ... kodun devamý (deðiþtirmeyin)
```

---

## ?? Önemli Notlar

1. **Post Sayýsý:** Ýstediðiniz kadar post ekleyebilirsiniz (önerilen: 6-12 adet)
2. **Sýralama:** Array'deki sýra carousel'de görünme sýrasýdýr
3. **Güncelleme:** Postlarý düzenli güncelleyin, yeni ürünlerinizi ekleyin
4. **Virgül:** Son post'tan sonra virgül koymayýn!

---

## ?? Alternatif Yöntemler (Geliþmiþ)

### **Yöntem 2: Instagram Graph API (Otomatik)**
- Facebook Page ve Instagram Business hesabý gerektirir
- Postlar otomatik olarak çekilir
- Token yönetimi gerekir
- Ücretli deðil ama kurulum karmaþýk

### **Yöntem 3: 3. Parti Servisler**
- Juicer.io, Elfsight, Flockler gibi servisler
- Kolay kurulum
- Ücretli abonelik gerektirir

---

## ?? Tasarýmý Özelleþtirme

Instagram bölümünün stillerini deðiþtirmek için `wwwroot\css\style.css` dosyasýnda:

```css
/* Instagram Section - Satýr ~1000 */
.instagram-section {
    padding: 100px 0;
    background: linear-gradient(135deg, #fafafa 0%, #f5f5f5 100%);
}

.instagram-follow-btn {
    background: linear-gradient(135deg, #e1306c, #fd1d1d, #fcaf45);
    /* Renkleri buradan deðiþtirebilirsiniz */
}
```

---

## ? Test Etme

1. Projeyi çalýþtýrýn: `dotnet run`
2. Anasayfaya gidin: `https://localhost:XXXX/Home/AnaSayfa`
3. Aþaðý kaydýrýn - Müþteri yorumlarýndan sonra Instagram bölümünü göreceksiniz
4. Post eklemediyseniz placeholder mesajý görünür
5. Post eklediyseniz carousel çalýþýr

---

## ?? Sorun Giderme

### Post Görünmüyor
- URL'lerin doðru formatta olduðundan emin olun
- Tarayýcý konsolunu kontrol edin (F12)
- Postlarýn public olduðundan emin olun

### Carousel Çalýþmýyor
- Swiper JS yüklendiðinden emin olun
- JavaScript hatalarýný konsola bakarak kontrol edin

### Stil Sorunlarý
- CSS dosyasýnýn doðru yüklendiðinden emin olun
- Cache temizleyin (Ctrl+F5)

---

## ?? Ýletiþim

Herhangi bir sorun yaþarsanýz:
- Browser konsolunu kontrol edin (F12 > Console)
- Network tab'ýndan Instagram URL'lerinin yüklenip yüklenmediðini kontrol edin

---

## ?? Tamamlandý!

Instagram feed'iniz hazýr! Artýk müþterileriniz sosyal medya içeriklerinizi doðrudan web sitenizden görebilir.

**Instagram Hesabýnýz:** @gaziantepli_hancerlioglu
