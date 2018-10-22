<template>
<div>
  <div class="description">
     <span class="left">ЗАПРОС ДОКУМЕНТА</span>
     <span class="right">{{product.shortName}}</span>
     <label style="float: right;" class="name">{{product.name}}</label>
  </div>
  <div class="content bottom" style="clear: both">   
   <div class="form-container">
   <div class="form">
    <label><span>1</span>Сведения о заявителе</label>
    <div class="form-item">
      <label>Пол</label>
      <sui-dropdown
    placeholder="Пол"
    selection
    :options="options"
    v-model="current"    
  />
    </div>
    <div class="form-item">
      <label>ФИО</label>
      <sui-input placeholder="ФИО" v-bind:value="name" />
    </div>
    <div class="form-item">
      <label>СНИЛС</label>
      <sui-input placeholder="СНИЛС" />
    </div>
    <div class="form-item">
      <label>Адрес</label>
      <sui-input placeholder="Адрес" />
    </div>
  </div>

<div class="form">
    <label><span>2</span>Контактные сведения</label>
    <div class="form-item">
      <label>Email</label>
      <sui-input placeholder="Email" />
    </div>
    <div class="form-item">
      <label>Телефон</label>
      <sui-input placeholder="Телефон" />
    </div>
    <div class="form-item">
      <label>Домашний телефон</label>
      <sui-input placeholder="Домашний телефон" />
    </div>
    </div>

    
<div class="form">
    <label><span>3</span>Сведения о документе, удостоверяющем личность</label>
    <div class="form-item">
      <label>Серия</label>
      <sui-input placeholder="Серия" />
    </div>
    <div class="form-item">
      <label>Номер</label>
      <sui-input placeholder="Номер" />
    </div>
    <div class="form-item">
      <label>Дата выдачи</label>
      <sui-input placeholder="Дата выдачи" />
    </div>
    </div>

    <div class="form">
    <label><span>4</span>Адрес заявителя</label>
    <div class="form-item">
      <label>Адрес</label>
      <sui-input placeholder="Начните вводить адрес" />
    </div>
    </div>
    </div>

  <a class="back" @click="clearCategory">
      <img alt="Пиктограмма" src="/pic12.png" srcset="pic12@2x.png 2x" />
          <span>Назад</span>
    </a>
    <div class="bottom-btn">
      <img src="/img18.png" srcset="pic18@2x.png 2x"/>
      <a>СДЕЛАТЬ ЗАПРОС</a>
    </div>
  </div>  </div>
</template>

<style>
</style>

<script>
export default {
  name: "ProductsSection",
  data: function() {
    return {
      current: null,
      options: [
        {
          text: "Мужской",
          value: 1
        },
        {
          text: "Женский",
          value: 2
        }
      ]
    };
  },
  computed: {
    id: function() {
      return this.$store.state.product;
    },
    product: function() {
      return this.$store.state.products.find(
        f => f.id == this.$store.state.product
      );
    },
    name: function() {
      return this.$store.state.name;
    }
  },
  methods: {
    clearCategory: function() {
      this.$store.dispatch("clearCategory");
    }
  },
  components: {}
};
</script>
