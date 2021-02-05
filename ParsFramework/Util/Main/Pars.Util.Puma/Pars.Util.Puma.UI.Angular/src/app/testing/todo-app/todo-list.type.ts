import { Todo } from './todo.type';
export { Todo } from './todo.type';

export class TodoList {
  private todos: Todo[] = [];
  add(todo: Todo) {
    this.todos.push(todo);
  }
  remove(todo: Todo) {
    this.todos.splice(this.todos.indexOf(todo), 1);
  }
  set(todo: Todo, index: number) {
    this.todos[index] = todo;
  }
  get(index: number) {
    return this.todos[index];
  }
  getAll() {
    return this.todos;
  }
}