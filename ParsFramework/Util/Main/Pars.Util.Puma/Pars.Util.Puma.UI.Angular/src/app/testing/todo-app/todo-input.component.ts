import { Component, EventEmitter, Output } from '@angular/core';
import { Todo } from './todo.type';

@Component({
    selector: 'todo-input',
    templateUrl: 'todo-input.component.html',    
    moduleId: module.id
})
export class TodoInputComponent {
    title: string;
    @Output() onTodo = new EventEmitter<Todo>();
    addTodo() {
        this.onTodo.emit({
            title: this.title,
            completed: false
        });
        this.title = '';
    }
}