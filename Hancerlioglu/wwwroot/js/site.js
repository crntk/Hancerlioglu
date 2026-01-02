// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Lazy Loading for Images
document.addEventListener('DOMContentLoaded', function() {
    // Lazy load images
    const images = document.querySelectorAll('img[data-src]');
    const imageObserver = new IntersectionObserver((entries, observer) => {
 entries.forEach(entry => {
            if (entry.isIntersecting) {
              const img = entry.target;
        img.src = img.dataset.src;
    img.removeAttribute('data-src');
       imageObserver.unobserve(img);
    }
        });
    });

    images.forEach(img => imageObserver.observe(img));

    // Defer non-critical CSS
    const deferredStyles = document.querySelectorAll('link[data-defer]');
    deferredStyles.forEach(link => {
        link.rel = 'stylesheet';
        link.removeAttribute('data-defer');
    });

    // Preload critical fonts
    const fontPreload = document.createElement('link');
    fontPreload.rel = 'preload';
    fontPreload.as = 'font';
    fontPreload.type = 'font/woff2';
    fontPreload.crossOrigin = 'anonymous';
    document.head.appendChild(fontPreload);
});

// Debounce function for scroll events
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
    const later = () => {
         clearTimeout(timeout);
            func(...args);
  };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Optimize animations on scroll
let ticking = false;
window.addEventListener('scroll', function() {
    if (!ticking) {
     window.requestAnimationFrame(function() {
  // Your scroll handling code here
            ticking = false;
        });
     ticking = true;
 }
});
