Тестовый проект для OpenMyGames

1. Игровая сцена находится в папке Scenes
2. Необходимо просто запустить Play mode
3. Данные об уровнях находятся в xml файле по пути Resources/XmlFiles/Levels.xml
4. Сохранение текущего уровня через PlayerPrefs
5. Уровни создаются программно исходя из xml файла (числа в аттрибуте <rowData row = .... соответствуют типу перечисления).
   После парсинга полученная матрица преобразуется в матрицу типа ElementType[,], через которую и происходят преобразования игрового поля
