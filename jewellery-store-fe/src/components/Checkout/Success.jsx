import { Link } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import Box from '@mui/system/Box';
import Button from '@mui/material/Button';

const Success = ({ orderId }) => {
  return (
    <>
      <Box
        display='flex'
        justifyContent='center'
        className='animate__animated animate__pulse'
      >
        <img src='/assets/img/jewellery-logo.png' alt='success' width={200} />
      </Box>
      <Typography variant='h5' align='center' gutterBottom>
          Thank you ver much for your purchase!
      </Typography>

        <Typography variant='subtitle1'>
            Your purchase code is:{' '}
            <Typography variant='subtitle' color='primary'>
                {orderId}
            </Typography>
            . We have sent an email to your address with the details of your purchase and we will be in touch with you to follow up on the status of your order.
        </Typography>

      <Button
        color='primary'
        component={Link}
        to='/'
        sx={{ mt: 3, mx: 'auto', display: 'block', textAlign: 'center' }}
      >
        Back to top
      </Button>
    </>
  );
};
export default Success;
