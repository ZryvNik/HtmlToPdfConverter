# HtmlToPdfConverter

## Web-приложение конвертирующее html-файл в pdf

## Используемые технологии
- Mediatr
- MassTransit (использовался как InMemory)
- LiteDb
- Net Core 6
- Razor Pages
- Puppeteer Sharp

## Описание АПИ

POST /Upload - позволяет загрузить файл на сервер и получить некоторый CorrelationId в ответ

GET /GetStatus - запрашивает статус конвертирования файла по CorrelationId, полученном при загрузке файла (вернёт также идентификатор pdf файла, когда конвертация будет закончена)

GET /Download - позволяет скачать файл по идентификатору

Предполагается, что клиент отправить файл на обработку и получит в ответ идентификатор обработки (CorrelationId), по которому сможет отследить статус обработки. Когда обработка будет закончена, то клиент получив идентификатор pdf-файла в хранилище, сможет сформировать ссылку на скачивание. 

За отправку закачанных в хранилище файлов в обработку отвечает фоновый процесс. Он опрашивает базу на предмет появления записей в статусе Added, если такова запись имеется, то ей проставляется статус InProgress и отправляется сообщение через Mass Transit, чтобы началась обработка. Консьюмер получает сообщение и начинает обработку. 

Также имеется фоновый процесс, который очистит базу и хранилище от устаревшей информации (считается, что пользователь не требует хранить ссылку на сконвертированный файл вечно, врем жизни можно задать через конфигурацию приложения)
