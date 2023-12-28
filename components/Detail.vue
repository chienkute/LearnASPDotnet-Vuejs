<template>
  <div class="card">
    <img :src="user.avatar" alt="Avatar" />
    <div class="card-body">
      <h3>{{ user.knownAs }}</h3>
      <p>{{ user.gender }}</p>
      <p>{{ user.city }}</p>
    </div>
  </div>
</template>
<script lang="ts" setup>
import type { User } from "~/types";
defineProps({
  user: { type: Object as PropType<User>, required: true },
});
</script>
