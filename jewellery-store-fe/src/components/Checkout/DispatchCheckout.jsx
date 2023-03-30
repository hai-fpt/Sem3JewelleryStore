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
    const randomNum = Math.floor(Math.random()*100000);

    //TODO: inventory management shit
    useEffect(async () => {
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
        resetCart();
        resetUserData();
    }, []);
    useEffect(async () => {
        if (verification == 200) {
            setOrderId("ORD" + randomNum);
        }
    },[verification])
    return (verification === 200) ? (
        <Success orderId={orderId}/>
    ) : (verification !== 200) ? (
        <FailedTransaction error={verification}/>
    ) : (
        <LoadingSpinner text='Transaction processing...'/>
    );
};

export default DispatchCheckout;
