<template>
  <div>
    <form @submit.prevent="addTodo">
      <input v-model="newTodo" placeholder="Add new todo" />
      <select v-model="selectedPersonId">
        <option v-for="person in persons" :value="person.id">
          {{ person.name }}
        </option>
      </select>        
      <button>Add</button>
    </form>

    <ul>
      <li v-for="item in todos" :key="item.id">
        <input type="checkbox" v-model="item.isComplete" @change="updateTodo(item)" />
        <span :style="{ textDecoration: item.isComplete ? 'line-through' : 'none' }">
          {{ item.name }}
        </span>
        <button @click="deleteTodo(item.id)">❌</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
const todos = ref([])
const newTodo = ref('')
const persons = ref([])
const selectedPersonId = ref('');

const api = '/api/todo'

async function fetchTodos() {
  const res = await fetch(api)
  todos.value = await res.json()
}

async function fetchPersons() {
  const res = await fetch('api/persons')
  persons.value = await res.json()
}

async function addTodo() {
  if (!newTodo.value.trim()) return
  const res = await fetch(api, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name: newTodo.value, isComplete: false, personId: selectedPersonId.value })
  })
  newTodo.value = ''
  await fetchTodos()
}

async function updateTodo(item) {
  await fetch(`${api}/${item.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(item)
  })
}

async function deleteTodo(id) {
  await fetch(`${api}/${id}`, { method: 'DELETE' })
  await fetchTodos()
}

onMounted(async () => {
  await fetchTodos()
  await fetchPersons()
})
</script>
