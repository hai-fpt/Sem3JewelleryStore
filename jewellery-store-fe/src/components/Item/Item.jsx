import { useNavigate } from 'react-router-dom';

import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';


const Item = (props) => {
  const id = props.id;
  const imgPath = `../assets/img/${id}.jpg`;
  const navigate = useNavigate();
  const handleNavigation = () => navigate(`/item/${id}`);

  return (
    <Card className='animate__animated animate__fadeIn' raised style={{backgroundColor: "rgb(203, 177, 124)"}}>
      <CardActionArea>
        <CardMedia
          component='img'
          height='260'
          image={imgPath}
          alt={id}
          onClick={handleNavigation}
        />
        <CardContent>
          <Typography variant='body2' color='text.secondary' noWrap>
            {props.jewelleryType.charAt(0).toUpperCase() + props.jewelleryType.slice(1)} {id}
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions sx={{ display: 'flex', justifyContent: 'space-around' }}>
        <Button size='small' sx={{color: "white", "&:hover": {backgroundColor: "rgb(203,203,124)"}}} onClick={handleNavigation}>
          Details
        </Button>
        <Typography variant='subtitle2' color='text.secondary' align='right'>
          {`$${props.item.mrp}`}
        </Typography>
      </CardActions>
    </Card>
  );
};

export default Item;
