import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import React from 'react';
import { CREATE_BRAND, HOME } from './Constants/pages';

import CreateProduct from './Components/Create';
import ListProduct from './Components/List';
// const CreateProduct = lazy(() => import('.Components/Create'));
// const ListProduct = lazy(() => import('.Components/List'));
// const UpdateProduct = lazy(() => import('./Update'));

function App() {
    return (
        <div className="">
            <h1>WELCOME!!!</h1>
            <BrowserRouter>
                <Switch>
                    <Route exact path={HOME}>
                        <ListProduct />
                    </Route>
                    <Route exact path={CREATE_BRAND}>
                        <CreateProduct />
                    </Route>
                    {/* <Route exact path={EDIT_BRAND}>
                    <UpdateBrand />
                </Route>  */}
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;
