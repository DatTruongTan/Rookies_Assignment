import React, { useState, useEffect } from 'react';
// import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import AuthService from '../../Services/auth-service';

import { USER_PROFILE_STORAGE_KEY } from '../../Constants/oidc-config';
import { Link } from 'react-router-dom';

const NavBar = () => {
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
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <div class="container">
                    <a class="navbar-brand" href="/">
                        Admin Page
                    </a>
                    <button
                        class="navbar-toggler"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#navbarNav"
                        aria-controls="navbarNav"
                        aria-expanded="false"
                        aria-label="Toggle navigation"
                    >
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div
                        class="d-flex justify-content-between align-items-center collapse navbar-collapse"
                        id="navbarNav"
                    >
                        <div class="navbar-nav">
                            <a
                                class="nav-link active"
                                aria-current="page"
                                href="/user"
                            >
                                Users
                            </a>
                        </div>
                        <ul class="d-flex align-items-center navbar-nav">
                            <li class="nav-item me-3">{userName}</li>
                            <li class="nav-item">
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
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    );
};

export default NavBar;
