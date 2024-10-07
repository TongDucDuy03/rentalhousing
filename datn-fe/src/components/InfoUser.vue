<template>
  <!--all: end điều hướng -->
  <div class="section-profile">
    <div class="container">
      <div class="info">
        <div class="row justify-content-between">
          <!-- left menu -->
          <div class="col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3">
            <div class="menu-profile">
              <div class="title">
                {{ data.username }}
              </div>
              <div class="link-profile">
                <ul>
                  <li>
                    <a href="" title="Account Information" class="active"> Account Information </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
          <!-- right content -->
          <div class="col-lg-7 col-12">
            <div class="content-profile">
              <h1 class="title">Account Information</h1>
              <form class="form" action="" method="post" autocomplete="off" novalidate="novalidate">
                <div class="form-group">
                  <label> Full Name</label>
                  <input v-model="data.username" type="text" class="form-control" />
                </div>

                <div class="form-group">
                  <label> Email </label>
                  <input v-model="data.email" type="text" class="form-control" />
                </div>

                <div class="form-group">
                  <label> Phone </label>
                  <input v-model="data.phone_number" type="text" class="form-control" />
                </div>

                <div class="form-group">
                  <label> Role </label>
                  <input v-model="data.role" type="text" class="form-control" />
                </div>

                <!-- <div
                  class="form-group"
                  v-if="state.user_model.login_type != null && state.user_model.login_type == 1"
                >
                  <label> Change password </label>
                  <div class="w-pass">
                    <input
                      type="text"
                      class="form-control"
                      placeholder=" Change password "
                      disabled=""
                    />
                    <p @click="ChangePassClick">Change Password</p>
                  </div>
                </div> -->

                <button
                  type="button"
                  nh-btn-action="button"
                  class="btn"
                  @click="SaveChange"
                >
                  Save changes
                </button>

                <input v-model="data.user_id" type="hidden" />
              </form>

              <div class="info-profile-bottom">
                <div class="titles">Delete Account</div>
                <p>
                  You can delete your account permanently. *Note that this process can take a while
                  and can't be undone after completion.
                </p>

                <span class="btn-action" @click="DeleteAccount"> DELETE ACCOUNT </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
