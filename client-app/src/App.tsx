import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { EditMerchandise } from './EditMerchandise';
import { MerchandiseView } from './Merchandise';
import { SignIn } from './SignIn';

export default function App() {
  return (    
  <div className="App">
    <Routes>
      <Route path="/" element={<SignIn />} />
      <Route path="merchandise" element={<MerchandiseView />} />
      <Route path="merchandise/:id" element={<EditMerchandise />} />
    </Routes>
  </div>
);
}
