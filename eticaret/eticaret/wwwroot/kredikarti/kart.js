const cardBack = document.querySelector('.card .back');
const cvvInput = document.getElementById('cvv-input');

cvvInput.addEventListener('input', function () {
    // Add hover effect when CVV input has value
    if (cvvInput.value.length > 0) {
        cardBack.classList.add('hover');
    } else {
        cardBack.classList.remove('hover');
    }
});

cvvInput.addEventListener('blur', function () {
    // Remove hover effect when CVV input loses focus
    cardBack.classList.remove('hover');
});

document.getElementById('card-form').addEventListener('input', function () {
    // Get form values
    const cardNumber = document.getElementById('card-number-input').value.replace(/\s+/g, '').match(/.{1,4}/g)?.join(' ') || '#### #### #### ####';
    const cardHolder = document.getElementById('card-holder-input').value || 'CARD HOLDER';
    const expiryMonth = document.getElementById('expiry-month').value || 'MM';
    const expiryYear = document.getElementById('expiry-year').value || 'YY';
    const cvv = document.getElementById('cvv-input').value || '###';

    // Update card details
    document.getElementById('card-number').textContent = cardNumber;
    document.getElementById('card-holder').textContent = cardHolder;
    document.getElementById('expiry-date').textContent = `${expiryMonth} / ${expiryYear}`;
    document.getElementById('cvv').textContent = cvv;
});
