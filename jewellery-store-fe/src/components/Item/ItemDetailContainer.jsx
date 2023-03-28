import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

import LoadingSpinner from '../ui/LoadingSpinner';
import ItemDetail from './ItemDetail';
import { db } from '../../firebase/config';
import { doc, getDoc } from 'firebase/firestore';

const ItemDetailContainer = () => {
  const [item, setItem] = useState(null);
  const { itemId } = useParams();

  useEffect(async () => {
   fetch("https://localhost:7211/api/JewelType/" + itemId)
       .then(res => res.json())
       .then(res => setItem(res));
  }, [itemId]);

  return item ? <ItemDetail {...item} /> : <LoadingSpinner />;
};

export default ItemDetailContainer;
