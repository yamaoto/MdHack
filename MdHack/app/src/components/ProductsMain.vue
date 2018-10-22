<template>
  <div class="content">
    <h1>ЖИЗНЕННЫЕ СИТУАЦИИ</H1>
    <ul class="ui-list left">
      <li>
        <a @click="openCategory(1)">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Личные документы</span>
        </a>
      </li>
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Выездное обслуживание</span>
        </a>
      </li>            
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Льготы и выплаты</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Моя семья</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Оформление недвижимости</span>
        </a>
      </li>            
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Охота и рыбалка</span>
        </a>
      </li>
    </ul><ul class="ui-list right">
      
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Паспорт</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Работа и занятость</span>
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
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Смена места жительства</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory(2)">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Справки, сведения, запросы</span>
        </a>
      </li>
      
      <li>
        <a @click="openCategory">
          <img alt="Пиктограмма" src="/pic51.png" srcset="pic51@2x.png 2x" /><span>Юридическая помощь</span>
        </a>
      </li>
    </ul>
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
    category: function() {
      return this.$store.state.category;
    },
    products: function(){
      return this.$store.state.products;
    },
  },
  methods: {
    openCategory: function(id) {
      this.$store.dispatch("openCategory", id);
    }
  },
  components: {}
};
</script>
