import React from 'react';
import { useCounter } from '../../hooks/useCounter';

import InputAdornment from '@mui/material/InputAdornment';
import OutlinedInput from '@mui/material/OutlinedInput';
import FormControl from '@mui/material/FormControl';
import FormHelperText from '@mui/material/FormHelperText';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import IconButton from '@mui/material/IconButton';
import Stack from '@mui/material/Stack';
import Box from '@mui/system/Box';
import Button from '@mui/material/Button';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

const ItemCount = ({ stock = 0, initial = 1, onAdd }) => {
  const { counter, increment, decrement } = useCounter(initial);

  const handleAddBtnClick = () => onAdd(counter);

  return (
    <Box>
      <Box
        display='flex'
        flexDirection={{ xs: 'column', sm: 'row' }}
        alignItems='center'
      >
        <FormControl
          sx={{
            m: 1,
            width: '11ch',
          }}
          variant='outlined'
        >
          <OutlinedInput
            id='outlined-adornment-weight'
            // endAdornment={<InputAdornment position='end'>A</InputAdornment>}
            aria-describedby='outlined-weight-helper-text'
            type='number'
            sx={{
                color: "black",
                border: "1px solid black",
                borderRadius: 1
            }}
            inputProps={{
              'aria-label': 'weight',
              type: 'number',
              value: stock > 0 && counter,
              max: 10,
              min: 1,
              disabled: true,
            }}
          />
          <FormHelperText id='outlined-weight-helper-text' style={{color: "black"}}>
            {stock < 1 ? 'Out of stock' : `Available stock: ${stock}`}
          </FormHelperText>
        </FormControl>
        <Stack direction={{ xs: 'row-reverse', sm: 'column' }} style={{color: "black"}}>
          <IconButton
            aria-label='addButton'
            disabled={stock < 1 || (counter === stock && true)}
            onClick={increment}
            style={{color: "black"}}
          >
            <AddIcon />
          </IconButton>
          <IconButton
            aria-label='removeButtom'
            disabled={counter < 1 || (stock < 1 && true)}
            onClick={decrement}
            style={{color: "black"}}
          >
            <RemoveIcon />
          </IconButton>
        </Stack>
      </Box>

      <Button
        aria-label='addToCart'
        variant='outlined'
        color='inherit'
        startIcon={<ShoppingCartIcon />}
        sx={{ mt: 1 }}
        onClick={handleAddBtnClick}
        disabled={stock < 1 || (counter < 1 && true)}
      >
        Add to cart
      </Button>
    </Box>
  );
};

export default ItemCount;
