/* eslint-disable */
import Vuex from 'vuex';
import axios from 'axios';
import router from '../router';

const baseUrl = 'http://localhost:50798/';

export default function getStore() {
    return new Vuex.Store({
        state: {
            show: 'main',
            testState: 'none',
            category: null,
            accessToken: null,
            loginStatus: null,
            name: localStorage.getItem('name'),
            passport: localStorage.getItem('passport'),
            products: [
                {
                    id: '1',
                    shortName: 'Справка об отсутствии судимости',
                    name: 'Получение справки о наличии (отсутствии) судимости и (или) факта уголовного преследования либо о прекращении уголовного преследования',
                    category: '1'
                },
                {
                    id: '2',
                    shortName: 'Заявление снилс',
                    category: '1'
                },
                {
                    id: '3',
                    shortName: 'Заявление ИНН',
                    category: '2'
                }
            ]
        },
        actions: {
            setLoginLoading: function (actions) {
                actions.commit('setLoginLoading');
            },
            testAuth: function (actions, obj) {
                const datas = obj.datas;
                actions.commit('setLoginLoading');
                const bodyFormData = new FormData();
                datas.forEach((data, index) => {
                    var blobBin = atob(data.split(',')[1]);
                    var array = [];
                    for (var i = 0; i < blobBin.length; i++) {
                        array.push(blobBin.charCodeAt(i));
                    }
                    var file = new Blob([new Uint8Array(array)], { type: 'image/png' });
                    switch (index) {
                        case 0: bodyFormData.set('Main', file);
                            break;
                        case 1: bodyFormData.set('One', file);
                            break;
                        case 2: bodyFormData.set('Two', file);
                            break;
                    }
                });
                bodyFormData.set('Name', obj.name);
                bodyFormData.set('Passport', obj.passport);
                axios({
                    method: 'post',
                    url: 'https://mdcasehackapp.azurewebsites.net/api/Face/face-add',
                    data: bodyFormData,
                    config: { headers: { 'Content-Type': 'multipart/form-data' } }
                }).then((response) => {
                    actions.commit('test', 'done');
                    actions.commit('setLoginLoadingStatus', 'ok');
                })
                    .catch((error) => {
                        console.log('error', error);
                        actions.commit('test', 'error');
                        actions.commit('setLoginLoadingStatus', 'error');
                    })
            },
            faceAuth: function (actions, data) {
                actions.commit('setLoginLoading');
                const bodyFormData = new FormData();
                var blobBin = atob(data.split(',')[1]);
                var array = [];
                for (var i = 0; i < blobBin.length; i++) {
                    array.push(blobBin.charCodeAt(i));
                }
                var file = new Blob([new Uint8Array(array)], { type: 'image/png' });
                bodyFormData.set('Face', file);
                axios({
                    method: 'post',
                    url: 'https://mdcasehackapp.azurewebsites.net/api/Face/face-auth',
                    data: bodyFormData,
                    config: { headers: { 'Content-Type': 'multipart/form-data' } }
                }).then((response) => {
                    if (response.data && response.data.access_token) {
                        actions.commit('setAccessToken', response.data);
                        localStorage.setItem('access_token', response.data.access_token)
                        localStorage.setItem('name', response.data.name)
                        localStorage.setItem('passport', response.data.passport)
                        var authHeader = 'Bearer ' + localStorage.getItem('access_token');
                        axios.defaults.headers.common['Authorization'] = authHeader;
                        router.push('/Main')
                    } else {
                        actions.commit('setLoginLoadingStatus', 'error');
                    }
                })
                    .catch((error) => {
                        console.log('error', error);
                        actions.commit('setLoginLoadingStatus', 'error');
                    })
            },
            openSearch: function (actions) {
                actions.commit('products', {
                    show: 'search'
                });
            },
            openCategory: function (actions, id) {
                actions.commit('products', {
                    show: 'inner',
                    category: id
                });
            },
            open: function (actions, id) {
                actions.commit('open', id);
            },
            clearCategory: function (actions) {
                actions.commit('products', {
                    show: 'main',
                    category: null
                });
            }
        },
        mutations: {
            test: function (state, done) {
                state.testState = done;
            },
            setLoginLoadingStatus:function (state, status) {
                state.loginStatus = status;
            },
            setLoginLoading: function (state, data) {
                state.loginStatus = 'loading';
            },
            setAccessToken: function (state, data) {
                state.accessToken = data.access_token;
                state.loginStatus = 'ok';
                state.name = data.name;
                state.pasport = data.pasport;
            },
            products: function (state, data) {
                state.show = data.show;
                if (data.category > 0 || data.category <= 0 || data.category == null) {
                    state.category = data.category;
                }
            },
            open: function (state, id) {
                state.show = 'product';
                state.product = id;
            }
        }
    });
}
