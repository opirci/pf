import { Component } from '@angular/core';
import { FooterComponent } from './footer.component';
import { TodoList } from './todo-list.type';

@Component({
    moduleId: module.id,
    selector: 'demo-todo-app',
    templateUrl: 'demo-todo-app.component.html',
    styles: [
        'todo-app { margin-top: 20px; margin-left: 20px; }'
    ],
    providers: [TodoList]
})
export class DemoTodoAppComponent {
}
