// Cache Buster - Force reload resources
(function() {
    'use strict';
    
    // Version number - increment this when you make changes
    const APP_VERSION = '1.0.1';
    const CACHE_KEY = 'app_version';
    
    // Check if version has changed
    const cachedVersion = localStorage.getItem(CACHE_KEY);
    
    if (cachedVersion !== APP_VERSION) {
        console.log('?? New version detected. Clearing cache...');
     
        // Clear all caches
        if ('caches' in window) {
  caches.keys().then(function(names) {
     names.forEach(function(name) {
           caches.delete(name);
  });
            });
        }
        
        // Clear localStorage (except user preferences)
   const theme = localStorage.getItem('theme');
        localStorage.clear();
        if (theme) {
     localStorage.setItem('theme', theme);
  }
        
        // Update version
     localStorage.setItem(CACHE_KEY, APP_VERSION);
     
   // Force reload
        console.log('? Cache cleared. Reloading page...');
        window.location.reload(true);
    }
})();
