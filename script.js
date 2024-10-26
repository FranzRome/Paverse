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
console.log(navigator.userAgent);

if(navigator.userAgent.includes('Safari')) {
   console.log('Safari');
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