export default function validateAddressForm(values, setErrors) {
    const errors = {};

    //name

    if (!values.name) {
        errors.name = 'First name required';
    } else if (values.name?.length < 3) {
        errors.name = 'First name must have at least 3 characters';
    } else if (!/^[A-Za-z]+(?:[ _-][A-Za-z]+)*$/i.test(values.name)) {
        errors.name = 'First name is invalid';
    }

    //lastName

    if (!values.lastName) {
        errors.lastName = 'Last name required';
    } else if (values.lastName?.length < 3) {
        errors.lastName = 'Last name must be at least 3 characters';
    } else if (!/^[A-Za-z]+(?:[ _-][A-Za-z]+)*$/i.test(values.lastName)) {
        errors.lastName = 'Last name is invalid';
    }

//email

    if (!values.email) {
        errors.email = 'Email required';
    } else if (values.email?.length < 3) {
        errors.email = 'Email must be at least 3 characters';
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i.test(values.email)) {
        errors.email = 'Email is invalid';
    }

//address

    if (!values.address) {
        errors.address = 'Address required';
    } else if (values.address?.length < 3) {
        errors.address = 'Address must be at least 3 characters';
    }

//city

    if (!values.city) {
        errors.city = 'City required';
    } else if (values.city?.length < 3) {
        errors.city = 'City must be at least 3 characters';
    } else if (!/^[A-Za-z]+(?:[ _-][A-Za-z]+)*$/i.test(values.city)) {
        errors.city = 'City is invalid';
    }

//state

    if (!values.state) {
        errors.state = 'State/Province required';
    } else if (values.state?.length < 3) {
        errors.state = 'The field must be at least 3 characters';
    } else if (!/^[A-Za-z]+(?:[ _-][A-Za-z]+)*$/i.test(values.state)) {
        errors.state = 'The field is invalid';
    }

    if (!values.zip) {
        errors.zip = 'Postal code required';
    } else if (values.zip?.length < 4 || values.zip?.length > 6) {
        errors.zip = 'Postal code must have 4 to 6 characters';
    } else if (!/^[0-9]{4,6}$/i.test(values.zip)) {
        errors.zip = 'Postal code is invalid';
    }

//phoneNumber

    if (!values.phoneNumber) {
        errors.phoneNumber = 'Phone number required';
    } else if (
        values.phoneNumber?.length < 8 ||
        values.phoneNumber?.length > 10
    ) {
        errors.phoneNumber = 'Phone number must have 8 to 10 characters';
    } else if (!/^[0-9]{8,10}$/i.test(values.phoneNumber)) {
        errors.phoneNumber = 'Phone number is invalid';
    }

    setErrors(errors);

    return Object.keys(errors).length === 0 ? true : false;
}
