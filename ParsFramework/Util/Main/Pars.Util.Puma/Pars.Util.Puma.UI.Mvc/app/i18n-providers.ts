﻿import {
    TRANSLATIONS, TRANSLATIONS_FORMAT, LOCALE_ID
    //, MissingTranslationStrategy
} from '@angular/core';
import { CompilerConfig } from '@angular/compiler';

export function getTranslationProviders(): Promise<Object[]> {

    // Get the locale id from the global
    const locale = document['locale'] as string;


    // return no providers if fail to get translation file for locale
    const noProviders: Object[] = [];

    // No locale or U.S. English: no translation providers
    //if (!locale || locale === 'en-US') {
    if (!locale) {
        return Promise.resolve(noProviders);
    }

    // Ex: 'locale/messages.es.xlf`
    const translationFile = `./locale/messages.${locale}.xml`;

    return getTranslationsWithSystemJs(translationFile)
        .then((translations: string) => [
            { provide: TRANSLATIONS, useValue: translations },
            { provide: TRANSLATIONS_FORMAT, useValue: 'xml' },
            { provide: LOCALE_ID, useValue: locale }
            //,{ provide: CompilerConfig, useValue: new CompilerConfig({ missingTranslation: MissingTranslationStrategy.Error }) }
        ])
        .catch(() => noProviders); // ignore if file not found
}

declare var System: any;

function getTranslationsWithSystemJs(file: string) {
    return System.import(file + '!text'); // relies on text plugin
}

