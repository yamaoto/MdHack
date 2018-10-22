<template>
<section class="login login-section">
  <div class="login-wrap">
    <h1 style="font-size: 68px">МОИ ДОКУМЕНТЫ</h1>
    <a class="face-btn login-btn" @click="esia">
      <img src="/pic1.png" srcset="/pic1@2x.png 2x" alt="img" />
      <span>ВОЙТИ ЧЕРЕЗ ЕСИА </span>
    </a>
    <a class="face-btn login-btn" @click="face">
      <img src="/pic2.png" srcset="pic2@2x.png 2x" alt="img" />
      <span>ВОЙТИ ЧЕРЕЗ БИОМЕТРИЮ</span>
    </a>
    <a class="test-btn login-btn" @click="test">      
    </a>
  </div>

<div class="loading" v-if="loginStatus == 'loading'">
  <h1>Loading...</h1>
</div>
<div class="status" v-if="loginStatus == 'ok'">
  <h1>OK</h1>
</div>
<div class="status" v-if="testState !== 'none'">
  <h1>Воспользуйтесь теперь входом</h1>
</div>

  <canvas style="visibility: hidden" id="canvas">
  </canvas>

  <div v-show="togleFaceForm" class="face-form">
     <div class="camera">
    <video @click="takePhoto" id="video">Video stream not available.</video>
    <a v-if="!togleTest" class="take-btn" @click="takePhoto">Take photo</a>
    <a v-if="togleTest && testRemain!=0" class="take-btn" @click="takePhoto">Test ({{testRemain}})</a>
    <a v-if="togleTest && testRemain==0" class="take-btn" @click="takePhoto">GO Test!!</a>
  </div>  
  </div>
</section>
</template>

<style>
.test-btn {
  background-color: #03ffe6 !important;
  background-image: url(http://pixelartmaker.com/art/ddf05a1bc021490.png);
  background-position: center center;
  background-repeat: no-repeat;
  background-size: 100px auto;
}
.login-section {
  width: 100%;
  height: 100%;
  color: #fff;
  background: linear-gradient(45deg, #5574f7 0%, #60c3ff 100%);
  background-repeat: no-repeat;
}
.login-section h1,
.login-section h2,
.login-section h3 {
  text-align: center;
}
.login-section video {
  cursor: pointer;
}
.login-section .take-btn {
  position: absolute;
  bottom: 50px;
  display: block;
  font-size: 52pt;
  color: #fff;
  background-color: #5887f9;
  padding: 20px 35px;
  margin: 0 auto;
  width: 300px;
  left: 50%;
  margin-left: -150px;
  cursor: pointer;
}
.login-section .login-wrap {
  padding-top: 150px;
  text-align: center;
}
.login-section .login-btn,
.login-section .login-wrap {
  width: 80%;
  margin: 0 auto;
}
.login-section .login-btn {
  font-size: 29px;
  vertical-align: middle;
  padding-top: 37px;
  padding-bottom: 37px;
  display: block;
  width: 600px;
  margin-bottom: 50px;
  cursor: pointer;
  color: #5887f9;
  border-radius: 6px;
  text-align: center;
  background-color: #fff;
}

.login-section .login-btn img {
  width: 50px;
  margin-right: 20px;
  vertical-align: middle;
  display: display-block;
}
.login-section .face-form {
  position: fixed;
  top: 0;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}
.login-section .face-form video {
  width: 100%;
  height: 100%;
}
</style>

<script>
export default {
  name: "LoginSection",
  created: function() {},
  mounted: function() {},
  beforeDestroy: function() {},
  data: function() {
    return {
      togleFaceForm: false,
      togleTest: false,
      testCount: 0,
      testImages: []
    };
  },
  computed: {
    testState: function() {
      return this.$store.state.testState;
    },
    loginStatus: function() {
      return this.$store.state.loginStatus;
    },
    testRemain: function() {
      return 3 - this.testCount;
    }
  },
  methods: {
    esia: function() {
      alert("netu takogo");
    },
    takePhoto: function() {
      if (this.togleTest && this.testCount == 3) {
        this.togleTest = false;
        this.togleFaceForm = false;
        const name = prompt("name");
        const test = {
          name: name,
          passport: "12 34 567 890",
          datas: [...this.testImages]
        };
        this.$store.dispatch("testAuth", test);
        this.togleFaceForm = false;
        this.togleTest = false;
        this.testImages = [];
        this.testCount = 0;
        return;
      }
      const video = document.getElementById("video");
      const canvas = document.getElementById("canvas");
      var context = canvas.getContext("2d");
      canvas.width = 1280;
      canvas.height = 720;
      context.drawImage(video, 0, 0, 1280, 720);
      var data = canvas.toDataURL("image/png");

      if (this.togleTest) {
        this.testCount++;
        this.testImages.push(data);
      } else {
        this.togleFaceForm = false;
        this.$store.dispatch("faceAuth", data);
      }
    },
    test: function() {
      const video = document.getElementById("video");
      const canvas = document.getElementById("canvas");
      navigator.mediaDevices
        .getUserMedia({ video: true, audio: false })
        .then(stream => {
          video.srcObject = stream;
          video.play();
          this.togleFaceForm = true;
          this.togleTest = true;
        })
        .catch(function(err) {
          console.log("ERROR!!!! " + err);
        });
    },
    face: function() {
      const video = document.getElementById("video");
      const canvas = document.getElementById("canvas");
      navigator.mediaDevices
        .getUserMedia({ video: true, audio: false })
        .then(stream => {
          video.srcObject = stream;
          video.play();
          this.togleFaceForm = true;
        })
        .catch(function(err) {
          console.log("ERROR!!!! " + err);
        });
    }
  },
  components: {}
};
</script>
