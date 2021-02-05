:: (1) AOT ye göre compile etme; Check-in öncesi bir sorun olmadığını test amaçlı
npm run build:check

:: (2) HTML template ler üzerinde lokalize edilecek metinlerin i18n attribute u ile işaretlenmesi https://angular.io/docs/ts/latest/cookbook/i18n.html 

:: (3) xi18n tool u çalıştırılarak app\locale klasörü altına messages.xlf dosyası oluşturma.
npm run i18n

:: (4) Proje root undaki locale klasörüne önceki adıma oluşan xlf dosyasını kopyalayıp bu dosyaya göre mevcut xml dosyalarının update ini sağlama
:: DıKKAT: locale klasörü altındaki dosyaların check out edilmesi unutulmamamlı!
npm run i18n:update

:: (5) locale klasöründe oluşan .update uzantılı dosyalar .xml dosyaları üzerine kopyalanıp **NEW** ve **MODIFY** ile işaretlenen değişiklikleri tercüme etme. Dosyaların
:: utf-8 olarak kaydedilmesi gerekmektedir.File > Save Advanced

:: (6) 
npm run build:aot:tr
npm run build:aot:en