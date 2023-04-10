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
    CardActions, Snackbar, Backdrop, Fade, TextField, Modal
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

const boxStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: '37rem',
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
}

// ----------------------------------------------------------------------

ShopProductCard.propTypes = {
    product: PropTypes.object,
};

export default function ShopProductCard(props) {
    const id = props.Id;
    const imgPath = `../assets/img/${id}.jpg`;
    const handleDelete = (id) => {
        fetch(`https://localhost:7211/api/JewelType/${id}`, {
            method: "DELETE"
        })
            .then(res => {
                window.location.reload();
            })
            .catch(err => console.error(err))
    }
    const [editOpen, setEditOpen] = useState(false);
    const handleEditOpen = () => setEditOpen(true);
    const handleEditClose = () => setEditOpen(false);

    const [editItem, setEditItem] = useState({
        id: id,
        jewelleryType: props.JewelleryType,
        itemId: props.itemId,
        imgPath: props.ImgPath
    })
    const handleInputChange = (e) => {
        setEditItem({...editItem, [e.target.name]: e.target.value});
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`https://localhost:7211/api/JewelType/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(editItem)
        })
            .then(() => {
                setEditItem({...editItem});
                handleEditClose();
            })
            .catch(err => console.error(err));
    }

    return (
        <Card className='animate__animated animate__fadeIn' raised>
            <CardActionArea>
                <CardMedia
                    component='img'
                    height='260'
                    image={imgPath}
                    alt={id}
                />
                <CardContent>
                    <Typography variant='body2' noWrap>
                        {props.JewelleryType.charAt(0).toUpperCase() + props.JewelleryType.slice(1)} {id}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions sx={{display: 'flex', justifyContent: 'space-around'}}>
                <Button size='small' onClick={handleEditOpen}>
                    Details
                </Button>
                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(props.Id)}>
                    Delete
                </Button>
            </CardActions>
            <Modal
                open={editOpen}
                onClose={handleEditClose}
                closeAfterTransition
                slots={{backdrop: Backdrop}}
                slotProps={{
                    backdrop: {
                        timeout: 500
                    }
                }}
            >
                <Fade in={editOpen}>
                    <Box sx={boxStyle}>
                        <Typography variant="h6" component="h2">
                            Edit product
                        </Typography>
                        <Typography sx={{mt: 2}}>
                            <Box
                                sx={{
                                    '& .MuiTextField-root': {m: 1, width: '25ch'}
                                }}
                                component="form"
                                noValidate
                                autoComplete="off"
                                onSubmit={handleSubmit}>
                                <div>
                                    <TextField
                                        label="ID"
                                        name="id"
                                        value={props.id}
                                        helperText={id}
                                        onChange={handleInputChange}
                                    />
                                    <TextField
                                        label="Jewellery Type"
                                        name="jewelleryType"
                                        value={props.jewelleryType}
                                        helperText={props.JewelleryType}
                                        onChange={handleInputChange}
                                    />

                                    <TextField
                                        label="Item ID"
                                        name="itemId"
                                        value={props.itemId}
                                        helperText={props.ItemId}
                                        onChange={handleInputChange}
                                    />

                                    <TextField
                                        label="Price"
                                        name="mrp"
                                        value={props.mrp}
                                        helperText={props.Mrp}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </Box>
                        </Typography>
                        <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                            <Button
                                variant="contained"
                                component="label"
                                sx={{
                                    marginRight: 1
                                }}
                            >
                                Upload Image
                                <input type="file" hidden />
                            </Button>
                            <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleSubmit}>
                                Edit Product
                            </Button>
                        </div>
                    </Box>
                </Fade>
            </Modal>
        </Card>
    );
}
