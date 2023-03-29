import { Fragment } from 'react';
import Paper from '@mui/material/Paper';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';

const ItemDescription = (props) => {
    const item = props.characteristics.item;

    return (
        <Paper elevation={8} sx={{ my: 3, backgroundColor: "beige", color: "black" }}>
            <List>
                <ListItem>
                    <Typography variant="caption">
                        <p>Id: {item.styleCode}</p>
                        <p>Brand: {item.brand.brandType}</p>
                        <p>Category: {item.cat.catName}</p>
                        <p>Certificate: {item.certify.certifyType}</p>
                        <p>Gold Type: {item.goldType.goldCrt}</p>
                        <p>Gold Rate: {item.goldRate}</p>
                    </Typography>
                </ListItem>
            </List>
        </Paper>
    );
}

export default ItemDescription;
