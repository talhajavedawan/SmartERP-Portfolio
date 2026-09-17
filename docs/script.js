(() => {
  const root = document.documentElement;
  const savedTheme = localStorage.getItem('tj-theme');
  const prefersLight = window.matchMedia('(prefers-color-scheme: light)').matches;
  root.dataset.theme = savedTheme || (prefersLight ? 'light' : 'dark');

  const themeToggle = document.querySelector('.theme-toggle');
  themeToggle?.addEventListener('click', () => {
    const next = root.dataset.theme === 'light' ? 'dark' : 'light';
    root.dataset.theme = next;
    localStorage.setItem('tj-theme', next);
  });

  const menuToggle = document.querySelector('.menu-toggle');
  const navLinks = document.querySelector('.nav-links');

  menuToggle?.addEventListener('click', () => {
    const open = navLinks?.classList.toggle('is-open');
    menuToggle.setAttribute('aria-expanded', String(Boolean(open)));
  });

  navLinks?.querySelectorAll('a').forEach((link) => {
    link.addEventListener('click', () => {
      navLinks.classList.remove('is-open');
      menuToggle?.setAttribute('aria-expanded', 'false');
    });
  });

  const revealItems = document.querySelectorAll('.reveal');
  if ('IntersectionObserver' in window) {
    const revealObserver = new IntersectionObserver((entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('is-visible');
          observer.unobserve(entry.target);
        }
      });
    }, { threshold: 0.12 });

    revealItems.forEach((item) => revealObserver.observe(item));
  } else {
    revealItems.forEach((item) => item.classList.add('is-visible'));
  }

  const dialog = document.querySelector('.lightbox');
  const dialogImage = dialog?.querySelector('img');
  const dialogCaption = dialog?.querySelector('p');
  const dialogClose = dialog?.querySelector('.lightbox-close');

  document.querySelectorAll('.gallery-item').forEach((item) => {
    item.addEventListener('click', () => {
      if (!dialog || !dialogImage || !dialogCaption) return;
      dialogImage.src = item.dataset.image || '';
      dialogImage.alt = item.dataset.title || 'SmartERP screenshot';
      dialogCaption.textContent = item.dataset.title || '';
      dialog.showModal();
    });
  });

  dialogClose?.addEventListener('click', () => dialog?.close());
  dialog?.addEventListener('click', (event) => {
    const rect = dialog.getBoundingClientRect();
    const outside = event.clientX < rect.left || event.clientX > rect.right || event.clientY < rect.top || event.clientY > rect.bottom;
    if (outside) dialog.close();
  });

  const year = document.querySelector('#year');
  if (year) year.textContent = String(new Date().getFullYear());
})();
