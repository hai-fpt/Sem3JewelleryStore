import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import LoadingSpinner from '../ui/LoadingSpinner';
import ItemList from './ItemList';

const ItemListCointainer = () => {
  const [items, setItems] = useState(null);
  const [loading, setLoading] = useState(false);
  const { categoryId, term } = useParams();

  useEffect(async () => {
    setLoading(true);
    if (categoryId == undefined){
      try {
        fetch("https://localhost:7211/api/JewelType")
            .then(res => res.json())
            .then(res => {
              setItems(res)
            })
        setLoading(false);
      } catch (err) {
        console.error(err);
      }
    } else {
      const category = categoryId.slice(0, -1);
      try {
        fetch("https://localhost:7211/api/JewelType/category?category=" + category)
            .then(res => res.json())
            .then(res => {
              setItems(res)
            });
        setLoading(false);
      } catch (err) {
        console.error(err)
      }
    }

  }, [categoryId, term]);

  return loading ? (
    <LoadingSpinner text='Loading products...' />
  ) : (
    <ItemList items={items} />
  );
};
export default ItemListCointainer;
