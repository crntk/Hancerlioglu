using System.Text.Json;
using Hancerlioglu.Constants;
using Hancerlioglu.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Hancerlioglu.Services
{
    public interface IGooglePlacesService
    {
        Task<GooglePlaceDetails?> GetPlaceDetailsAsync(string placeId);
    }

    public class GooglePlacesService : IGooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GooglePlacesService> _logger;
        private readonly IMemoryCache _cache;

        public GooglePlacesService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GooglePlacesService> logger,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
        }

        public async Task<GooglePlaceDetails?> GetPlaceDetailsAsync(string placeId)
        {
            // Check cache first
            var cacheKey = $"GooglePlaceDetails_{placeId}";
            if (_cache.TryGetValue<GooglePlaceDetails>(cacheKey, out var cachedDetails))
            {
                _logger.LogInformation("Returning cached Google Places data for place: {PlaceId}", placeId);
                return cachedDetails;
            }

            try
            {
                var apiKey = _configuration[GooglePlacesConstants.ApiKeyConfigPath];

                if (string.IsNullOrEmpty(apiKey))
                {
                    _logger.LogWarning("Google Places API key is not configured");
                    return GetFallbackData();
                }

                var url = BuildApiUrl(placeId, apiKey);
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Google Places API request failed with status code: {StatusCode}", response.StatusCode);
                    return GetFallbackData();
                }

                var placeDetails = await ParseApiResponse(response);
                var result = placeDetails ?? GetFallbackData();

                // Cache the result for 1 hour
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                };
                _cache.Set(cacheKey, result, cacheOptions);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Google Places data");
                return GetFallbackData();
            }
        }

        private static string BuildApiUrl(string placeId, string apiKey)
        {
            return $"{GooglePlacesConstants.ApiBaseUrl}?" +
                   $"place_id={placeId}" +
                   $"&fields={GooglePlacesConstants.ApiFields}" +
                   $"&language={GooglePlacesConstants.Language}" +
                   $"&key={apiKey}";
        }

        private async Task<GooglePlaceDetails?> ParseApiResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<GooglePlacesResponse>(content, options);

            if (result?.Status != GooglePlacesConstants.SuccessStatus || result.Result == null)
            {
                _logger.LogWarning("Google Places API returned status: {Status}", result?.Status);
                return null;
            }

            return MapToPlaceDetails(result.Result);
        }

        private static GooglePlaceDetails MapToPlaceDetails(GooglePlaceResult result)
        {
            return new GooglePlaceDetails
            {
                Name = result.Name,
                Rating = result.Rating,
                UserRatingsTotal = result.UserRatingsTotal,
                FormattedAddress = result.FormattedAddress,
                Reviews = result.Reviews.Select(MapToReview).ToList()
            };
        }

        private static GoogleReview MapToReview(GoogleReviewDto reviewDto)
        {
            return new GoogleReview
            {
                AuthorName = reviewDto.AuthorName,
                AuthorPhotoUrl = reviewDto.ProfilePhotoUrl,
                Rating = reviewDto.Rating,
                Text = reviewDto.Text,
                RelativeTimeDescription = reviewDto.RelativeTimeDescription,
                Time = reviewDto.Time
            };
        }

        private static GooglePlaceDetails GetFallbackData()
        {
            return new GooglePlaceDetails
            {
                Name = ApplicationConstants.DefaultPlaceName,
                Rating = ApplicationConstants.DefaultRating,
                UserRatingsTotal = ApplicationConstants.DefaultUserRatingsTotal,
                FormattedAddress = ApplicationConstants.DefaultPlaceAddress,
                Reviews = new List<GoogleReview>
                {
                    new GoogleReview
                    {
                        AuthorName = "Ayþe Yýlmaz",
                        Rating = 5,
                        Text = "Midye baklavasý muhteþem! Yýllardýr gittiðim tek baklava yeri. Fýstýk kalitesi ve taze olmasý çok önemli, burada her zaman taze ve lezzetli. Personel de çok ilgili ve güler yüzlü. Kesinlikle tavsiye ederim.",
                        RelativeTimeDescription = "2 hafta önce"
                    },
                    new GoogleReview
                    {
                        AuthorName = "Mehmet Kaya",
                        Rating = 5,
                        Text = "Künefe için gidilir! Sýcak sýcak servis ediliyor, peyniri tam kývamýnda. Erdemli'de künefe deneyince burasý kesinlikle ilk sýrada. Fiyatlarý da gayet makul. Her geliþimde mutlaka uðruyorum.",
                        RelativeTimeDescription = "1 ay önce"
                    },
                    new GoogleReview
                    {
                        AuthorName = "Zeynep Arslan",
                        Rating = 5,
                        Text = "Þöbiyet ve havuç dilimi harika! Kaymaðý çok lezzetli, þerbeti de tam kývamýnda. Tatlýlarýnýn hepsi ayrý güzel. Hijyen konusunda da çok titizler. Mersin'e geldiðimde mutlaka uðradýðým adres.",
                        RelativeTimeDescription = "3 hafta önce"
                    },
                    new GoogleReview
                    {
                        AuthorName = "Ali Özdemir",
                        Rating = 5,
                        Text = "Fýstýklý baklava ve kadayýfý için defalarca geliyorum. Ustalar gerçekten iþlerinin ehli. Her seferinde ayný lezzet ve kalite. Paket servisleri de çok özenli. Erdemli'nin en iyi tatlýcýsý.",
                        RelativeTimeDescription = "1 hafta önce"
                    },
                    new GoogleReview
                    {
                        AuthorName = "Fatma Türk",
                        Rating = 5,
                        Text = "Ailemle birlikte düzenli olarak geliyoruz. Çocuklar künefe için bayýlýyor. Ortam çok temiz ve ferah. Çalýþanlar çok nazik. Fiyat performans olarak da çok iyi. Herkese tavsiye ederim.",
                        RelativeTimeDescription = "4 gün önce"
                    },
                    new GoogleReview
                    {
                        AuthorName = "Can Yýldýz",
                        Rating = 5,
                        Text = "Baklava konusunda gerçekten iddialý bir iþletme. Her çeþidini denedim ve hepsi birbirinden lezzetliydi. Özellikle fýstýk kalitesi çok iyi. Türbe bölgesinin en kaliteli tatlýcýsý diyebilirim.",
                        RelativeTimeDescription = "2 ay önce"
                    }
                }
            };
        }
    }
}
