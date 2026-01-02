# Google Places API Entegrasyonu - Kurulum Rehberi

## ?? Google API Key Alma Adýmlarý

### 1. Google Cloud Console'a Giriþ
1. [Google Cloud Console](https://console.cloud.google.com/) adresine gidin
2. Google hesabýnýzla giriþ yapýn

### 2. Yeni Proje Oluþturma
1. Üst menüden "Proje Seç" > "Yeni Proje" týklayýn
2. Proje adý: `Hancerlioglu-Reviews` (veya istediðiniz bir isim)
3. "Oluþtur" butonuna týklayýn

### 3. Places API'yi Aktif Etme
1. Sol menüden "API'ler ve Hizmetler" > "Kitaplýk" seçin
2. Arama kutusuna "Places API" yazýn
3. "Places API" seçin ve "Etkinleþtir" butonuna týklayýn

### 4. API Key Oluþturma
1. Sol menüden "API'ler ve Hizmetler" > "Kimlik Bilgileri" seçin
2. Üst kýsýmdan "+ KÝMLÝK BÝLGÝSÝ OLUÞTUR" > "API anahtarý" seçin
3. API anahtarýnýz oluþturulacak - **Güvenli bir yere kaydedin!**

### 5. API Key'i Kýsýtlama (Önemli!)
1. Oluþturulan API key'in yanýndaki düzenleme ikonuna týklayýn
2. "Uygulama kýsýtlamalarý" bölümünden:
   - "HTTP yönlendirme adresleri" seçin
   - Sitenizin domain'ini ekleyin (örn: `https://yoursite.com/*`)
   - Geliþtirme için: `http://localhost:*` ekleyin
3. "API kýsýtlamalarý" bölümünden:
   - "API'leri kýsýtla" seçin
   - "Places API" seçin
4. "Kaydet" butonuna týklayýn

## ?? Google Place ID Bulma

### Yöntem 1: Place ID Finder (Önerilen)
1. [Place ID Finder](https://developers.google.com/maps/documentation/javascript/examples/places-placeid-finder) adresine gidin
2. Ýþletmenizin adýný arayýn veya haritadan seçin
3. Place ID'yi kopyalayýn (Örnek: `ChIJ...`)

### Yöntem 2: Google Maps URL'den
1. Google Maps'te iþletmenizi bulun
2. URL'deki `!1s` ile baþlayan kýsmý arayýn
3. Örnek: `https://www.google.com/maps/place/...!1sChIJabcdefg...`

### Yöntem 3: Adres ile Arama
1. [Geocoding API](https://developers.google.com/maps/documentation/geocoding/overview) kullanýn
2. Adresinizi girin ve Place ID'yi alýn

## ?? Projeye Entegrasyon

### 1. appsettings.json Güncelleme
```json
{
  "GooglePlaces": {
    "ApiKey": "BURAYA_GOOGLE_API_KEY_YAPIÞTIRIN",
    "PlaceId": "BURAYA_PLACE_ID_YAPIÞTIRIN"
  }
}
```

**Örnek:**
```json
{
  "GooglePlaces": {
    "ApiKey": "AIzaSyB1234567890abcdefghijklmnopqrs",
    "PlaceId": "ChIJN1t_tDeuEmsRUsoyG83frY4"
  }
}
```

### 2. Güvenlik Ýçin appsettings.Development.json
Geliþtirme ortamý için ayrý bir dosya oluþturun:

```json
{
  "GooglePlaces": {
    "ApiKey": "DEVELOPMENT_API_KEY",
    "PlaceId": "PLACE_ID"
  }
}
```

### 3. .gitignore Güncellemesi
API key'lerinizi GitHub'a yüklememeye dikkat edin:

```
appsettings.json
appsettings.Development.json
appsettings.*.json
```

### 4. Ortam Deðiþkenleri (Production için)
Production ortamýnda API key'i environment variable olarak kullanýn:

**Azure App Service:**
1. Azure Portal > App Service > Configuration
2. Application Settings'e ekleyin:
   - Name: `GooglePlaces__ApiKey`
   - Value: `YOUR_API_KEY`

**IIS:**
```xml
<environmentVariables>
  <environmentVariable name="GooglePlaces__ApiKey" value="YOUR_API_KEY" />
  <environmentVariable name="GooglePlaces__PlaceId" value="YOUR_PLACE_ID" />
</environmentVariables>
```

## ?? Kullaným

### API Endpoint Test
Tarayýcýnýzda test edin:
```
http://localhost:5000/api/reviews
```

Baþarýlý yanýt:
```json
{
  "name": "HANÇERLÝOÐLU Baklava",
  "rating": 4.8,
  "userRatingsTotal": 256,
  "formattedAddress": "Noter Çaprazý, Merkez, Türbe Cd. No:71 H/4, 33730 Erdemli/Mersin",
  "reviews": [...]
}
```

### Fallback Mekanizmasý
- API key yoksa veya hata varsa, otomatik olarak statik yorumlar gösterilir
- Kullanýcý deneyimi kesintisiz devam eder
- Loglar kontrol edilebilir

## ?? Maliyet Optimizasyonu

### Google Places API Fiyatlandýrmasý
- Ýlk $200 kredi her ay ücretsiz
- Place Details: $17 / 1000 istek
- Aylýk 11,764 ücretsiz istek hakký

### Önbellek (Cache) Stratejisi
Maliyeti düþürmek için:

1. **Memory Cache Ekleyin:**
```csharp
builder.Services.AddMemoryCache();
```

2. **Service'de Cache Kullanýn:**
```csharp
private readonly IMemoryCache _cache;

public async Task<GooglePlaceDetails?> GetPlaceDetailsAsync(string placeId)
{
    var cacheKey = $"reviews_{placeId}";
    
    if (_cache.TryGetValue(cacheKey, out GooglePlaceDetails? cachedData))
    {
      return cachedData;
    }
    
    var data = await FetchFromGoogleAPI(placeId);
    
    _cache.Set(cacheKey, data, TimeSpan.FromHours(24)); // 24 saat cache
    
  return data;
}
```

## ?? Sorun Giderme

### API Key Çalýþmýyor
- API key kýsýtlamalarýný kontrol edin
- Places API'nin aktif olduðundan emin olun
- Faturalandýrma hesabý aktif mi kontrol edin

### Yorumlar Görünmüyor
1. Browser Console'u açýn (F12)
2. Network tab'ýnda `/api/reviews` isteðini kontrol edin
3. Server loglarýný kontrol edin

### CORS Hatasý
Program.cs'e CORS policy ekleyin:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
    builder.AllowAnyOrigin()
   .AllowAnyMethod()
      .AllowAnyHeader();
    });
});
```

## ?? Notlar

- **Google My Business** hesabýnýzdan yorumlarý yönetebilirsiniz
- Minimum 5 yorum gösterilmesi önerilir
- API güncellemeleri için [Google Places API Docs](https://developers.google.com/maps/documentation/places/web-service/overview) kontrol edin
- Rate limiting için exponential backoff stratejisi uygulayýn

## ?? Destek

Sorun yaþarsanýz:
1. Console loglarýný kontrol edin
2. API key ve Place ID'yi doðrulayýn
3. Google Cloud Console'da kullaným metriklerini inceleyin

---

**Önemli:** API key'lerinizi asla public repository'lere commit etmeyin!
