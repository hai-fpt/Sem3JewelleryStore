import GoBackBtn from '../ui/GoBackBtn';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';

const FailedTransaction = ({ error }) => {
  return (
    <>
      <Typography variant='h5' align='center' gutterBottom>
        There was an error in your transaction. Contact an administrator to resolve the problem.
      </Typography>

      <Divider
        sx={{
          my: { xs: 3, md: 6 },
          mx: { xs: 'auto', md: 0 },
        }}
        variant='middle'
      />

      <Typography variant='subtitle1' align='center' gutterBottom>
        {`Details: ${error}`}
      </Typography>

      <GoBackBtn />
    </>
  );
};
export default FailedTransaction;
