// Topbar date
document.addEventListener('DOMContentLoaded', () => {
  const el = document.getElementById('topbarDate');
  if (el) {
    el.textContent = new Date().toLocaleDateString('en-US', {
      weekday: 'short', month: 'short', day: 'numeric', year: 'numeric'
    }).toUpperCase();
  }

  // Show save toast if redirected after a save (query param ?saved=1)
  const params = new URLSearchParams(window.location.search);
  if (params.get('saved')) {
    showToast('Saved successfully ✓', 'success');
    // clean up URL
    const url = new URL(window.location);
    url.searchParams.delete('saved');
    window.history.replaceState({}, '', url);
  }
});

function showToast(msg, type = 'success') {
  const toast = document.getElementById('toast');
  const dot   = document.getElementById('tdot');
  const msgEl = document.getElementById('toastMsg');
  if (!toast) return;
  msgEl.textContent = msg;
  dot.style.background = type === 'error' ? 'var(--red)' : type === 'warn' ? 'var(--amber)' : 'var(--green)';
  toast.classList.add('show');
  setTimeout(() => toast.classList.remove('show'), 2800);
}
