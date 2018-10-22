<template>
  <div class="content">
   <h1>{{product.shortName}}</h1>
  </div>
</template>

<style>

</style>

<script>
import router from '../router';
export default {
  name: "ProductsSection",
  data: function() {
    return {      
    };
  },
  computed: {
    id: function(){
      return this.$store.state.product;
    },
    product: function(){
      return this.$store.state.products.find(f=>f.id==this.$store.state.product);
    },
    name: function() {
      return this.$store.state.name;
    }
  },
  methods: {

  },
  components: {}
};
</script>
