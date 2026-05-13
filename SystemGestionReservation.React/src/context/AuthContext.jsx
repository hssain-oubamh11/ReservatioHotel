import React, { createContext, useContext, useState } from 'react';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const token = localStorage.getItem('jwtToken');
        const role = localStorage.getItem('userRole');
        const login = localStorage.getItem('userLogin');
        return token ? { token, role, login } : null;
    });

    const loginUser = (authResult) => {
        localStorage.setItem('jwtToken', authResult.token);
        localStorage.setItem('userRole', authResult.role);
        localStorage.setItem('userLogin', authResult.login);
        setUser({
            token: authResult.token,
            role: authResult.role,
            login: authResult.login
        });
    };

    const logoutUser = () => {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userLogin');
        setUser(null);
    };

    const isAdmin = () => user?.role === 'Administrateur';
    const isReceptionniste = () =>
        user?.role === 'Receptionniste' || user?.role === 'Administrateur';

    return (
        <AuthContext.Provider value={{
            user, loginUser, logoutUser, isAdmin, isReceptionniste
        }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);