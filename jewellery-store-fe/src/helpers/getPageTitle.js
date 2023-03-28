import { useParams } from 'react-router-dom';

const getPageTitle = () => {
  const { categoryId } = useParams();

  switch (categoryId) {
    case 'necklaces':
      return 'Necklaces';
    case 'rings':
      return 'Rings';
    case 'earrings':
      return 'Earrings';
    default:
      return 'All items';
  }
};

export default getPageTitle;
