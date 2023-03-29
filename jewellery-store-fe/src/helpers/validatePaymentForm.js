export default function validatePaymentForm(values, setError) {
    const errors = {};

//cardNumber

    if (!values.cardNumber) {
        errors.cardNumber = 'Card number required';
    } else if (values.cardNumber?.length < 16 || values.cardNumber?.length > 16) {
        errors.cardNumber = 'Card number must be 16 digits';
    } else if (!/^[0-9]*$/i.test(values.cardNumber)) {
        errors.cardNumber = 'Card number is invalid';
    }

//cardName

    if (!values.cardName) {
        errors.cardName = 'Card name required';
    } else if (values.cardName?.length < 3) {
        errors.cardName = 'Card name must have at least 3 characters';
    } else if (!/^[A-Za-z]+(?:[ _-][A-Za-z]+)*$/i.test(values.cardName)) {
        errors.cardName = 'Card name is invalid';
    }

//cardExpDate

    if (!values.cardExpDate) {
        errors.cardExpDate = 'Expiration date required';
    }

//cardCvv

    if (!values.cardCvv) {
        errors.cardCvv = 'CVV required';
    } else if (values.cardCvv?.length !== 3) {
        errors.cardCvv = 'CVV must be 3 digits';
    } else if (!/^[0-9]*$/i.test(values.cardCvv)) {
        errors.cardCvv = 'CVV is invalid';
    }

    setError(errors);

    return Object.keys(errors).length === 0 ? true : false;
}