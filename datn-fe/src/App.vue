<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import LoginUser from '../src/views/LoginUser.vue'
import InfoUser from './components/InfoUser.vue'

const token = ref(null)
const router = useRouter()

onMounted(() => {
  const savedToken = localStorage.getItem('token')
  if (savedToken) {
    token.value = savedToken
  }
})
const updateToken = (newToken) => {
  token.value = newToken
}
const logout = () => {
  localStorage.removeItem('token')
  token.value = null
  router.push({ name: 'login' })
}
</script>

<template>
  <div class="section-main">
    <LoginUser v-if="!token" @login-success="updateToken" />
    <div v-else>
      <InfoUser />
      <button class="btn btn-logout" @click="logout">Logout</button>
    </div>
  </div>
</template>

<style scoped>
.btn-logout {
  margin-top: 20px;
  padding: 10px 20px;
  background-color: red;
  color: white;
  border: none;
  cursor: pointer;
  border-radius: 5px;
}
header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}
nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
<style>
.form-1[readonly] {
  background-color: #e9ecef;
  opacity: 1;
}
.btn {
  align-items: center;
  border-radius: 10px;
  color: #fff;
  display: inline-block;
  line-height: 1.2;
  transition: all 0.2s;
  font-size: 14px;
  padding: 15px 20px 14px;
  background-color: #2997dd;
  border-color: #2997dd;
}
.btn:hover,
.btn-action:hover {
  background-color: #38dbf8;
  border-color: #38dbf8;
}
.form-control {
  display: block;
  width: 100%;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #212529;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid #ced4da;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  border-radius: 0.25rem;
  transition:
    border-color 0.15s ease-in-out,
    box-shadow 0.15s ease-in-out;
}
.btn-action {
  width: 100%;
  max-width: 250px;
  text-align: center;
  border: 1px solid #2997dd;
  color: #2997dd;
  padding: 5px 0;
  display: inline-block;
  border-radius: 10px;
  font-size: 12px;
}
</style>
