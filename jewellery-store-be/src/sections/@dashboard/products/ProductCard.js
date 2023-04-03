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
    CardActions, Snackbar
} from '@mui/material';
import {styled} from '@mui/material/styles';
// utils
import {fCurrency} from '../../../utils/formatNumber';
// components
import Label from '../../../components/label';
import {ColorPreview} from '../../../components/color-utils';
import {useNavigate} from "react-router-dom";
import {Alert} from "@mui/lab";
import {useState} from "react";

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
    const id = props.Id;
    const imgPath = `../assets/img/${id}.jpg`;
    const navigate = useNavigate();
    const handleNavigation = () => navigate(`/item/${id}`);
    const handleDelete = (id) => {
        fetch(`https://localhost:7211/api/JewelType/${id}`, {
            method: "DELETE"
        })
            .then(res => {
                handleOpen();
                window.location.reload();
            })
            .catch(err => console.error(err))
    }
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

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
                        {props.JewelleryType.charAt(0).toUpperCase() + props.JewelleryType.slice(1)} {id}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions sx={{display: 'flex', justifyContent: 'space-around'}}>
                <Button size='small' onClick={handleNavigation}>
                    Details
                </Button>
                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(props.Id)}>
                    Delete
                </Button>
            </CardActions>
            <Snackbar
                open={open}
                autoHideDuration={6000}
                onClose={handleClose}
            >
                <Alert severity="error">Deleted</Alert>
            </Snackbar>
        </Card>
    );
}
