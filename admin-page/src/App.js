import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import {
    CREATE_PRODUCT,
    EDIT_PRODUCT,
    USER,
    AUTH,
    HOME,
    CATEGORY,
} from './Constants/pages';

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
                        path={EDIT_PRODUCT}
                        component={UpdateProduct}
                    />
                </Switch>
            </BrowserRouter>
        </div>
    );
}

export default App;
