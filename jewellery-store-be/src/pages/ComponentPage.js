import { Helmet } from 'react-helmet-async';
// @mui
import {
  Grid,
  Button,
  Container,
  Stack,
  Typography,
  Snackbar,
  Modal,
  Backdrop,
  Fade,
  Box,
  TextField, TableContainer, Table, TableBody, TableFooter, TableRow, TablePagination, TableHead, TableCell
} from '@mui/material';
// components
import Iconify from '../components/iconify';
import { BlogPostCard, BlogPostsSort, BlogPostsSearch } from '../sections/@dashboard/blog';
// mock
import POSTS from '../_mock/blog';
import {Alert} from "@mui/lab";
import {ProductList} from "../sections/@dashboard/products";
import {useEffect, useState} from "react";
import SvgColor from "../components/svg-color";

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

export default function ComponentPage() {
  const [loading, setLoading] = useState(false);

  const [brand, setBrand] = useState([]);
  const [cat, setCat] = useState([]);
  const [cert, setCert] = useState([]);
  const [gold, setGold] = useState([]);
  const [prod, setProd] = useState([]);
  const initData = [];

  const [openBrand, setOpenBrand] = useState(false);
  const handleOpenBrand = () => setOpenBrand(true);
  const handleCloseBrand = () => {
    setOpenBrand(false);
  }
  const [openCat, setOpenCat] = useState(false);
  const handleOpenCat = () => setOpenCat(true);
  const handleCloseCat = () => setOpenCat(false);

  const [openCert, setOpenCert] = useState(false);
  const handleOpenCert = () => setOpenCert(true);
  const handleCloseCert = () => setOpenCert(false);

  const [openGold, setOpenGold] = useState(false);
  const handleOpenGold = () => setOpenGold(true);
  const handleCloseGold = () => setOpenGold(false);

  const [openProd, setOpenProd] = useState(false);
  const handleOpenProd = () => setOpenProd(true);
  const handleCloseProd = () => setOpenProd(false);

  const [successOpen, setSuccessOpen] = useState(false);
  const handleSuccessOpen = () => setSuccessOpen(true);
  const handleSuccessClose = () => setSuccessOpen(false);

  const [deleteSuccess, setDeleteSuccess] = useState(false);
  const handleDeleteSuccess = () => setDeleteSuccess(true);
  const handleDeleteClose = () => setDeleteSuccess(false);


  const [newBrand, setNewBrand] = useState([]);
  const [newCat, setNewCat] = useState([]);
  const [newCert, setNewCert] = useState([]);
  const [newGold, setNewGold] = useState([]);
  const [newProd, setNewProd] = useState([]);

  const handleInputChange = (e, stateSetter) => {
    const {name, value} = e.target;
    stateSetter((prevState) => ({
      ...prevState,
      [name]: value
    }));
  }

  const handleSubmit = (e, type) => {
    e.preventDefault();
    switch (type) {
      case "brand":
        fetch("https://localhost:7211/api/Brand", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(newBrand)
        })
            .then(res => console.log(res))
            .catch(err => console.error(err));
        setNewBrand(initData);
        handleSuccessOpen();
        handleCloseBrand();
        break;
      case "cat":
        fetch("https://localhost:7211/api/Cat", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(newCat)
        })
            .then(res => console.log(res))
            .catch(err => console.error(err));
        setNewCat(initData);
        handleSuccessOpen();
        handleCloseCat();
        break;
      case "cert":
        fetch("https://localhost:7211/api/Certify", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(newCert)
        })
            .then(res => console.log(res))
            .catch(err => console.error(err));
        setNewCert(initData);
        handleSuccessOpen();
        handleCloseCert();
        break;
      case "gold":
        fetch("https://localhost:7211/api/GoldKrt", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(newGold)
        })
            .then(res => console.log(res))
            .catch(err => console.error(err));
        setNewGold(initData);
        handleSuccessOpen();
        handleCloseGold();
        break;
      case "prod":
        fetch("https://localhost:7211/api/Prod", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(newProd)
        })
            .then(res => console.log(res))
            .catch(err => console.error(err));
        setNewProd(initData);
        handleSuccessOpen();
        handleCloseProd();
        break;
    }
  }

  const handleDelete = (id, type) => {
    switch (type) {
      case "brand":
        fetch(`https://localhost:7211/api/Brand/${id}`,{
          method: "DELETE"
        })
            .then(res => {handleDeleteSuccess();})
            .catch(err => console.error(err))
        break;
      case "cat":
        fetch(`https://localhost:7211/api/Cat/${id}`,{
          method: "DELETE"
        })
            .then(res => {handleDeleteSuccess();})
            .catch(err => console.error(err))
        break;
      case "cert":
        fetch(`https://localhost:7211/api/Certify/${id}`,{
          method: "DELETE"
        })
            .then(res => {handleDeleteSuccess();})
            .catch(err => console.error(err))
        break;
      case "gold":
        fetch(`https://localhost:7211/api/GoldKrt/${id}`,{
          method: "DELETE"
        })
            .then(res => {handleDeleteSuccess();})
            .catch(err => console.error(err))
        break;
      case "prod":
        fetch(`https://localhost:7211/api/Prod/${id}`,{
          method: "DELETE"
        })
            .then(res => {handleDeleteSuccess();})
            .catch(err => console.error(err))
        break;
    }
  }

  useEffect(() => {
    const interval = setInterval(() => {
      setLoading(true);
      try {
        fetch("https://localhost:7211/api/Brand")
            .then(res => res.json())
            .then(res => {
              setBrand(res.$values);
            });

        fetch("https://localhost:7211/api/Cat")
            .then(res => res.json())
            .then(res => {
              setCat(res.$values);
            })

        fetch("https://localhost:7211/api/Certify")
            .then(res => res.json())
            .then(res => setCert(res.$values));

        fetch("https://localhost:7211/api/GoldKrt")
            .then(res => res.json())
            .then(res => setGold(res.$values));

        fetch("https://localhost:7211/api/Prod")
            .then(res => res.json())
            .then(res => setProd(res.$values));

        setLoading(false);
      } catch (err) {
        console.error(err);
      }
      return () => clearInterval(interval);
    }, 3000)
  }, [])

  return (
    <>
      <Container>
        <Typography variant="h4" sx={{mb: 5}}>
          Components
        </Typography>
        <div>
          <Snackbar
              open={successOpen}
              autoHideDuration={6000}
              onClose={handleSuccessClose}
          >
            <Alert severity="success">Success!</Alert>
          </Snackbar>
          <Snackbar
              open={deleteSuccess}
              autoHideDuration={3000}
              onClose={handleDeleteClose}
          >
            <Alert severity="error">Deleted</Alert>
          </Snackbar>
        </div>
        <Modal
            open={openBrand}
            onClose={handleCloseBrand}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
              backdrop: {
                timeout: 500
              }
            }}
        >
          <Fade in={openBrand}>
            <Box sx={boxStyle}>
              <Typography variant="h6" component="h2">
                Add new brand
              </Typography>
              <Typography sx={{mt: 2}}>
                <Box
                    sx={{
                      '& .MuiTextField-root': { m: 1, width: '25ch'}
                    }}
                    component="form"
                    noValidate
                    autoComplete="off"
                    // onSubmit={handleSubmit}
                >
                  <div>
                    <TextField
                        label="ID"
                        name="brandId"
                        value={newBrand.brandId}
                        onChange={(e) => handleInputChange(e, setNewBrand)}
                    />
                    <TextField
                        label="Brand Name"
                        name="brandType"
                        value={newBrand.brandType}
                        onChange={(e) => handleInputChange(e, setNewBrand)}
                    />
                  </div>
                </Box>
              </Typography>
              <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}}
                    onClick={(e) => handleSubmit(e, "brand")}
                >
                  Add Brand
                </Button>
              </div>
            </Box>
          </Fade>
        </Modal>
        <Modal
            open={openCat}
            onClose={handleCloseCat}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
              backdrop: {
                timeout: 500
              }
            }}
        >
          <Fade in={openCat}>
            <Box sx={boxStyle}>
              <Typography variant="h6" component="h2">
                Add new category
              </Typography>
              <Typography sx={{mt: 2}}>
                <Box
                    sx={{
                      '& .MuiTextField-root': { m: 1, width: '25ch'}
                    }}
                    component="form"
                    noValidate
                    autoComplete="off"
                    // onSubmit={handleSubmit}
                >
                  <div>
                    <TextField
                        label="ID"
                        name="catId"
                        value={newCat.catId}
                        onChange={(e) => handleInputChange(e, setNewCat)}
                    />
                    <TextField
                        label="Category Name"
                        name="catName"
                        value={newCat.catName}
                        onChange={(e) => handleInputChange(e, setNewCat)}
                    />
                  </div>
                </Box>
              </Typography>
              <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}}
                    onClick={(e) => handleSubmit(e, "cat")}
                >
                  Add Category
                </Button>
              </div>
            </Box>
          </Fade>
        </Modal>
        <Modal
            open={openCert}
            onClose={handleCloseCert}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
              backdrop: {
                timeout: 500
              }
            }}
        >
          <Fade in={openCert}>
            <Box sx={boxStyle}>
              <Typography variant="h6" component="h2">
                Add new certificate
              </Typography>
              <Typography sx={{mt: 2}}>
                <Box
                    sx={{
                      '& .MuiTextField-root': { m: 1, width: '25ch'}
                    }}
                    component="form"
                    noValidate
                    autoComplete="off"
                    // onSubmit={handleSubmit}
                >
                  <div>
                    <TextField
                        label="ID"
                        name="certifyId"
                        value={newCert.certifyId}
                        onChange={(e) => handleInputChange(e, setNewCert)}
                    />
                    <TextField
                        label="Certificate Name"
                        name="certifyType"
                        value={newCert.certifyType}
                        onChange={(e) => handleInputChange(e, setNewCert)}
                    />
                  </div>
                </Box>
              </Typography>
              <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}}
                    onClick={(e) => handleSubmit(e, "cert")}
                >
                  Add Certificate
                </Button>
              </div>
            </Box>
          </Fade>
        </Modal>
        <Modal
            open={openGold}
            onClose={handleCloseGold}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
              backdrop: {
                timeout: 500
              }
            }}
        >
          <Fade in={openGold}>
            <Box sx={boxStyle}>
              <Typography variant="h6" component="h2">
                Add new brand
              </Typography>
              <Typography sx={{mt: 2}}>
                <Box
                    sx={{
                      '& .MuiTextField-root': { m: 1, width: '25ch'}
                    }}
                    component="form"
                    noValidate
                    autoComplete="off"
                    // onSubmit={handleSubmit}
                >
                  <div>
                    <TextField
                        label="ID"
                        name="goldTypeId"
                        value={newGold.goldTypeId}
                        onChange={(e) => handleInputChange(e, setNewGold)}
                    />
                    <TextField
                        label="Gold Karat"
                        name="goldCrt"
                        value={newGold.goldCrt}
                        onChange={(e) => handleInputChange(e, setNewGold)}
                    />
                  </div>
                </Box>
              </Typography>
              <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}}
                    onClick={(e) => handleSubmit(e, "gold")}
                >
                  Add Gold Karat
                </Button>
              </div>
            </Box>
          </Fade>
        </Modal>
        <Modal
            open={openProd}
            onClose={handleCloseProd}
            closeAfterTransition
            slots={{backdrop: Backdrop}}
            slotProps={{
              backdrop: {
                timeout: 500
              }
            }}
        >
          <Fade in={openProd}>
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
                    // onSubmit={handleSubmit}
                >
                  <div>
                    <TextField
                        label="ID"
                        name="prodId"
                        value={newProd.prodId}
                        onChange={(e) => handleInputChange(e, setNewProd)}
                    />
                    <TextField
                        label="Product Name"
                        name="prodType"
                        value={newProd.prodType}
                        onChange={(e) => handleInputChange(e, setNewProd)}
                    />
                  </div>
                </Box>
              </Typography>
              <div style={{display: "flex", justifyContent: "center", marginTop: 20}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}}
                    onClick={(e) => handleSubmit(e,"prod")}
                >
                  Add Product
                </Button>
              </div>
            </Box>
          </Fade>
        </Modal>
        <Grid container spacing={2}>
          <Grid item xs={6} rowSpacing={6}>
            <Box sx={{border: 1}}>
              <Typography variant="h6" sx={{mb: 2, display: "flex", justifyContent: "center"}}>
                Brands
              </Typography>
              <TableContainer>
                <Table sx={{ minWidth: 50}}>
                  <TableHead>
                    <TableRow>
                      <TableCell align="center">ID</TableCell>
                      <TableCell align="center">Name</TableCell>
                      <TableCell align="center">Action</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {brand != null ? (
                        brand.map((brand) => (
                              <TableRow
                                  key={brand.brandId}
                              >
                                <TableCell component="th" align="center">
                                  {brand.brandId}
                                </TableCell>
                                <TableCell align="center">
                                  {brand.brandType}
                                </TableCell>
                                <TableCell sx={{width: 20}} align="center">
                                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(brand.brandId, "brand")}>
                                  Delete
                                </Button>
                                </TableCell>
                              </TableRow>
                          ))
                    ): ("loading")}
                  </TableBody>
                </Table>
              </TableContainer>
              <div style={{display: "flex", justifyContent: "center", marginTop: 10, marginBottom: 10}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpenBrand}>
                  Add New Brand
                </Button>
              </div>
            </Box>
          </Grid>
          <Grid item xs={6}>
            <Box sx={{border: 1}}>
              <Typography variant="h6" sx={{mb: 2, display: "flex", justifyContent: "center"}}>
                Categories
              </Typography>
              <TableContainer>
                <Table sx={{ minWidth: 50}}>
                  <TableHead>
                    <TableRow>
                      <TableCell align="center">ID</TableCell>
                      <TableCell align="center">Name</TableCell>
                      <TableCell align="center">Action</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {cat != null ? (
                        cat.map((cat) => (
                            <TableRow
                                key={cat.catId}
                            >
                              <TableCell component="th" align="center">
                                {cat.catId}
                              </TableCell>
                              <TableCell align="center">
                                {cat.catName}
                              </TableCell>
                              <TableCell sx={{width: 20}} align="center">
                                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(cat.catId, "cat")}>
                                  Delete
                                </Button>
                              </TableCell>
                            </TableRow>
                        ))
                    ): ("loading")}
                  </TableBody>
                </Table>
              </TableContainer>
              <div style={{display: "flex", justifyContent: "center", marginTop: 10, marginBottom: 10}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpenCat}>
                  Add New Category
                </Button>
              </div>
            </Box>
          </Grid>
          <Grid item xs={6}>
            <Box sx={{border: 1}}>
              <Typography variant="h6" sx={{mb: 2, display: "flex", justifyContent: "center"}}>
                Certificate
              </Typography>
              <TableContainer>
                <Table sx={{ minWidth: 50}}>
                  <TableHead>
                    <TableRow>
                      <TableCell align="center">ID</TableCell>
                      <TableCell align="center">Name</TableCell>
                      <TableCell align="center">Action</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {cert != null ? (
                        cert.map((cert) => (
                            <TableRow
                                key={cert.certifyId}
                            >
                              <TableCell component="th" align="center">
                                {cert.certifyId}
                              </TableCell>
                              <TableCell align="center">
                                {cert.certifyType}
                              </TableCell>
                              <TableCell sx={{width: 20}} align="center">
                                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(cert.certifyId, "cert")}>
                                  Delete
                                </Button>
                              </TableCell>
                            </TableRow>
                        ))
                    ): ("loading")}
                  </TableBody>
                </Table>
              </TableContainer>
              <div style={{display: "flex", justifyContent: "center", marginTop: 10, marginBottom: 10}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpenCert}>
                  Add New Certificate
                </Button>
              </div>
            </Box>
          </Grid>
          <Grid item xs={6}>
            <Box sx={{border: 1}}>
              <Typography variant="h6" sx={{mb: 2, display: "flex", justifyContent: "center"}}>
                Gold Carat
              </Typography>
              <TableContainer>
                <Table sx={{ minWidth: 50}}>
                  <TableHead>
                    <TableRow>
                      <TableCell align="center">ID</TableCell>
                      <TableCell align="center">Name</TableCell>
                      <TableCell align="center">Action</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {gold != null ? (
                        gold.map((gold) => (
                            <TableRow
                                key={gold.goldTypeId}
                            >
                              <TableCell component="th" align="center">
                                {gold.goldTypeId}
                              </TableCell>
                              <TableCell align="center">
                                {gold.goldCrt}
                              </TableCell>
                              <TableCell sx={{width: 20}} align="center">
                                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(gold.goldTypeId, "gold")}>
                                  Delete
                                </Button>
                              </TableCell>
                            </TableRow>
                        ))
                    ): ("loading")}
                  </TableBody>
                </Table>
              </TableContainer>
              <div style={{display: "flex", justifyContent: "center", marginTop: 10, marginBottom: 10}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpenGold}>
                  Add New Gold Karat
                </Button>
              </div>
            </Box>
          </Grid>
          <Grid item xs={6}>
            <Box sx={{border: 1}}>
              <Typography variant="h6" sx={{mb: 2, display: "flex", justifyContent: "center"}}>
                Product
              </Typography>
              <TableContainer>
                <Table sx={{ minWidth: 50}}>
                  <TableHead>
                    <TableRow>
                      <TableCell align="center">ID</TableCell>
                      <TableCell align="center">Name</TableCell>
                      <TableCell align="center">Action</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {prod != null ? (
                        prod.map((prod) => (
                            <TableRow
                                key={prod.prodId}
                            >
                              <TableCell component="th" align="center">
                                {prod.prodId}
                              </TableCell>
                              <TableCell align="center">
                                {prod.prodType}
                              </TableCell>
                              <TableCell sx={{width: 20}} align="center">
                                <Button sx={{color: "red"}} size='small' onClick={() => handleDelete(prod.prodId, "prod")}>
                                  Delete
                                </Button>
                              </TableCell>
                            </TableRow>
                        ))
                    ): ("loading")}
                  </TableBody>
                </Table>
              </TableContainer>
              <div style={{display: "flex", justifyContent: "center", marginTop: 10, marginBottom: 10}}>
                <Button variant="contained" sx={{backgroundColor: "#2196f3"}} onClick={handleOpenProd}>
                  Add New Product
                </Button>
              </div>
            </Box>
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
