using Microsoft.AspNetCore.Mvc;

namespace Hancerlioglu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private const string PlaceName = "HANÇERLÝOÐLU Baklava (Türbe Þube)";
        private const string PlaceAddress = "4.Noter Çaprazý, Merkez, Türbe Cd. No:71 H/4, 33730 Erdemli/Mersin";
        private const double PlaceRating = 5.0;
        private const int TotalRatings = 4;

        [HttpGet]
        public IActionResult GetReviews()
        {
            var placeDetails = CreatePlaceDetails();
            return Ok(placeDetails);
        }

        private static object CreatePlaceDetails()
        {
            return new
            {
                name = PlaceName,
                formattedAddress = PlaceAddress,
                rating = PlaceRating,
                userRatingsTotal = TotalRatings,
                reviews = GetSampleReviews()
            };
        }

        private static object[] GetSampleReviews()
        {
            return new[]
            {
                new
                {
                    authorName = "Enes Öztekin",
                    rating = 5,
                    text = "Hem lezzeti hemde güler yüzleriyle kesinlikle erdeminin en iyisi ??",
                    relativeTimeDescription = "2 hafta önce"
                },
                new
                {
                    authorName = "Emine Yapar",
                    rating = 5,
                    text = "tatlýlarýn þerbeti tam kararýnda, ne fazla ne eksik... Yedikçe yediren cinsten",
                    relativeTimeDescription = "2 hafta önce"
                },
                new
                {
                    authorName = "Eren Ataseven",
                    rating = 5,
                    text = "Ürünler günlük ve çok taze. Þerbeti hafif, fýstýðý bol, aðýzda daðýlýyor. Hijyen konusunda da oldukça titizler. Gönül rahatlýðýyla tavsiye ederim.",
                    relativeTimeDescription = "2 hafta önce"
                },
                new
                {
                    authorName = "mediha dilek",
                    rating = 5,
                    text = "Tatlýnýn en kaliteli merkezi ??",
                    relativeTimeDescription = "2 hafta önce"
                }
            };
        }
    }
}
