import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import {
    CREATE_PRODUCT,
    EDIT_PRODUCT,
    USER,
    AUTH,
    HOME,
    CATEGORY,
    CREATE_CATEGORY,
    EDIT_CATEGORY,
} from './Constants/pages';

import ListCategory from './Components/Category/index';
import CreateCategory from './Components/Category/Create';
import UpdateCategory from './Components/Category/Update';
import CreateProduct from './Components/Create';
import ListProduct from './Components/List';
import ListUser from './Components/Customer';
import UpdateProduct from './Components/Update';
import Auth from './Components/Auth/Auth';
import NavBar from './Components/Navbar';

function App() {
    return (
        <div>
            <NavBar />
            <h1 className="App">WELCOME!!!</h1>
            <BrowserRouter>
                <Switch>
                    <Route exact path={HOME} component={ListProduct} />
                    <Route exact path={USER} component={ListUser} />
                    <Route exact path={CATEGORY} component={ListCategory} />
                    <Route path={AUTH} component={Auth} />
                    <Route
                        exact
                        path={CREATE_PRODUCT}
                        component={CreateProduct}
                    />
                    <Route
                        exact
                        path={CREATE_CATEGORY}
                        component={CreateCategory}
                    />
                    <Route
                        exact
                        path={EDIT_PRODUCT}
                        component={UpdateProduct}
                    />
                    <Route
                        exact
                        path={EDIT_CATEGORY}
                        component={UpdateCategory}
                    />
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;
