<template>
  <div class="container">
    <div class="row">
      <input
        class="form-control my-3"
        type="text"
        v-model="keySearch"
        @keyup.enter="search"
      />
    </div>
    <div v-if="members" class="row">
      <div class="col-3" v-for="(user, index) in members" :key="index">
        <Detail :user="user" />
      </div>
    </div>
    <div class="row">
      <button class="btn btn-primary text-center my-3" @click="loadMore">
        Load more...
      </button>
    </div>
  </div>
</template>

<script setup>
import Detail from "../components/Detail";
const userTokenCookie = useCookie("userToken");
const members = ref([]);
const currentPage = ref(0);
const pageSize = ref(20);
const keySearch = ref("");
const loadMore = async () => {
  const { data } = await useFetch("http://localhost:5128/api/members", {
    headers: { Authorization: `Bearer ${userTokenCookie.value}` },
    query: { currentPage, pageSize, keySearch },
  });
  if (data && data.value) {
    members.value.push(...data.value);
    currentPage.value++;
  }
};
const search = async () => {
  currentPage.value = 0;
  members.value = [];
  await loadMore();
};
loadMore();
</script>
