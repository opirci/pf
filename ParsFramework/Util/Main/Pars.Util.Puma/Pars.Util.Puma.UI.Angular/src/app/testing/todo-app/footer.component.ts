import { Component } from '@angular/core';
import { TodoList } from './todo-list.type';

@Component({
    selector: 'app-footer',
    template: '<ng-content></ng-content>',  
    moduleId: module.id
})
export class FooterComponent {
    constructor(private todos: TodoList) {
        debugger;
    }
}