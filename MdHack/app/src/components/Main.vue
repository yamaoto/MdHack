<template>
<section class="products-section">
  <div class="top-menu">
    <a class="logo">
      <img alt="logo"/>
    </a>
    <a  @click="clearCategory">
      <img v-if="show==='main'" alt="ГЛАВНАЯ" src="/pic3.png" srcset="pic3@2x.png 2x" />
      <img v-if="show!=='main'" alt="ГЛАВНАЯ" src="/pic33.png" srcset="pic33@2x.png 2x" />
      <span>Главная</span>
    </a>

    <a @click="openCategory(-1)">
      <img v-if="show==='inner' || show=='product'"  alt="УСЛУГИ" src="/pic4.png" srcset="pic4@2x.png 2x" />
      <img v-if="show!=='inner' && show!='product'" src="/pic34.png" srcset="pic34@2x.png 2x" />
      <span>Услуги</span>
    </a>
    <a>
      <img alt="СТАТУС" src="/pic35.png" srcset="pic35@2x.png 2x" />
      <span>Статус заявок</span>
    </a>

    <a style="cursor: not-allowed">
      <img alt="ЧАТ" src="/pic36.png" srcset="pic36@2x.png 2x" />
      <span>Чат</span>
    </a>

    <a @click="openSearch">
      <img v-if="show==='search'" alt="ПОИСК" src="/pic7.png" srcset="pic7@2x.png 2x" />
      <img v-if="show!=='main'" alt="ПОИСК" src="/pic37.png" srcset="pic37@2x.png 2x" />
      <span>Поиск</span>
    </a>

    <a class="user-profile">
      <img alt="БОЛЬЗОВАТЕЛЬ" v-auth-image="'https://mdcasehackapp.azurewebsites.net/api/account/avatar'" />
      <span>{{name}}</span>
    </a>
  </div>  
  <ProductsList v-if="show=='inner'"></ProductsList>
  <ProductsMain v-if="show=='main'"></ProductsMain>
  <ProductsSearch v-if="show=='search'"></ProductsSearch>
  
  <ProductMvd v-if="show=='product' && product==1"></ProductMvd>
  <Product v-if="show=='product' && product != 1"></Product>
</section>
</template>

<style>
.content {
  overflow: scroll !important;
}
.logo img {
  height: 75px;
  margin-top: -13px;
}
.form-container {
  display: flex;
  vertical-align: top;
}
.form-container .form {
  flex: auto;
  width: 400px !important;
  margin-right: 50px;
  vertical-align: top;
}
.form, .form-item {
  font-size: 20px;
}
.form input, label {
  font-size: 18px;
  
}
.content.bottom {
  padding-bottom: 115px;
  margin-bottom: 115px;
}
.description {
  margin-top: -46px;
  height: 65px;
}

.form > label {
  font-size:20px;
  margin-top: 40px;
  margin-bottom: 10px;
  display: block;
}
.form:first-child > label {
}
.form > label span {
  font-size: 22px !important;
  color: #5574f7;
  font-weight: 600;
}
.form > label span:after {
content: ' ';
}

