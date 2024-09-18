document.querySelectorAll('.custom-radio-option').forEach(function(label) {
    label.addEventListener('click', function() {
        document.querySelectorAll('.custom-radio-option').forEach(function(item) {
            item.style.color = '#333'; // Diğerlerinin rengini geri al
        });
        this.style.color = 'red'; // Tıklananın rengini değiştir
      
    });
});


function validateNumber(input) {
    // Yalnızca rakamları kabul eder
    input.value = input.value.replace(/[^0-9]/g, '');
}


document.addEventListener('DOMContentLoaded', function() {
    const fileInputs = document.querySelectorAll('.file-input');
    const modal = document.getElementById('photo-modal');
    const modalImg = document.getElementById('modal-image');
    const closeBtn = document.querySelector('.close');

    fileInputs.forEach(input => {
        input.addEventListener('change', function() {
            const file = this.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function(e) {
                    // Find the associated label and set its background image
                    const label = document.querySelector(`label[for="${input.id}"]`);
                    if (label) {
                        label.style.backgroundImage = `url(${e.target.result})`;
                    }
                };
                reader.readAsDataURL(file);
            }
        });
    });

    document.querySelectorAll('.file-input + label').forEach(label => {
        label.addEventListener('click', function() {
            const imgSrc = getComputedStyle(this).backgroundImage.slice(5, -2); // Extract URL
            modalImg.src = imgSrc;
            modal.style.display = 'block';
        });
    });

    closeBtn.addEventListener('click', function() {
        modal.style.display = 'none';
    });

    window.addEventListener('click', function(event) {
        if (event.target === modal) {
            modal.style.display = 'none';
        }
    });
});




document.addEventListener('DOMContentLoaded', function() {
    const fileInput = document.getElementById('fileInput');
    const imagePreview = document.getElementById('imagePreview');
    const previewImg = document.getElementById('previewImg');
    const imageModal = document.getElementById('imageModal');
    const modalImg = document.getElementById('modalImg');
    const closeModal = document.getElementsByClassName('close')[0];

    fileInput.addEventListener('change', function() {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                previewImg.src = e.target.result;
                previewImg.style.display = 'block'; // Resmi görünür yap
            };
            reader.readAsDataURL(file);
        }
    });

    imagePreview.addEventListener('click', function() {
        if (previewImg.src) {
            modalImg.src = previewImg.src;
            imageModal.style.display = 'block';
        }
    });

    closeModal.addEventListener('click', function() {
        imageModal.style.display = 'none';
    });

    window.addEventListener('click', function(event) {
        if (event.target === imageModal) {
            imageModal.style.display = 'none';
        }
    });
});

