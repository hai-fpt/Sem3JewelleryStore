import { Helmet } from 'react-helmet-async';
import {useEffect, useState} from 'react';
// @mui
import {Button, Container, Stack, Typography} from '@mui/material';
// components
import { ProductSort, ProductList, ProductCartWidget, ProductFilterSidebar } from '../sections/@dashboard/products';
// mock
import PRODUCTS from '../_mock/products';
import {useParams} from "react-router-dom";

// ----------------------------------------------------------------------

export default function ProductsPage() {
  const [items, setItems] = useState(null);
  const [loading, setLoading] = useState(false);
  const { categoryId, term } = useParams();

  useEffect(async () => {
    setLoading(true);
    try {
      fetch("https://localhost:7211/api/JewelType")
          .then(res => res.json())
          .then(res => {
            setItems(res.$values)
          })
      setLoading(false);
    } catch (err) {
      console.error(err);
    }
  }, [categoryId, term]);

  return (
    <>
      <Helmet>
        <title> Dashboard: Products | Minimal UI </title>
      </Helmet>

      <Container>
        <Typography variant="h4" sx={{ mb: 5 }}>
          Products
        </Typography>

        <Button variant="contained" sx={{backgroundColor: "#2196f3"}}>
          Add New Product
        </Button>

        <Stack direction="row" flexWrap="wrap-reverse" alignItems="center" justifyContent="flex-end" sx={{ mb: 5 }}>
          <Stack direction="row" spacing={1} flexShrink={0} sx={{ my: 1 }}>
          </Stack>
        </Stack>
        {items != null ? (
            <ProductList products={items} />
        ) : ("loading")}
      </Container>
    </>
  );
}
