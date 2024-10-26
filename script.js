// Detect when an object is shown and play the transition
const observer = new IntersectionObserver((entries) => {
   entries.forEach((entry) => {
      //console.log(entry);
      if (entry.isIntersecting) {
         entry.target.classList.add('show');
         setTimeout(() => { entry.target.classList.add('fast-transition'); }, 1000)
      }
   });
});

const hiddenElements = document.querySelectorAll('.hidden');
hiddenElements.forEach((el) => observer.observe(el));

// Parallax effect
if(navigator.userAgent === 'Mozilla/5.0 (iPhone13,2; U; CPU iPhone OS 14_0 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/15E148 Safari/602.1' ||
   navigator.userAgent === 'Mozilla/5.0 (iPhone12,1; U; CPU iPhone OS 13_0 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/15E148 Safari/602.1'
) {
   console.log('iPhone12');
   setInterval(() => {
      document.body.style.backgroundPosition = '0%' + window.scrollY/500 +'%';
   },
   5
   );
} else {
   console.log('other device');
   
   setInterval(() => {
      document.body.style.backgroundPosition = '0%' + window.scrollY/5 +'%';
   },
   30
   );
}