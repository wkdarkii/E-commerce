document.addEventListener('DOMContentLoaded', () => {
    // Modal ve butonları seçiyoruz
    const modal = document.getElementById('imageModal');
    const modalImage = document.getElementById('modalImage');
    const closeButton = document.querySelector('.close');

    // Resme tıklama olayı
    document.querySelectorAll('.product-image').forEach(img => {
        img.addEventListener('click', (event) => {
            // Tıklanan resmin URL'sini modal içindeki resme atıyoruz
            modalImage.src = event.target.src;
            modal.style.display = 'flex'; // Modali göster
        });
    });

    // Modal kapama olayı
    closeButton.addEventListener('click', () => {
        modal.style.display = 'none'; // Modali gizle
    });

    // Modal dışına tıklanırsa kapama
    window.addEventListener('click', (event) => {
        if (event.target === modal) {
            modal.style.display = 'none'; // Modali gizle
        }
    });
});
