# Space Traveler

2D-игра на Unity о подготовке корабля к космическому полёту. Игрок выбирает планету назначения, собирает для корабля еду и топливо в мини-играх, рассчитывает нужное количество топлива и отправляется в путь. Если топлива хватает, полёт заканчивается успешно, если нет — неудачей.

## Игровой цикл

1. **Главное меню**: новая игра, продолжение сохранённой игры, выход.
2. **Сюжет**: вступительный комикс.
3. **Выбор планеты**: у каждой планеты своё расстояние и число препятствий (`Assets/Resources/PlanetsInfo.json`). От них зависит, сколько еды нужно взять.
4. **Экран прогресса** (мастерская): отсюда запускаются мини-игры, открываются калькулятор параметров корабля, меню настроек (музыка, выход в главное меню) и кнопка «Поехали».
   - **Еда — 2048.** Собрать плитками нужное количество еды.
   - **Топливо — платформер на время.** Персонаж ходит по складу и открывает двери с топливом. Каждая дверь — это сначала **взлом схемы** (найти пары), затем **Сокобан**: дотолкать бочки до выхода. Обычная бочка (`!`) даёт 30 единиц топлива, усиленная (`$`) — 60.
   - **Броня — ночная свалка металлолома** (в разработке). Металлолом едет по конвейеру от шредера, раунд идёт на время. Крана пока нет, поэтому временная кнопка «Готово» загружает ровно нужную броню, вес равен броне.
5. **Запуск**: успешный или неудачный финал (комикс). После финала сохранение сбрасывается.

## Требования

- **Unity 6000.3.0f1** (Unity 6.3).
- Пакеты (`Packages/manifest.json`): 2D feature set, TextMesh Pro / uGUI, Newtonsoft JSON (`com.unity.nuget.newtonsoft-json`), Timeline, Aseprite importer.
- **Zenject (Extenject) 9.2.0** — лежит в `Assets/Plugins/Zenject`.

## Как открыть и запустить

1. Открыть папку проекта в Unity Hub с версией редактора 6000.3.0f1.
2. Нажать Play. Редакторный скрипт `Assets/Editor/StartSceneAssigner.cs` всегда запускает игру с `MainMenuScene`, какая бы сцена ни была открыта.
3. Сборка: **File → Build Profiles** (порядок сцен ниже важен).

### Сцены в Build Settings

Переходы между сценами идут по индексам, они собраны в `Assets/Scripts/Model/Consts/SceneNumbers.cs`. **При изменении порядка сцен в Build Settings обновите `SceneNumbers`.**

| # | Сцена | `GameSceneType` |
|---|---|---|
| 0 | `Scenes/MainMenuScene` | `main` |
| 1 | `Scenes/StoryScene` | `story` |
| 2 | `Scenes/GameProgressScene` | `gameProgress` |
| 3 | `Scenes/FuelScene/FuelScene` | `fuel` |
| 4 | `Scenes/FuelScene/FuelSocobanScene` | `fuelSocoban` (грузится поверх сцены топлива) |
| 5 | `Scenes/Scene2048` | `food` |
| 6 | `Scenes/FailLaunchScene` | `fail` |
| 7 | `Scenes/WinLaunchScene` | `success` |
| 8 | `Scenes/PlanetSelectScene` | `planeSelect` |
| 9 | `Scenes/FuelScene/Bypass/BypassHacking` | `bypassHacking` (сейчас не используется) |
| 10 | `Scenes/FuelScene/Bypass/BypassSchemePairs` | `bypassPairs` (грузится поверх сцены топлива) |
| 11 | `Scenes/ArmorScene` | `armor` |

## Архитектура

Зависимости внедряет Zenject.

