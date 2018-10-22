// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import VueAuthImage from 'vue-auth-image';
import SuiVue from 'semantic-ui-vue';
import VueBus from 'vue-bus';
import Vuex from 'vuex';
import axios from 'axios';
import router from './router';
import getStore from './store';

import App from './App';

Vue.config.productionTip = false;

Vue.use(VueAuthImage);
Vue.use(SuiVue);
Vue.use(VueBus);
Vue.use(Vuex);
Vue.use(require('vue-moment'));




const store = getStore();
/* eslint-disable no-new */
const app = new Vue({
    el: '#app',
    router: router,
    store: store,
    beforeMount: function(){
        const token = localStorage.getItem('access_token');
        if(token) {
            const authHeader = 'Bearer ' + token;
            axios.defaults.headers.common['Authorization'] = authHeader;
            console.log('load with auth');
        }
    },
    components: {
        App: App
    },
    template: '<App/>',
    created: function () {
    }
});