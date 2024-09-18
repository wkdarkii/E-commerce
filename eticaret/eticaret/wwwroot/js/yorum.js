// Sayfa yüklendiğinde canAddComment false ise uyarı mesajını gösterir
document.addEventListener('DOMContentLoaded', function () {
    var canAddComment = @Html.Raw(Json.Encode(ViewBag.CanAddComment));
    if (canAddComment === false) {
        document.getElementById('commentWarning').style.display = 'block';
    }
});

// Uyarı mesajını kapatmak için fonksiyon
function closeWarning() {
    document.getElementById('commentWarning').style.display = 'none';
}