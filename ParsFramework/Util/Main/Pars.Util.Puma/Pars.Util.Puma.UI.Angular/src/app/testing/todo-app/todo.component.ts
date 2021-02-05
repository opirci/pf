import { Component, EventEmitter, Output, Input } from '@angular/core';
import { Todo } from './todo.type';

@Component({
    selector: 'todo-item',
    templateUrl: 'todo.component.html',
    styleUrls: [ 'todo.component.css'],
    moduleId: module.id
})
export class TodoComponent {
    @Output() onCompletionChange = new EventEmitter<Todo>();
    @Input() todo: Todo;
    completionChanged(todo: Todo) {
        this.onCompletionChange.emit(todo);
    }
}
