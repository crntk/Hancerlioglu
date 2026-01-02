namespace Hancerlioglu.Helpers
{
    /// <summary>
    /// Image optimization ve lazy loading helper
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Lazy loading için image tag oluþturur
      /// </summary>
 public static string LazyImage(string src, string alt, string cssClass = "", int width = 0, int height = 0)
        {
         var widthAttr = width > 0 ? $"width=\"{width}\"" : "";
    var heightAttr = height > 0 ? $"height=\"{height}\"" : "";
       var classAttr = !string.IsNullOrEmpty(cssClass) ? $"class=\"{cssClass}\"" : "";
       
          // Placeholder 1x1 transparent GIF
         const string placeholder = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";
    
            return $"<img src=\"{placeholder}\" data-src=\"{src}\" alt=\"{alt}\" {classAttr} {widthAttr} {heightAttr} loading=\"lazy\">";
        }

        /// <summary>
 /// Responsive image srcset oluþturur
        /// </summary>
 public static string ResponsiveImage(string basePath, string alt, string cssClass = "")
    {
  var classAttr = !string.IsNullOrEmpty(cssClass) ? $"class=\"{cssClass}\"" : "";
            
            return $@"<img src=""{basePath}"" 
       srcset=""{basePath}?w=400 400w, 
              {basePath}?w=800 800w, 
  {basePath}?w=1200 1200w""
    sizes=""(max-width: 400px) 400px, 
   (max-width: 800px) 800px, 
           1200px""
             alt=""{alt}"" 
       {classAttr} 
 loading=""lazy"">";
        }

     /// <summary>
        /// WebP format desteði kontrolü
        /// </summary>
public static string GetOptimizedImagePath(string imagePath, bool supportsWebP = false)
        {
        if (supportsWebP && !imagePath.EndsWith(".gif"))
            {
    var webpPath = Path.ChangeExtension(imagePath, ".webp");
     return webpPath;
  }
        return imagePath;
      }
    }
}
