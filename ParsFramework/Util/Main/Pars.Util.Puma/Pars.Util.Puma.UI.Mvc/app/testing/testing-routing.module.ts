import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//import { TestingComponent } from "./testing.component";
//import { SupplierListTestComponent } from "./supplier-list-test.component";
import { DemoTodoAppComponent } from "./todo-app/demo-todo-app.component";

const routes: Routes = [  
    //{ path: 'tesing', component: TestingComponent },    
    { path: 'todo', component: DemoTodoAppComponent},
    //{ path: 'SupplierTest', component: SupplierListTestComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]   
})

export class TestingRouterModule { }

export const testingModuleRoutableComponents = [/*TestingComponent, SupplierListTestComponent,*/ DemoTodoAppComponent];
