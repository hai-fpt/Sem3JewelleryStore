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

export default function IncomePage() {
    const [data, setData] = useState(null);

    useEffect(() => {
        fetch("https://localhost:7211/api/Income")
            .then(res => res.json())
            .then(data => setData(data.$values))
    }, [])

    console.log(data)
    const sum = data ? data.reduce((total, item) => total + parseFloat(item.amount), 0).toFixed(2) : 0;

    return (
        <>
            <Container>
                <Typography variant="h4" sx={{mb: 5}}>
                    Income
                </Typography>
                <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell align={"center"}>Order ID</TableCell>
                                <TableCell align={"center"}>Amount</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data != null ? (
                                data.map((data) => (
                                    <TableRow key={data.orderId}>
                                        <TableCell component="th" align={"center"}>
                                            {data.orderId}
                                        </TableCell>
                                        <TableCell align={"center"}>
                                            ${data.amount}
                                        </TableCell>
                                    </TableRow>
                                ))
                            ) : ("loading")}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                <TableCell>Total:</TableCell>
                                <TableCell align="right">${sum}</TableCell>
                            </TableRow>
                        </TableFooter>
                    </Table>
                </TableContainer>
            </Container>
        </>
    );
}
