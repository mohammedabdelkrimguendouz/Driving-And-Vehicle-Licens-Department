import { Route, Routes } from 'react-router-dom';
import Login from './Login';

export default function App() {
  return (
    <div className='App'>
       <Routes>
            <Route path="/Login" element={<Login />} />
        </Routes>
    </div>
       
  );
}
