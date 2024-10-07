<template>
  <div class="section-register">
    <div class="register">
      <h1 class="title">Register</h1>
      <form @submit.prevent="registerUser">
        <div class="rowp">
          <label for="username">Username:</label>
          <input v-model="registerData.username" class="form-control" type="text" id="username" required />
        </div>
        <div class="rowp">
          <label for="email">Email:</label>
          <input v-model="registerData.email" class="form-control" type="email" id="email" required />
        </div>
        <div class="rowp">
          <label for="password">Password:</label>
          <input v-model="registerData.password" class="form-control" type="password" id="password" required />
        </div>
        <button class="btn" type="submit">Register</button>
      </form>
      <p v-if="message">{{ message }}</p>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'

export default {
  name: 'Register',
  setup() {
    const registerData = ref({
      username: '',
      email: '',
      password: ''
    })
    const message = ref('')

    const registerUser = async () => {
      try {
        const response = await fetch('https://localhost:7083/api/register', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(registerData.value)
        })

        if (response.ok) {
          message.value = 'Registration successful!'
        } else {
          const errorText = await response.text()
          message.value = errorText || 'Registration failed!'
        }
      } catch (error) {
        message.value = 'Error registering user!'
      }
    }

    return {
      registerData,
      registerUser,
      message
    }
  }
}
</script>

<style scoped>
.register {
  padding: 30px;
}
.section-register .rowp{
    padding-bottom: 20px;
}
.btn{
    min-width: 390px ;
}
.section-register {
  min-height: 40vh;
  min-width: 450px;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 10px 30px 0 rgb(0 0 0 / 85%);
}
.section-register .title {
  font-weight: 600;
  text-align: center;
  padding-bottom: 20px;
}
</style>
