import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { AppModule } from "./app.module";
import { getTranslationProviders } from "./i18n-providers";

const translationDisabled: boolean = true;

if (translationDisabled) {
    platformBrowserDynamic().bootstrapModule(AppModule).catch(err => hanldeError(err));
} else {
    getTranslationProviders().then((providers: Object[]) => {
        const options: any = { providers };
        platformBrowserDynamic().bootstrapModule(AppModule, options).catch(err => hanldeError(err));
    }).catch(err => hanldeError(err));
}

const hanldeError = (err: any): void => {
    console.warn('Error occured when bootstapping app ...')
    console.error(err);
}
