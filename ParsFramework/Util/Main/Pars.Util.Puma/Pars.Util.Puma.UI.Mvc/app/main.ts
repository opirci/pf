import { enableProdMode } from '@angular/core';
import { platformBrowser } from "@angular/platform-browser";
import { AppModuleNgFactory } from "../aot/app/app.module.ngFactory";

console.log('Running AOT compiled');
enableProdMode();
platformBrowser().bootstrapModuleFactory(AppModuleNgFactory).catch(err => handleError(err));

const handleError = (err: any): void => {
    console.error(err);
}
