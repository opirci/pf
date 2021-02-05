import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { TestingRouterModule, testingModuleRoutableComponents} from './testing-routing.module';

import { FooterComponent } from './todo-app/footer.component';
import { DemoTodoAppComponent } from './todo-app/demo-todo-app.component';
import { TodoAppComponent } from './todo-app/todo-app.component';
import { TodoInputComponent } from './todo-app/todo-input.component';
import { TodoComponent } from './todo-app/todo.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        TestingRouterModule
    ],
    exports: [DemoTodoAppComponent],
    declarations: [TodoComponent, TodoInputComponent, TodoAppComponent, FooterComponent, testingModuleRoutableComponents],
    

})
export class TestingModule { }


