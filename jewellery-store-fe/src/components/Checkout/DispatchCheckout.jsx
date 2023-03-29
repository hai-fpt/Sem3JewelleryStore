import {useState, useEffect} from 'react';

import dispatchOrder from '../../firebase/dispatchOrder';
import LoadingSpinner from '../ui/LoadingSpinner';
import Success from './Success';
import FailedTransaction from './FailedTransaction';

const DispatchCheckout = ({
                              userData,
                              resetUserData,
                              cart,
                              resetCart,
                              totalCartPrice,
                          }) => {
    const [verification, setVerification] = useState(null);
    const [orderId, setOrderId] = useState(null);
    const [error, setError] = useState(null);
    console.log(cart)
    console.log(userData)
    //TODO: inventory management shit
    useEffect(async () => {
        // await dispatchOrder(cart, userData, totalCartPrice, setOrderId, setError);
        fetch("https://localhost:7211/api/verify",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    CardCvv: userData.cardCvv,
                    CardExpiration: userData.cardExpDate,
                    CardName: userData.cardName,
                    CardNumber: userData.cardNumber
                })
            })
            .then(res => res.status)
            .then(res => setVerification(res));
        // if (verification == 200) {
        //     fetch("https://localhost:7211/api/")
        // }
        // resetCart();
        // resetUserData();
    }, []);
    return orderId ? (
        <Success orderId={orderId}/>
    ) : error ? (
        <FailedTransaction error={error}/>
    ) : (
        <LoadingSpinner text='Transaction processing...'/>
    );
};

export default DispatchCheckout;
