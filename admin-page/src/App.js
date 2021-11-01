import './App.css';
import axios from 'axios';
import React, { useState, useEffect } from 'react';

function App() {
    const [product, setProduct] = useState(null);

    useEffect(() => {
        axios.get('https://localhost:5001/api/products').then((response) => {
            console.log(response.data.items);
            setProduct(response.data.items);
        });
    }, []);

    if (!product) return null;

    return (
        <div className="App">
            <h1>WELCOME!!!</h1>
            <p>{product.map((p) => p.name)}</p>
            <p>{product.map((p) => p.price)}</p>
        </div>
    );
}

export default App;
