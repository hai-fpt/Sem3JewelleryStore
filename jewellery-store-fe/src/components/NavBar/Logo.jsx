import React from 'react';
import { Link } from 'react-router-dom';

import Avatar from '@mui/material/Avatar';

const Logo = () => (
  <Avatar
    component={Link}
    to='/'
    src='../assets/img/jewellery-logo.png'
    alt='JewelleryLogo'
    loading='lazy'
  />
);

export default Logo;
