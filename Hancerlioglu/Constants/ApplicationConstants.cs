namespace Hancerlioglu.Constants
{
    /// <summary>
    /// Google Places API ile ilgili sabit deðerler
    /// </summary>
    public static class GooglePlacesConstants
    {
        public const string ApiBaseUrl = "https://maps.googleapis.com/maps/api/place/details/json";
    public const string ApiKeyConfigPath = "GooglePlaces:ApiKey";
        public const string PlaceIdConfigPath = "GooglePlaces:PlaceId";
        public const string SuccessStatus = "OK";
        public const string Language = "tr";
        public const string ApiFields = "name,rating,user_ratings_total,formatted_address,reviews";
    }

    /// <summary>
    /// Uygulama genelinde kullanýlan sabit deðerler
    /// </summary>
    public static class ApplicationConstants
    {
        public const string DefaultPlaceName = "HANÇERLÝOÐLU Baklava (Türbe Þube)";
        public const string DefaultPlaceAddress = "J864+Q2 Erdemli, Mersin, Türbe Cd. No:71 H/4, 33730 Erdemli/Mersin";
        public const double DefaultRating = 4.8;
      public const int DefaultUserRatingsTotal = 256;
    }

    /// <summary>
    /// Cache ile ilgili sabit deðerler
    /// </summary>
    public static class CacheConstants
    {
        public const string GooglePlaceDetailsKey = "GooglePlaceDetails";
   public const int CacheDurationInMinutes = 60;
    }
}
