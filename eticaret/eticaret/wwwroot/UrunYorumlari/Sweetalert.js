$(document).ready(function () {
    @if (ViewBag.YorumKaydedildi != null) {
        Swal.fire({
            title: 'Yorum Kaydedildi',
            text: 'Yorumunuz başarıyla kaydedildi.',
            icon: 'success',
            timer: 2000,
            timerProgressBar: true,
            showConfirmButton: true
        });
    }
});