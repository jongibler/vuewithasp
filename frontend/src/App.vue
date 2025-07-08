<template>
  <div class="container">
    <h1>Todo List</h1>
    <TodoList />
  </div>
</template>

<script setup>
import TodoList from './components/TodoList.vue'
</script>

<style>
.container {
  max-width: 600px;
  margin: 2rem auto;
  font-family: sans-serif;
}
</style>