.description .left {
  float: left;
  width: 60%;
  color: #5574F7;
  background-color: #fff;
  box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.1);
  font-size:48px;
  font-weight: 900 !important;
  height: 65px;
  text-align: center;
}
.description .right {
  float: right;
  width: 40%;
  background: linear-gradient(45deg,  #5574F7 0%, #60C3FF 100%);
  color: #fff;
  font-size: 34px;
  height: 65px;
  text-align: center;
  vertical-align: middle
}
.form .form-item {
  padding-bottom: 30px;
}
.form .form-item label {
  display: block;
}
.form .form-item label:after{
  content: ':'
}
.bottom-btn {
  position: fixed;
  left: 0;
  bottom: 0;
  width: 100%;
  height: 109px;
  text-align: center;
  vertical-align: middle;
  color: #fff;
  background: linear-gradient(45deg,  #5574F7 0%, #60C3FF 100%);
  background-repeat: no-repeat;
  cursor: pointer;
}
.bottom-btn > * {
display: inline;

}
.bottom-btn img {
  margin-right: 32px;
  margin-top:27px;
  text-align: center;
  vertical-align: middle;
}
.bottom-btn a {
  display: inline-block;
  font-size: 32px;
  text-align: center;
  margin-top:27px;
  vertical-align: middle;
  cursor: pointer;
}
h1,h2 {
  margin: 0 !important;
}
.content {
  width: 100%;
  min-height: 100%-150px - 50px;
}
.products-section {
  width: 100%;
  height: 100%;
}
.products-section .user-profile img {
  width: 75px;
  height: 75px;
  border-radius: 37.5px;
}
.products-section .back {
  position: fixed;
  bottom: 150px;
  right: 224px;
  font-size: 31px;
  vertical-align: middle;
  color: #bcc5d3;
  cursor: pointer;
}
.products-section .back img {
  margin-right: 18px;
  vertical-align: middle;
  color: #bcc5d3;
}
.products-section {
  background: none !important;
  color: #4c5264;
}
.products-section .content {
  padding-left: 224px;
  padding-right: 224px;
  overflow-wrap: break-word;
  overflow: hidden;
}
.products-section h1 {
  font-size: 48px;
  color: #5574f7;
  text-align: left;
}
.products-section .top-menu {
  padding-left: 77px;
  padding-right: 77px;
  display: flex;
  -webkit-flex-direction: row;
  flex-direction: row;
  box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.1);
  margin-bottom: 50px;
}
.products-section .top-menu a.active {
  color: #5574f7;
}
.top-menu {
  padding: 0 78px;
  vertical-align: middle !important;
}
.top-menu a, .top-menu a span {
  height: 75px;
}
.top-menu a {
  display: block;
  margin: 54px 0;
  flex: auto;
  text-align: center;
  color: #bcc5d3;
  cursor: pointer;
  vertical-align: middle !important;
}
.top-menu a > * {
  vertical-align: middle !important;
}
.top-menu a img {
  object-fit: cover;
}
.top-menu a span {
  margin-left: 20px;
  font-size: 31px;
  vertical-align: middle !important;
}
.products-section .ui-list {
  list-style: none;
  margin: 0;
  font-size: 39px;
  display: flex;
  flex-direction: column;
}
.products-section .ui-list img {
  margin-right: 72px;
  vertical-align: middle;
}
.products-section .ui-list > li a {
  cursor: pointer;
  vertical-align: middle;
  font-weight: 600;
}
.products-section .ui-list > li {
  padding-top: 22px;
  padding-bottom: 22px;
  min-width: 50%;
  flex: auto;
  vertical-align: middle;
}
.products-section .ui-list.left {
  float: left;
  width: 45%;
}
.products-section .ui-list.right {
  float: right;
  width: 45%;
}
@media only screen and (max-width: 1200px) {
.content {
  padding-left: 20px !important;
  padding-right: 20px !important;
}
.content .left,
.content .right {
  float: none !important;
  width: 80% !important;;
}
.top-menu {
  padding:0 !important;
  flex-direction: row;
}

.top-menu a > img, 
.top-menu a > span{
display: block !important;
font-size: 18px !important;
margin: 0 !important;
text-align: left;
}
}
</style>

<script>
import ProductsList from './ProductsList';
import ProductsMain from './ProductsMain';
import ProductsSearch from './ProductsSearch';
import ProductMvd from './forms/ProductMvd';
import Product from './Product';

export default {
  name: "ProductsSection",
  created: function() {
    // this.$bus.on('chooseRevision', this.chooseRevision);
  },
  mounted: function() {
    // this.$store.dispatch("updateSearchStatus", 'Unchecked');
  },
  beforeDestroy: function() {
    // this.$bus.off('chooseRevision', this.chooseRevision);
  },
  data: function() {
    return {
    };
  },
  computed: {
    name: function() {
      return this.$store.state.name;
    },
    noProducts: function() {
      const cat = this.$store.state.category;
      return cat == null && !this.$store.state.products.some(s => s.category == cat);
    },
    show: function() {
      return this.$store.state.show;
    },
    category: function() {
      return this.$store.state.category;
    },
    products: function(){
      return this.$store.state.products;
    },
    product: function(){
      return this.$store.state.product;
    },
  },
  methods: {
    openSearch: function() {
      this.$store.dispatch("openSearch");
    },
    openCategory: function(id) {
      this.$store.dispatch("openCategory", id);
    },
    open: function(id) {
      this.$store.dispatch("open", id);
    },
    clearCategory: function() {
      this.$store.dispatch("clearCategory");
    }
  },
  components: {
    Product,ProductsList,ProductsMain,ProductsSearch,ProductMvd
  }
};
</script>
