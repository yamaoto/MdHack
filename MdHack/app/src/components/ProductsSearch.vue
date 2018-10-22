<template>
<div>
  <div class="content" v-if="show=='search'">
    <h1 class="warning">Поиск пока не работает...</h1>
    <a class="back" @click="clearCategory">
      <img alt="Пиктограмма" src="/pic12.png" srcset="pic12@2x.png 2x" />
          <span>Назад</span>
    </a>
  </div>
  <div class="content" v-if="show=='main'">
    <h1>ЖИЗНЕННЫЕ СИТУАЦИИ</H1>
    <ul class="ui-list left">
      <li>
        <a @click="openCategory(1)">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Личные документы</span>
        </a>
      </li>
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Выездное обслуживание</span>
        </a>
      </li>            
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Льготы и выплаты</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Моя семья</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Оформление недвижимости</span>
        </a>
      </li>            
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Охота и рыбалка</span>
        </a>
      </li>
    </ul>
    <ul class="ui-list right">
      
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Паспорт</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Работа и занятость</span>
        </a>
      </li>
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Свой бизнес</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Смена места жительства</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory(2)">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Справки, сведения, запросы</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" />
          <span>Юридическая помощь</span>
        </a>
      </li>
    </ul>
  </div>
  <div class="content" v-if="show=='inner'">
    <h1 v-if="noProducts">Увы, пока в данной категории нет услуг</h1>
  <ul class="ui-list left">
      
        <li v-for="item in products" v-bind:key="item.id" v-if="item.category==category || category == -1">
<a @click="open(item.id)">
        <span>{{item.shortName}}</span>
        </a>
      </li>      
      
    </ul>
    <a class="back" @click="clearCategory">
      <img alt="Пиктограмма" src="/pic12.png" srcset="pic12@2x.png 2x" />
          <span>Назад</span>
    </a>
  </div>
</div>
</template>

<style>
</style>

<script>
export default {
  name: "ProductsSection",
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
  components: {}
};
</script>
