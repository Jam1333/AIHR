import React, { FormEvent, FocusEvent, ChangeEvent, useEffect, useState } from 'react';
import {NavLink} from 'react-router-dom';

import FormControl from './common/FormControl/FormControl';

import style from './Login.module.css';

const Login = () => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [rememberMe, setRememberMe] = useState(false);
    const [loginDirty, setLoginDirty] = useState(false);
    const [passwordDirty, setPasswordDirty] = useState(false);
    const [loginError, setLoginError] = useState('Login cannot be empty');
    const [passwordError, setPasswordError] = useState('Password cannot be empty');
    const [formValid, setFormValid] = useState(false);
    
    useEffect(() => {
        if (loginError || passwordError) {
            setFormValid(false);
        } else {
            setFormValid(true);
        }
    }, [loginError, passwordError]);
    
    const loginHandler = (e: ChangeEvent<HTMLInputElement>) => {
        setLogin(e.target.value);
        
        const validation = /^[a-zA-Z0-9_-]{5,15}$/;

        if (!validation.test(String(e.target.value))) {
            if (e.target.value.length < 5 || e.target.value.length > 15) {
                setLoginError('The login must be between 5 and 15 characters long.');
            } else {
                setLoginError('Invalid login format');
            }
        } else {
            setLoginError('');
        }
    };

    const passwordHandler = (e: ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
        
        const validation = /^[a-zA-Z0-9_-]{8,15}$/;

        if (!validation.test(String(e.target.value))) {
            if (e.target.value.length < 8 || e.target.value.length > 15) {
                setPasswordError('The password must be between 8 and 15 characters long.');
            } else {
                setPasswordError('Invalid password format');
            }
        } else {
            setPasswordError('');
        }
    };

    const rememberMeHandler = (e: ChangeEvent<HTMLInputElement>) => {
        const newRememberMe = !rememberMe;
        setRememberMe(newRememberMe);
    };

    const blurHandler = (e: FocusEvent<HTMLInputElement, Element>) => {
        switch (e.target.name) {
            case 'login':
                setLoginDirty(true);
                break;
            case 'password':
                setPasswordDirty(true);
                break;
        }
    };

    const submitHandler = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        if (formValid) {
            //auth proccess
        }
    };
    
    return (
        <div className={style.loginWindow}>
            <div className={style.loginBlock}>
                <h2 className={style.h2}>Log in</h2>

                <form className={style.loginForm} onSubmit={e => submitHandler(e)}>
                    <input onChange={e => loginHandler(e)} value={login} onBlur={e => blurHandler(e)} name='login' placeholder='Login' />
                    {(loginDirty && loginError) && <FormControl errorText={loginError} />}

                    <input onChange={e => passwordHandler(e)} value={password} onBlur={e => blurHandler(e)} name='password' type='password' placeholder='Password'></input>
                    {(passwordDirty && passwordError) && <FormControl errorText={passwordError} />}

                    <div className={style.checkboxContainer}>
                        <input onChange={e => rememberMeHandler(e)} checked={rememberMe} name='rememberMe' className={style.checkbox} type='checkbox' /> Remember me
                    </div>

                    <button className={style.button}>Confirm</button>
                    {<FormControl errorText={'Invalid login or password'} />}
                </form>

                No account? <NavLink to='/registration'>Register</NavLink>
            </div>
        </div>
    )
}

export default Login;