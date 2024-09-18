// JavaScript kodları genellikle form validasyonu, dinamik içerik ekleme veya animasyonlar için kullanılır.
// Burada basit bir örnek olarak formun gönderilmesini önleyeceğiz ve bilgi mesajı göstereceğiz.

document.getElementById('profile-form').addEventListener('submit', function(event) {
    event.preventDefault(); // Formun varsayılan gönderim işlemini engeller
    
    // Form verilerini almak
    const username = document.getElementById('username').value;
    const email = document.getElementById('email').value;
    const phone = document.getElementById('phone').value;
    const address = document.getElementById('address').value;

    // Bsasit bir bilgi mesajı gösterme
    alert(`Bilgileriniz başarıyla güncellendi:\n\nKullanıcı Adı: ${username}\nE-posta: ${email}\nTelefon: ${phone}\nAdres: ${address}`);
});
