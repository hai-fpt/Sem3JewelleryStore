import {useMemo, useState} from 'react';
import { useNavigate } from 'react-router-dom';

import debounce from '../../helpers/debounce';

import { styled, alpha } from '@mui/material/styles';
import InputBase from '@mui/material/InputBase';
import SearchIcon from '@mui/icons-material/Search';
import ItemList from "../Item/ItemList";
import ItemListCointainer from "../Item/ItemListCointainer";
import Item from "../Item/Item";
import Grid from "@mui/material/Grid";
import {Button, CardActionArea, CardActions, Select} from "@mui/material";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Card from "@mui/material/Card";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";

//styled components
const Search = styled('div')(({ theme }) => ({
  position: 'relative',
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.15),
  '&:hover': {
    backgroundColor: alpha(theme.palette.common.white, 0.25),
  },
  marginRight: theme.spacing(2),
  marginLeft: 0,
  width: '100%',
  [theme.breakpoints.up('sm')]: {
    marginLeft: theme.spacing(3),
    width: 'auto',
  },
}));

const SearchIconWrapper = styled('div')(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: '100%',
  position: 'absolute',
  pointerEvents: 'none',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
}));

const SearchSelectWrapper = styled("div")(({theme}) => ({
  padding: theme.spacing(0),
  height: '100%',
  position: 'absolute',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'flex-end',
  right: 0,
  top: 0
}))

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: 'inherit',
  '& .MuiInputBase-input': {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('md')]: {
      width: '39ch',
    },
  },
}));
const SearchResults = styled('div')(({ theme }) => ({
  color: 'black',
  position: 'absolute',
  top: 'calc(100% + 4px)',
  left: 0,
  right: 0,
  zIndex: theme.zIndex.tooltip,
  maxHeight: '200px',
  overflowY: 'auto',
  backgroundColor: theme.palette.background.paper,
  borderRadius: theme.shape.borderRadius,
  boxShadow: theme.shadows[4],
  '& > div': {
    padding: theme.spacing(1),
    cursor: 'pointer',
    '&:hover': {
      backgroundColor: alpha(theme.palette.action.hover, 0.5),
    },
  },
}));

const SearchBar = () => {
  const navigate = useNavigate();
  const [result, setResult] = useState([]);
  const [searchCat, setSearchCat] = useState("");
  const imgPath = `../assets/img/`;
  const handleNavigation = (e) => {
    navigate(`item/${e}`);
    document.querySelector('input[type="text"]').value = '';
    setResult(undefined);
  }

  const handleChange = (e) => {
    setSearchCat(e.target.value);
  }

  const debouncedSearchHandler = useMemo(() => {
    console.log(searchCat)
    return debounce((value) =>
        fetch("https://localhost:7211/search?query=" + value )
            .then(res => res.json())
            .then(res => setResult(res.$values))
        , 500);
  }, []);
  console.log(result)

  return (
    <Search>
      <SearchIconWrapper>
        <SearchIcon />
      </SearchIconWrapper>
      <StyledInputBase
        placeholder='Search...'
        inputProps={{ 'aria-label': 'search', maxLength: 50 }}
        onChange={(e) => debouncedSearchHandler(e.target.value)}
      />
      {result !== undefined && (
          <SearchResults>
            {result.map((result) => (
                <Card className='animate__animated animate__fadeIn' raised style={{backgroundColor: "rgb(203, 177, 124)"}}>
                  <CardActions sx={{ display: 'flex', justifyContent: 'space-around' }}>
                    <CardMedia
                        component='img'
                        sx={{width: 70}}
                        image={imgPath + result.id + '.jpg'}
                        alt="alt"
                        onClick={e => handleNavigation(result.id)}
                    />
                    <Button size='large' sx={{color: "white", "&:hover": {backgroundColor: "rgb(203,203,124)"}}}
                            onClick={e => handleNavigation(result.id)}
                    >
                      <Typography variant='body2' color='text.secondary' noWrap>
                        {result.jewelleryType} {result.id}
                      </Typography>
                    </Button>
                  </CardActions>
                </Card>
            ))}
          </SearchResults>
      )}
      <SearchSelectWrapper>
        <FormControl size="small">
        <Select
            labelStyle={{color: 'white'}}
            sx={{
              color: "white",
              '.MuiOutlinedInput-notchedOutline': {
                border: 0
              },
              '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                border: 0
              },
              '&:hover .MuiOutlinedInput-notchedOutline': {
                border: 0
              },
              '.MuiSvgIcon-root ': {
                fill: "white !important",
              }
            }}
            labelId="categories-select"
            id="categories-select"
            value={searchCat}
            label="Search Category"
            onChange={handleChange}
        >
          <MenuItem value={"Item"}>Item name</MenuItem>
          <MenuItem value={"Brand"}>Brand</MenuItem>
          <MenuItem value={"Cat"}>Category</MenuItem>
          <MenuItem value={"Certify"}>Certification</MenuItem>
          <MenuItem value={"Gold"}>Gold Karat</MenuItem>
        </Select>
        </FormControl>
      </SearchSelectWrapper>
    </Search>
  );
};

export default SearchBar;
