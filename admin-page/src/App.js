import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import AuthService from './Services/auth-service';

import { USER_PROFILE_STORAGE_KEY } from './Constants/oidc-config';

import {
    CREATE_PRODUCT,
    EDIT_PRODUCT,
    EDIT_PRODUCT_ID,
    AUTH,
    HOME,
} from './Constants/pages';

import CreateProduct from './Components/Create';
import ListProduct from './Components/List';
import UpdateProduct from './Components/Update';
import Auth from './Components/Auth/Auth';
// const CreateProduct = lazy(() => import('.Components/Create'));
// const ListProduct = lazy(() => import('.Components/List'));
// const UpdateProduct = lazy(() => import('./Update'));

function App() {
    const [userName, setUserName] = useState(undefined);

    const handleLogin = (e) => {
        AuthService.loginAsync();
    };

    const handleLogout = (e) => {
        AuthService.logoutAsync();
    };

    useEffect(() => {
        let user = JSON.parse(localStorage.getItem(USER_PROFILE_STORAGE_KEY));
        console.log('Thong tin user:', user);
        if (user !== undefined) {
            setUserName(user?.name);
        }
    }, []);

    return (
        <div className="">
            <h1>WELCOME!!!</h1>
            <div>
                <p> {userName} </p>
                {userName === undefined ? (
                    <button
                        className="btn btn-danger"
                        type="button"
                        onClick={(e) => handleLogin()}
                    >
                        Login
                    </button>
                ) : (
                    <button
                        className="btn btn-danger"
                        type="button"
                        onClick={(e) => handleLogout()}
                    >
                        Logout
                    </button>
                )}
            </div>

            <BrowserRouter>
                <Switch>
                    <Route exact path={HOME}>
                        <ListProduct />
                    </Route>
                    <Route path={AUTH}>
                        <Auth />
                    </Route>
                    <Route exact path={CREATE_PRODUCT}>
                        <CreateProduct />
                    </Route>
                    <Route exact path={EDIT_PRODUCT}>
                        <UpdateProduct />
                    </Route>
                    {/* <Route exact path={DELETE_PRODUCT_ID}>
                        <DeleteProduct id={ListProduct.id} />
                    </Route> */}
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;
