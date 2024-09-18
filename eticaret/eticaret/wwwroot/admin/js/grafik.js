
        // Pie Chart for Total Amount
  var ctxPie = document.getElementById('pie-chart').getContext('2d');
  new Chart(ctxPie, {
    type: 'pie',
  data: {
    labels: [@foreach (var item in Model) $"'{item.KullaniciAdi}'", @endforeach],
  datasets: [{
    label: 'Toplam Miktar',
  data: [@foreach (var item in Model) $"{item.ToplamMiktar}", @endforeach],
  backgroundColor: [
  'rgba(255, 99, 132, 0.2)',
  'rgba(54, 162, 235, 0.2)',
  'rgba(255, 206, 86, 0.2)',
  'rgba(75, 192, 192, 0.2)',
  'rgba(153, 102, 255, 0.2)'
  ],
  borderColor: [
  'rgba(255, 99, 132, 1)',
  'rgba(54, 162, 235, 1)',
  'rgba(255, 206, 86, 1)',
  'rgba(75, 192, 192, 1)',
  'rgba(153, 102, 255, 1)'
  ],
  borderWidth: 1
                }]
            },
  options: {
    responsive: true,
  plugins: {
    legend: {
    display: true
                    }
                }
            }
        });

  // Bar Chart for Last Purchase Date
  var ctxBar = document.getElementById('bar-chart').getContext('2d');
  new Chart(ctxBar, {
    type: 'bar',
  data: {
    labels: [@foreach (var item in Model) $"'{item.KullaniciAdi}'", @endforeach],
  datasets: [{
    label: 'Son Alma Zamanı (Gün)',
  data: [@foreach (var item in Model) $"{(DateTime.Now - item.SonAlmaZamani).TotalDays:0}", @endforeach],
  backgroundColor: 'rgba(75, 192, 192, 0.2)',
  borderColor: 'rgba(75, 192, 192, 1)',
  borderWidth: 1
                }]
            },
  options: {
    responsive: true,
  scales: {
    y: {
    beginAtZero: true
                    }
                },
  plugins: {
    legend: {
    display: true
                    }
                }
            }
        });
