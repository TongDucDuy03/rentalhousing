<template>
    <div class="section-login">
      <div class="login">
        <h1 class="title">Login</h1>
        <form @submit.prevent="loginUser">
          <div class="rowp">
            <label for="email">Email:</label>
            <input v-model="loginData.email" class="form-control" type="email" id="email" required />
          </div>
          <div class="rowp">
            <label for="password">Password:</label>
            <input
              v-model="loginData.password"
              class="form-control"
              type="password"
              id="password"
              required
            />
          </div>
          
          <button class="btn" type="submit">Login</button>
        </form>
        <p v-if="message">{{ message }}</p>
      </div>
    </div>
  </template>
  
  <script>
  import { ref } from 'vue'
  import { useRouter } from 'vue-router'
  
  export default {
    name: 'Login',
    setup() {
      const loginData = ref({
        email: '',
        password: ''
      });
      const message = ref('');
      const router = useRouter(); 
  
      const loginUser = async () => {
        try {
          const response = await fetch('https://localhost:7083/api/login', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData.value)
          });
  
          if (response.ok) {
            const data = await response.json();
            message.value = data.message;
  
           
            localStorage.setItem('token', data.token);
  
          
            router.push({ name: 'InfoUser' });
          } else {
            const errorText = await response.text();
            message.value = errorText || 'Login failed!';
          }
        } catch (error) {
          message.value = 'Error logging in!';
        }
      };
  
      return {
        loginData,
        loginUser,
        message
      };
    }
  };
  </script>
  
  <style scoped>
  .login {
    padding: 30px;
  }
  .section-login .rowp {
    padding-bottom: 20px;
  }
  .btn {
    min-width: 390px;
  }
  .section-login {
    min-height: 40vh;
    min-width: 450px;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 10px 30px 0 rgb(0 0 0 / 85%);
  }
  .section-login .title {
    font-weight: 600;
    text-align: center;
    padding-bottom: 20px;
  }
  </style>
  