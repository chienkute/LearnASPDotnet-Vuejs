<template>
  <nav class="navbar navbar-expand-lg bg-primary">
    <div class="container-fluid">
      <a class="navbar-brand" href="#">Navbar</a>
      <div class="navbar-collapse">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="/">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">Link</a>
          </li>
        </ul>
        <form class="d-flex" role="search" v-if="!userTokenCookie">
          <input
            class="form-control me-2"
            type="text"
            placeholder="Username"
            v-model="userLogin.username"
          />
          <input
            class="form-control me-2"
            type="password"
            placeholder="Password"
            v-model="userLogin.password"
          />
          <button class="btn btn-success" type="button" @click="login">
            Login
          </button>
        </form>
        <button
          v-else
          class="btn btn-success"
          type="button"
          @click="userTokenCookie = null"
        >
          Logout
        </button>
      </div>
    </div>
  </nav>
</template>

<script lang="ts" setup>
import type { UserLogin } from "~/types";

const userLogin: UserLogin = { username: "jeiler0@columbia.edu", password: "Password$" };
const userTokenCookie = useCookie("userToken");
const login = async () => {
  if (userLogin.username && userLogin.password) {
    const { data } = await useFetch<string>(
      "http://localhost:5128/api/auth/login",
      {
        method: "POST",
        body: userLogin,
      }
    );
    if (data && data.value) userTokenCookie.value = data.value;
  }
};
</script>
