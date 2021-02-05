import { Component, ViewChild } from '@angular/core';

import { TodoInputComponent } from './todo-input.component';
import { TodoList } from './todo-list.type';
import { Todo } from './todo.type';

@Component({
    selector: 'todo-app',
    viewProviders: [TodoList],
    templateUrl: 'todo-app.component.html',    
    moduleId: module.id    
})
export class TodoAppComponent {
    @ViewChild(TodoInputComponent)
    input: TodoInputComponent;

    constructor(public todos: TodoList) { }

    addTodo(todo: Todo) {
        this.todos.add(todo);
    }
    ngAfterViewInit() {
        // console.log(this.input);
    }
}