<template>
  <div>
    <form @submit.prevent="addTodo">
      <input v-model="newTodo" placeholder="Add new todo" />
      <button>Add</button>
    </form>

    <ul>
      <li v-for="item in todos" :key="item.id">        
        <span>
          {{ item.name }}
        </span>        
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
const todos = ref([])
const newTodo = ref('')

const api = '/api/todo'

async function fetchTodos() {
  const res = await fetch(api)
  todos.value = await res.json()
}

async function addTodo() {
  if (!newTodo.value.trim()) return
  const res = await fetch(api, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name: newTodo.value, isComplete: false })
  })
  newTodo.value = ''
  await fetchTodos()
}

onMounted(fetchTodos)
</script>