export default {
  setup() {
    const data = ref({
      username: '',
      email: '',
      phone_number: '',
      presenter_id: ''
    })
    const error = ref(null)
    const fetchData = async () => {
      console.log('Calling API...')
      try {
        const response = await fetch('https://localhost:7083/api/getUser/18')
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`)
        }
        data.value = await response.json()
        console.log('API data:', data.value)
      } catch (err) {
        error.value = err.message
        console.log('API error:', err.message)
      }
    }
    onMounted(() => {
      fetchData()
    })
    return {
      data,
      error
    }
  },
  methods: {
    ChangePassClick() {},
    // async SaveChange() {
    //   console.log('Save changes:', this.data)

    //   const updatedUser = {
    //     username: this.data.username,
    //     email: this.data.email,
    //     phone_number: this.data.phone_number,
    //     user_id: this.data.user_id, // If user_id is required
    //     role: this.data.role, // Đảm bảo giá trị này hợp lệ
    //     gender: this.data.gender, // Kiểm tra giá trị hợp lệ
    //     password: this.data.password, // Kiểm tra xem mật khẩu có cần được gửi không
    //     user_image: this.data.user_image
    //   }

    //   try {
    //     const response = await fetch(`https://localhost:7083/api/putUser/${this.data.user_id}`, {
    //       // Dynamic user_id
    //       method: 'PUT',
    //       headers: {
    //         'Content-Type': 'application/json'
    //       },
    //       body: JSON.stringify(updatedUser)
    //     })

    //     if (!response.ok) {
    //       const errorResponse = await response.json()
    //       console.log('Error response:', errorResponse) // Log error details
    //       throw new Error(`Failed to update user. Status: ${response.status}`)
    //     }

    //     const result = await response.json()
    //     console.log('User updated successfully:', result)
    //     alert('User details updated successfully')
    //   } catch (err) {
    //     console.error('Error updating user:', err)
    //     alert('Failed to update user')
    //   }
    // },
    async SaveChange() {
      console.log('Save changes:', this.data)

      const updatedUser = {
        username: this.data.username,
        email: this.data.email,
        phone_number: this.data.phone_number,
        user_id: this.data.user_id // If user_id is required
      }

      try {
        const response = await fetch(`https://localhost:7083/api/putUser/${this.data.user_id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedUser)
        })
        const responseText = await response.text()
        console.log('Response text:', responseText)
        if (responseText) {
          const result = JSON.parse(responseText)
          console.log('User updated successfully:', result)
          alert('User details updated successfully')
        } else {
          console.log('Empty response or non-JSON response from server')
        }

        if (!response.ok) {
          throw new Error(`Failed to update user. Status: ${response.status}`)
        }
      } catch (err) {
        throw new Error('Failed to update user')
      }
    },
    async DeleteAccount() {
      try {
        const response = await fetch(`https://localhost:7083/api/deleteUser/18`, {
          method: 'DELETE'
        })

        if (!response.ok) {
          throw new Error(`Failed to delete user. Status: ${response.status}`)
        }
        alert('Account deleted successfully')
      } catch (err) {
        alert('Failed to delete account')
      }
    }
  }
}
</script>

<style scoped>
@media (min-width: 1200px) {
  .col-xl-3 {
    flex: 0 0 auto;
    width: 25%;
  }
}
@media (min-width: 1200px) {
  .container,
  .container-sm,
  .container-md,
  .container-lg,
  .container-xl {
    max-width: 1390px;
  }
}
.row {
  --bs-gutter-x: 1.5rem;
  --bs-gutter-y: 0;
  display: flex;
  flex-wrap: wrap;
  margin-top: calc(-1 * var(--bs-gutter-y));
  margin-right: calc(-0.5 * var(--bs-gutter-x));
  margin-left: calc(-0.5 * var(--bs-gutter-x));
}
nav.breadcrumbs-section a {
  position: relative;
  padding-right: 25px;
  margin-right: 25px;
  color: #000;
}
.justify-content-between {
  justify-content: space-between !important;
}
.section-profile .menu-profile,
.section-profile .content-profile {
  box-shadow: 0 10px 30px 0 rgb(0 0 0 / 85%);
  border-radius: 10px;
  background-color: #fff;
}
.section-profile .menu-profile .title {
  border-bottom: 1px solid #000;
}
.section-profile .menu-profile .title,
.section-profile .content-profile .title {
  padding: 20px 20px;
  font-size: 20px;
  font-weight: 700;
}
.section-profile .content-profile .titles{
  font-size: 20px;
  font-weight: 700;
}
.section-profile .menu-profile .link-profile,
.section-profile .content-profile .form,
.section-profile .content-profile .info-profile-bottom {
  padding: 20px 20px;
}
.section-profile .menu-profile .link-profile > ul {
  padding-left: 0;
}
.section-profile .menu-profile .link-profile > ul > li {
  list-style: none;
  margin-bottom: 10px;
}
.section-profile .menu-profile .link-profile > ul > li > a {
  padding-bottom: 12px;
  border-bottom: 1px solid #000000;
}
.section-profile .menu-profile .link-profile > ul > li > a {
  display: block;
  color: #8a8a8a;
}
a {
  text-decoration: none;
  color: #000;
}
@media (min-width: 992px) {
  .col-lg-7 {
    flex: 0 0 auto;
    width: 58.33333333%;
  }
}
.content-profile .title {
  font-size: 25px;
  font-weight: 700;
  margin-bottom: 20px;
}
.form {
  display: block;
  margin-top: 0em;
  unicode-bidi: isolate;
}
.content-profile .form .form-group {
  position: relative;
}
.form-group {
  margin-bottom: 15px;
}

.info-profile-bottom .title {
  font-size: 16px;
  margin-bottom: 10px;
}
.info-profile-bottom .btn-action {
  width: 100%;
  max-width: 250px;
  text-align: center;
}

@media (min-width: 541px) {
  .form-control {
    width: 100%;
    height: 48px;
    border-color: #ced4da;
    border-radius: 10px;
  }
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
button,
input,
optgroup,
select,
textarea {
  margin: 0;
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
}
</style>
