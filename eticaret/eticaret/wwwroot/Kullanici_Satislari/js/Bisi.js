
function downloadPDF() {
  const element = document.querySelector('.invoice-container'); // PDF'e dönüştürmek istediğiniz alanın sınıfını seçin
  const options = {
    margin: 0.5,
    filename: 'invoice.pdf',
    image: { type: 'jpeg', quality: 0.98 },
    html2canvas: { scale: 2 },
    jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
  };

  html2pdf().from(element).set(options).save();
}