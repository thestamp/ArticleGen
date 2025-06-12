// ArticleGen JavaScript
document.addEventListener('DOMContentLoaded', function() {
    
    // Mobile menu functionality
    function initMobileMenu() {
        const nav = document.querySelector('.nav');
        const navList = document.querySelector('.nav-list');
        
        // Create mobile menu button if not exists
        if (!document.querySelector('.mobile-menu-btn')) {
            const mobileMenuBtn = document.createElement('button');
            mobileMenuBtn.className = 'mobile-menu-btn';
            mobileMenuBtn.innerHTML = '☰';
            mobileMenuBtn.setAttribute('aria-label', 'Toggle navigation menu');
            
            const headerContent = document.querySelector('.header-content');
            headerContent.insertBefore(mobileMenuBtn, nav);
            
            // Toggle mobile menu
            mobileMenuBtn.addEventListener('click', function() {
                navList.classList.toggle('nav-open');
                mobileMenuBtn.innerHTML = navList.classList.contains('nav-open') ? '✕' : '☰';
            });
        }
    }
    
    // Newsletter form submission
    function initNewsletterForm() {
        const newsletterForm = document.querySelector('.newsletter-form');
        
        if (newsletterForm) {
            newsletterForm.addEventListener('submit', function(e) {
                e.preventDefault();
                
                const emailInput = this.querySelector('.newsletter-input');
                const email = emailInput.value.trim();
                
                if (email && isValidEmail(email)) {
                    // Simulate successful subscription
                    showNotification('Thank you for subscribing to our newsletter!', 'success');
                    emailInput.value = '';
                } else {
                    showNotification('Please enter a valid email address.', 'error');
                }
            });
        }
    }
    
    // Email validation
    function isValidEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }
    
    // Show notification
    function showNotification(message, type = 'info') {
        // Remove existing notification
        const existingNotification = document.querySelector('.notification');
        if (existingNotification) {
            existingNotification.remove();
        }
        
        // Create notification element
        const notification = document.createElement('div');
        notification.className = `notification notification-${type}`;
        notification.innerHTML = `
            <span>${message}</span>
            <button class="notification-close" aria-label="Close notification">✕</button>
        `;
        
        // Add notification styles
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            background: ${type === 'success' ? '#10b981' : type === 'error' ? '#ef4444' : '#2563eb'};
            color: white;
            padding: 1rem 1.5rem;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            z-index: 1000;
            display: flex;
            align-items: center;
            gap: 1rem;
            max-width: 400px;
            font-size: 0.875rem;
            font-weight: 500;
            transform: translateX(100%);
            transition: transform 0.3s ease;
        `;
        
        // Close button styles
        const closeBtn = notification.querySelector('.notification-close');
        closeBtn.style.cssText = `
            background: none;
            border: none;
            color: white;
            font-size: 1rem;
            cursor: pointer;
            padding: 0;
            margin-left: auto;
        `;
        
        document.body.appendChild(notification);
        
        // Animate in
        setTimeout(() => {
            notification.style.transform = 'translateX(0)';
        }, 100);
        
        // Close functionality
        closeBtn.addEventListener('click', () => {
            notification.style.transform = 'translateX(100%)';
            setTimeout(() => notification.remove(), 300);
        });
        
        // Auto remove after 5 seconds
        setTimeout(() => {
            if (notification.parentNode) {
                notification.style.transform = 'translateX(100%)';
                setTimeout(() => notification.remove(), 300);
            }
        }, 5000);
    }
    
    // Smooth scrolling for anchor links
    function initSmoothScrolling() {
        const links = document.querySelectorAll('a[href^="#"]');
        
        links.forEach(link => {
            link.addEventListener('click', function(e) {
                const href = this.getAttribute('href');
                
                if (href === '#') {
                    e.preventDefault();
                    return;
                }
                
                const target = document.querySelector(href);
                
                if (target) {
                    e.preventDefault();
                    
                    const headerHeight = document.querySelector('.header').offsetHeight;
                    const targetPosition = target.offsetTop - headerHeight - 20;
                    
                    window.scrollTo({
                        top: targetPosition,
                        behavior: 'smooth'
                    });
                }
            });
        });
    }
    
    // Article card hover effects and analytics
    function initArticleCards() {
        const articleCards = document.querySelectorAll('.article-card, .featured-article');
        
        articleCards.forEach(card => {
            card.addEventListener('click', function(e) {
                // If clicking on a link inside the card, let it handle navigation
                if (e.target.tagName === 'A') {
                    return;
                }
                
                // Otherwise, find the read more link and trigger it
                const readMoreLink = this.querySelector('.read-more, a');
                if (readMoreLink) {
                    readMoreLink.click();
                }
            });
            
            // Add keyboard navigation
            card.setAttribute('tabindex', '0');
            card.setAttribute('role', 'article');
            
            card.addEventListener('keydown', function(e) {
                if (e.key === 'Enter' || e.key === ' ') {
                    e.preventDefault();
                    const readMoreLink = this.querySelector('.read-more, a');
                    if (readMoreLink) {
                        readMoreLink.click();
                    }
                }
            });
        });
    }
    
    // Search functionality (placeholder)
    function initSearch() {
        // Create search input if needed
        const nav = document.querySelector('.nav');
        if (nav && !document.querySelector('.search-input')) {
            const searchContainer = document.createElement('div');
            searchContainer.className = 'search-container';
            searchContainer.innerHTML = `
                <input type="text" class="search-input" placeholder="Search articles..." aria-label="Search articles">
                <button class="search-btn" aria-label="Search">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <circle cx="11" cy="11" r="8"></circle>
                        <path d="m21 21-4.35-4.35"></path>
                    </svg>
                </button>
            `;
            
            // Add search styles
            searchContainer.style.cssText = `
                position: relative;
                display: flex;
                align-items: center;
                margin-left: 2rem;
            `;
            
            const searchInput = searchContainer.querySelector('.search-input');
            searchInput.style.cssText = `
                padding: 0.5rem 2.5rem 0.5rem 1rem;
                border: 1px solid #d1d5db;
                border-radius: 6px;
                font-size: 0.875rem;
                width: 200px;
                transition: all 0.2s ease;
            `;
            
            const searchBtn = searchContainer.querySelector('.search-btn');
            searchBtn.style.cssText = `
                position: absolute;
                right: 0.5rem;
                background: none;
                border: none;
                color: #6b7280;
                cursor: pointer;
                padding: 0.25rem;
                display: flex;
                align-items: center;
            `;
            
            nav.appendChild(searchContainer);
            
            // Search functionality
            searchInput.addEventListener('input', function() {
                const query = this.value.toLowerCase().trim();
                // Placeholder for search functionality
                console.log('Searching for:', query);
            });
            
            searchInput.addEventListener('focus', function() {
                this.style.borderColor = '#2563eb';
                this.style.boxShadow = '0 0 0 2px rgba(37, 99, 235, 0.1)';
            });
            
            searchInput.addEventListener('blur', function() {
                this.style.borderColor = '#d1d5db';
                this.style.boxShadow = 'none';
            });
        }
    }
    
    // Intersection Observer for animations
    function initScrollAnimations() {
        const observerOptions = {
            threshold: 0.1,
            rootMargin: '0px 0px -50px 0px'
        };
        
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }
            });
        }, observerOptions);
        
        // Observe elements that should animate in
        const animateElements = document.querySelectorAll('.article-card, .widget, .featured-article');
        animateElements.forEach(el => {
            el.style.opacity = '0';
            el.style.transform = 'translateY(20px)';
            el.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            observer.observe(el);
        });
    }
    
    // Initialize all functionality
    initMobileMenu();
    initNewsletterForm();
    initSmoothScrolling();
    initArticleCards();
    initSearch();
    initScrollAnimations();
    
    // Add loading state management
    window.addEventListener('load', function() {
        document.body.classList.add('loaded');
    });
    
    // Add responsive utilities
    function handleResize() {
        const isMobile = window.innerWidth <= 768;
        document.body.classList.toggle('mobile', isMobile);
        document.body.classList.toggle('desktop', !isMobile);
    }
    
    window.addEventListener('resize', handleResize);
    handleResize(); // Initial call
});

// Add utility functions
window.ArticleGen = {
    // Utility to format dates
    formatDate: function(dateString) {
        const options = { year: 'numeric', month: 'long', day: 'numeric' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    },
    
    // Utility to calculate reading time
    calculateReadingTime: function(text) {
        const wordsPerMinute = 200;
        const wordCount = text.split(/\s+/).length;
        const readingTime = Math.ceil(wordCount / wordsPerMinute);
        return `${readingTime} min read`;
    },
    
    // Utility to truncate text
    truncateText: function(text, maxLength) {
        if (text.length <= maxLength) return text;
        return text.substr(0, maxLength) + '...';
    }
};