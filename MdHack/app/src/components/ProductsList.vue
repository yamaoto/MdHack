<template>
  <div class="content">
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
    noProducts: function() {
      const cat = this.$store.state.category;
      return cat == null && !this.$store.state.products.some(s => s.category == cat);
    },
    category: function() {
      return this.$store.state.category;
    },
    products: function(){
      return this.$store.state.products;
    },
  },
  methods: {
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
