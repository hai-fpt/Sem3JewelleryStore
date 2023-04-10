import {useEffect, useState} from 'react';
// @mui
import {
    Backdrop,
    Box,
    Button,
    CircularProgress,
    Container,
    Fade,
    Modal,
    Snackbar,
    Stack,
    TextField,
    Typography
} from '@mui/material';
// components
import {ProductSort, ProductList, ProductCartWidget, ProductFilterSidebar} from '../sections/@dashboard/products';

import {useParams} from "react-router-dom";
import {Alert} from "@mui/lab";
// ----------------------------------------------------------------------

const boxStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -39%)',
    width: '37rem',
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
}


export default function ProductsPage() {
    const [items, setItems] = useState(null);
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => {
        resetFormData();
        setOpen(false);
    }
    const [open2, setOpen2] = useState(false);
    const handleOpen2 = () => setOpen2(true);
    const handleClose2 = () => {
        resetItemData();
        setOpen2(false);
    }
    const [successOpen, setSuccessOpen] = useState(false);
    const handleSuccessOpen = () => setSuccessOpen(true);
    const handleSuccessClose = () => setSuccessOpen(false);
    const [loading, setLoading] = useState(false);
    const {categoryId, term} = useParams();

    useEffect(() => {
        const interval = setInterval(() => {
            setLoading(true);
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
        }, 3000);
        return () => clearInterval(interval);
    }, [categoryId, term]);

    const [formData, setFormData] = useState({
        id: '',
        jewelleryType: '',
        itemId: '',
        imgPath: '',
        mrp: ''
    });
    const initFormData = {
        id: '',
        jewelleryType: '',
        itemId: '',
        imgPath: '',
        mrp: ''
    }

    const [itemData, setItemData] = useState({
        styleCode: '',
        pairs: 0,
        brandId: '',
        quantity: 0,
        catId: '',
        prodQuality: '',
        certifyId: '',
        prodId: '',
        goldTypeId: '',
        goldWt: 0,
        stoneWt: 0,
        netGold: 0,
        wstgPer: 0,
        wstg: 0,
        totGrossWt: 0,
        goldRate: 0,
        goldAmt: 0,
        goldMaking: 0,
        stoneMaking: 0,
        otherMaking: 0,
        totMaking: 0,
        mrp: 0,
    });

    const initItemData = {
        styleCode: '',
        pairs: 0,
        brandId: '',
        quantity: 0,
        catId: '',
        prodQuality: '',
        certifyId: '',
        prodId: '',
        goldTypeId: '',
        goldWt: 0,
        stoneWt: 0,
        netGold: 0,
        wstgPer: 0,
        wstg: 0,
        totGrossWt: 0,
        goldRate: 0,
        goldAmt: 0,
        goldMaking: 0,
        stoneMaking: 0,
        otherMaking: 0,
        totMaking: 0,
        mrp: 0,
    }



    const handleInputChange = (e) => {
        const {name, value} = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    }

    const handleItemInputChange = (e) => {
        const {name, value} = e.target;
        setItemData({
            ...itemData,
            [name]: value
        });
    }

    useEffect(() => {
        setFormData(prevState => ({
            ...prevState,
            imgPath: `src/assets/img/${prevState.id}`
        }))
    }, [formData.id])

    console.log(formData)
    const resetFormData = () => {
        setFormData(initFormData);
    }

    const resetItemData = () => {
        setItemData(initItemData);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch("https://localhost:7211/api/JewelType", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(formData)
        })
            .then(handleSuccessOpen)
            .catch(err => {
                console.error(err);
            });
        handleClose();
        resetFormData();
    }

    const handleSubmitItem = (e) => {
        e.preventDefault();
        fetch("https://localhost:7211/api/Items", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(itemData)
        })
            .then(handleSuccessOpen)
            .catch(err => {
                console.error(err)
            })
        handleClose2();
        resetItemData();
    }

    return (
        <>
            <Container>
                <Typography variant="h4" sx={{mb: 5}}>
                    Products
                </Typography>

                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpen}>
                    Add New Jewel
                </Button>

                <Button variant="contained" sx={{backgroundColor: "#2196f3", marginLeft: 10}} onClick={handleOpen2}>
                    Add New Item
                </Button>
                <div>
                    <Snackbar
                        open={successOpen}
                        autoHideDuration={6000}
                        onClose={handleSuccessClose}
                        >
                        <Alert severity="success">Success!</Alert>
                    </Snackbar>
                </div>
                <Modal
                    open={open}
                    onClose={handleClose}
                    closeAfterTransition
                    slots={{backdrop: Backdrop}}
                    slotProps={{
                        backdrop: {
                            timeout: 500
                        }
                    }}
                >
                    <Fade in={open}>
                        <Box sx={boxStyle}>
                            <Typography variant="h6" component="h2">
                                Add new product
                            </Typography>
                            <Typography sx={{mt: 2}}>
                                <Box
                                    sx={{
                                        '& .MuiTextField-root': { m: 1, width: '25ch'}
                                    }}
                                    component="form"
                                    noValidate
                                    autoComplete="off"
                                    onSubmit={handleSubmit}>
                                    <div>
                                        <TextField
                                            label="ID"
                                            name="id"
                                            value={formData.id}
                                            onChange={handleInputChange}
                                        />
                                        <TextField
                                            label="Jewellery Type"
                                            name="jewelleryType"
                                            value={formData.jewelleryType}
                                            onChange={handleInputChange}
                                        />

                                        <TextField
                                            label="Item ID"
                                            name="itemId"
                                            value={formData.itemId}
                                            onChange={handleInputChange}
                                        />

                                        <TextField
                                            label="Price"
                                            name="mrp"
                                            value={formData.mrp}
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
                                    Add Product
                                </Button>
                            </div>
                        </Box>
                    </Fade>
                </Modal>
                <Modal
                    open={open2}
                    onClose={handleClose2}
                    closeAfterTransition
                    sx={{
                        position:'absolute',
                        top:'10%',
                        left:'10%',
                        overflow:'scroll',
                        height:'100%',
                        display:'block'
                    }}
                    slots={{backdrop: Backdrop}}
                    slotProps={{
                        backdrop: {
                            timeout: 500
                        }
                    }}
                    >
                    <Fade in={open2}>
                        <Box sx={boxStyle} style={{width: "37rem"}}>
                            <Typography variant="h6" component="h2">
                                Add new item
                            </Typography>
                            <Typography sx={{mt: 2}}>
                                <Box
                                    sx={{
                                        '& .MuiTextField-root': { m: 1, width: '25ch'}
                                    }}
                                    component="form"
                                    noValidate
                                    autoComplete="off"
                                    onSubmit={handleSubmit}>
                                    <div>
                                        <TextField
                                            label="Style Code"
                                            name="styleCode"
                                            value={itemData.styleCode}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Pairs"
                                            name="pairs"
                                            type="number"
                                            value={itemData.pairs}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Brand ID"
                                            name="brandId"
                                            value={itemData.brandId}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Quantity"
                                            name="quantity"
                                            type="number"
                                            value={itemData.quantity}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Category ID"
                                            name="catId"
                                            value={itemData.catId}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Product Quality"
                                            name="prodQuality"
                                            value={itemData.prodQuality}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Certify ID"
                                            name="certifyId"
                                            value={itemData.certifyId}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Product ID"
                                            name="prodId"
                                            value={itemData.prodId}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Gold Type ID"
                                            name="goldTypeId"
                                            value={itemData.goldTypeId}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Gold Weight"
                                            name="goldWt"
                                            type="number"
                                            value={itemData.goldWt}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Stone Weight"
                                            name="stoneWt"
                                            type="number"
                                            value={itemData.stoneWt}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Net Gold"
                                            name="netGold"
                                            type="number"
                                            value={itemData.netGold}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="WstgPer"
                                            name="wstgPer"
                                            type="number"
                                            value={itemData.wstgPer}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Wstg"
                                            name="wstg"
                                            type="number"
                                            value={itemData.wstg}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Total Gross Weight"
                                            name="totGrossWt"
                                            type="number"
                                            value={itemData.totGrossWt}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Gold Rate"
                                            name="goldRate"
                                            type="number"
                                            value={itemData.goldRate}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Gold Amount"
                                            name="goldAmt"
                                            type="number"
                                            value={itemData.goldAmt}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Gold Making"
                                            name="goldMaking"
                                            type="number"
                                            value={itemData.goldMaking}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Stone Making"
                                            name="stoneMaking"
                                            type="number"
                                            value={itemData.stoneMaking}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Other Making"
                                            name="otherMaking"
                                            type="number"
                                            value={itemData.otherMaking}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Total Making"
                                            name="totMaking"
                                            type="number"
                                            value={itemData.totMaking}
                                            onChange={handleItemInputChange}
                                        />
                                        <TextField
                                            label="Medium Retail Price"
                                            name="mrp"
                                            type="number"
                                            value={itemData.mrp}
                                            onChange={handleItemInputChange}
                                            />
                                    </div>
                                </Box>
                            </Typography>
                            <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleSubmitItem}>
                                    Add Item
                                </Button>
                            </div>
                        </Box>
                    </Fade>
                </Modal>

                <Stack direction="row" flexWrap="wrap-reverse" alignItems="center" justifyContent="flex-end"
                       sx={{mb: 5}}>
                    <Stack direction="row" spacing={1} flexShrink={0} sx={{my: 1}}>
                    </Stack>
                </Stack>
                {items != null ? (
                    <ProductList products={items}/>
                ) : (
                    <Box display='flex' flexDirection='column' alignItems='center' mt={10}>
                        <CircularProgress />
                        <Typography variant='overline'>Loading</Typography>
                    </Box>
                )}
            </Container>
        </>
    );
}
