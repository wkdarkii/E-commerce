// your-script.js

document.getElementById('seller-button').addEventListener('click', function() {
    Swal.fire({
        title: 'Satıcı Olmak İster Misiniz?',
        text: "Bu teklifi kabul ediyorsanız, lütfen onaylayın.",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Evet, Olmak İstiyorum!',
        cancelButtonText: 'Hayır, Teşekkürler'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Harika!',
                'Satıcı olmak için gerekli adımları takip edin.',
                'success'
            );
        }
    });
});


document.addEventListener('DOMContentLoaded', function() {
    const backIcon = document.querySelector('.back-icon');
    const solust = document.querySelector('.solust');

    // Tıklama olayı
    backIcon.addEventListener('click', function() {
        // .solust sınıfına sahip öğeye 'show' sınıfını ekle
        solust.classList.toggle('show');
    });
});

