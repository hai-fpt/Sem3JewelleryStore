import PropTypes from 'prop-types';
// @mui
import { Grid } from '@mui/material';
import ShopProductCard from './ProductCard';

// ----------------------------------------------------------------------

ProductList.propTypes = {
  products: PropTypes.array.isRequired,
};

export default function ProductList({ products }) {
    const sortItems = () => {
        const sortedItems = products.sort((a, b) => {
            const idA = parseInt(a.id.substring(2));
            const idB = parseInt(b.id.substring(2));
            return idA - idB;
        })
    }
  return (
      <>
          <Grid container spacing={3}>
              {products?.map((product) => (
                  <Grid key={product.id} item xs={12} sm={6} md={4}>
                      <ShopProductCard {...product}/>
                  </Grid>
              ))}
          </Grid>
      </>
    // <Grid container spacing={3} {...other}>
    //   {products.map((product) => (
    //     <Grid key={product.id} item xs={12} sm={6} md={3}>
    //       <ShopProductCard product={product} />
    //     </Grid>
    //   ))}
    // </Grid>
  );
}
