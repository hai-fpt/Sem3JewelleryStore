import React from 'react';

import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Container from '@mui/material/Container';

import SearchBar from './SearchBar';
import CartWidget from '../Cart/CartWidget';
import Logo from './Logo';
import MenuNavList from './MenuNavList';

import ThemeContext from '../../context/ThemeContext';

const NavBar = () => {
  const pages = ['Necklaces', 'Rings', 'Earrings'];

  return (
    <ThemeContext>
      <AppBar position='static' sx={{ bgcolor: '#000' }}>
        <Container maxWidth='xl'>
          <Toolbar disableGutters>
            <Logo />
            <MenuNavList pages={pages} />
            <Container maxWidth='xs' disableGutters>
              <SearchBar />
            </Container>
            <CartWidget />
          </Toolbar>
        </Container>
      </AppBar>
    </ThemeContext>
  );
};

export default NavBar;
