import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginUser from '../views/LoginUser.vue'
import InfoUser from '../components/InfoUser.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: LoginUser
    },
    {
      path: '/info-user',
      name: 'InfoUser',
      component: InfoUser
    }
  ]
})

export default router
