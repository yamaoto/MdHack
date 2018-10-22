/* eslint-disable */
import Vue from 'vue';
import Router from 'vue-router';

import Welcome from '@/components/Welcome';
import Main from '@/components/Main';
import Product from '@/components/Product';
import Login from '@/components/Login';

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: 'Login',
            component: Login
        },
        {
            path: '/Main',
            name: 'Main',
            component: Main
        },
        {
            path: '/Login',
            name: 'Login',
            component: Login
        }
    ]
});
