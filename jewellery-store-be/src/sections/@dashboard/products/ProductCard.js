import PropTypes from 'prop-types';
// @mui
import {
  Box,
  Card,
  Link,
  Typography,
  Stack,
  CardActionArea,
  CardMedia,
  CardContent,
  Button,
  CardActions
} from '@mui/material';
import { styled } from '@mui/material/styles';
// utils
import { fCurrency } from '../../../utils/formatNumber';
// components
import Label from '../../../components/label';
import { ColorPreview } from '../../../components/color-utils';
import {useNavigate} from "react-router-dom";

// ----------------------------------------------------------------------

const StyledProductImg = styled('img')({
  top: 0,
  width: '100%',
  height: '100%',
  objectFit: 'cover',
  position: 'absolute',
});

// ----------------------------------------------------------------------

ShopProductCard.propTypes = {
  product: PropTypes.object,
};

export default function ShopProductCard(props) {
  console.log(props)
  const id = props.id;
  const imgPath = `../assets/img/${id}.jpg`;
  const navigate = useNavigate();
  const handleNavigation = () => navigate(`/item/${id}`);

  return (
      <Card className='animate__animated animate__fadeIn' raised>
        <CardActionArea>
          <CardMedia
              component='img'
              height='260'
              image={imgPath}
              alt={id}
              onClick={handleNavigation}
          />
          <CardContent>
            <Typography variant='body2' noWrap>
              {props.jewelleryType.charAt(0).toUpperCase() + props.jewelleryType.slice(1)} {id}
            </Typography>
          </CardContent>
        </CardActionArea>
        <CardActions sx={{ display: 'flex', justifyContent: 'space-around' }}>
          <Button size='small' onClick={handleNavigation}>
            Details
          </Button>
          <Typography variant='subtitle2' color='text.secondary' align='right'>
            {`$${props.item.mrp}`}
          </Typography>
        </CardActions>
      </Card>
  );
}