- **`Assets/Resources/ProjectContext.prefab`** с `ProjectDIInstaller` — сервисы на всю игру:
  - `LocalDataManager` → `UserPrefsManager` — сохранения в `PlayerPrefs` (JSON через Newtonsoft);
  - `SceneLoader` → `UnitySceneLoader` — переходы между сценами (`loadScene(GameSceneType)`), additive-загрузка мини-игр;
  - `TipsManager` — подсказки: в редакторе `DebugTipsManager` (показываются каждый раз), в сборке `GameTipsManager` (один раз);
  - `SoundService` и `TipsScreenUIFactory` — из префабов в `Resources/Prefabs`.
- **В каждой сцене** есть `SceneContext` с `MonoInstaller` (папки `DI/` рядом со сценами), который связывает UI-интерфейсы сцены с компонентами (`FromComponentInHierarchy`).
- Логика сцены — в классе `<Имя>Scene` (например, `GameProgressScene`, `FuelScene`), отображение — в `*UI`. Логика получает UI через интерфейс.
- Состояние корабля — неизменяемая структура `SpaceShipState` с методом `copy(...)`.

### Структура `Assets/Scripts`

```
DI/                 ProjectDIInstaller
Model/              состояние игры, константы, настройки, модели подсказок
Services/           сохранения, загрузка сцен, подсказки, звук, данные планет, расчёт параметров корабля
Scenes/<Сцена>/     логика, UI и DI-установщик каждой сцены
UI/                 общие UI-компоненты: экраны подсказок, комиксы, опции
```

### Сохранения

Ключи `PlayerPrefs`:

| Ключ | Что хранит |
|---|---|
| `space_quest_state` | `SpaceShipState` — текущая игра (планета, сколько нужно и собрано еды/топлива/брони) |
| `space_quest_tips` | `UserTipsState` — какие подсказки уже показаны |
| `user_settings` | `UserSettings` — музыка вкл/выкл |

Сбросить прогресс в редакторе: **Edit → Clear All PlayerPrefs**.

## Уровни Сокобана

Уровни лежат в `Assets/Resources/EasyLevels.txt`, `NormalLevels.txt`, `HardLevels.txt`. Уровни разделяются строкой `;`. Обычная дверь берёт случайный уровень из лёгких, усиленная — из средних.

| Символ | Объект |
|---|---|
| `#` | стена |
| `@` | игрок |
| `!` | бочка (30 топлива) |
| `$` | усиленная бочка (60 топлива) |
| `.` | выход — место для бочки |
| пробел | пол |

Соответствие символов и префабов задаётся в компоненте `SocobanLevelBuilder` сцены `FuelSocobanScene`. Решения уровней — `Assets/Docs/NormalLevelsSolve.txt`, схемы — в `Docs/`.

## Металлолом для брони

Все пластины дают одинаковую броню, но весят по-разному, а каждая единица веса брони стоит `ShipParametersConsts.baseArmorCofficient` (2) топлива. Параметры предметов собраны в `Assets/Scripts/Scenes/ArmorScene/Model/ArmorCollectionConsts.cs`, баланс правится там.

| Предмет | `ScrapType` | Броня | Вес | Магнитится |
|---|---|---|---|---|
| Титан (обломки авиатехники) | `titanium` | +2 | 1 | нет |
| Стальной лист | `steel` | +2 | 2 | да |
| Ржавое кровельное железо | `rustyIron` | +2 | 4 | да |
| Покрышка, доска | `junk` | 0 | 3 | нет |
| Чугунная ванна, холодильник | `castIron` | — | не грузится | да |

Добычу раунда копит `ArmorLoot`: броня, вес и добавка к топливу (вес × коэффициент). Результат раунда заменяет прошлый в `SpaceShipState`, а не прибавляется к нему.

Раунд (`ArmorRound`) длится `roundDuration` секунд, лента разгоняется от `beltStartSpeed` до `beltEndSpeed`. Предметы появляются из шредера с просветом от `minItemGap` до `maxItemGap` и берутся из перемешанного мешка (`ScrapBag`), а не случайными бросками: состав мешка — `scrapBagBase` плюс чугунный хлам по `castIronPerObstacle` на каждое препятствие планеты, одного типа подряд не больше `maxSameTypeInRow`. Все эти числа — в том же `ArmorCollectionConsts`.
