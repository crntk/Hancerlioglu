namespace Hancerlioglu.Models
{
    public class GoogleReview
    {
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorPhotoUrl { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public string RelativeTimeDescription { get; set; } = string.Empty;
        public long Time { get; set; }
    }

    public class GooglePlaceDetails
    {
        public string Name { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
        public string FormattedAddress { get; set; } = string.Empty;
        public List<GoogleReview> Reviews { get; set; } = new List<GoogleReview>();
    }

    public class GooglePlacesResponse
    {
        public GooglePlaceResult Result { get; set; } = new GooglePlaceResult();
        public string Status { get; set; } = string.Empty;
    }

    public class GooglePlaceResult
    {
        public string Name { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
        public string FormattedAddress { get; set; } = string.Empty;
        public List<GoogleReviewDto> Reviews { get; set; } = new List<GoogleReviewDto>();
    }

    public class GoogleReviewDto
    {
        public string AuthorName { get; set; } = string.Empty;
        public string ProfilePhotoUrl { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string RelativeTimeDescription { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public long Time { get; set; }
    }
}
