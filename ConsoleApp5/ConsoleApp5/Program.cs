//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Exceptions;
//using Telegram.Bot.Polling;
//using System.Text.Json;
//using ConsoleApp5;
//using System.Timers;
//using Timer = System.Timers.Timer;
//using Telegram.Bot.Types.ReplyMarkups;
//using System.Diagnostics.Eventing.Reader;

//namespace TelegramBotExperiments
//{
//    static class Program
//    {
//        static Update update = null;

//        static ITelegramBotClient bot = new TelegramBotClient("5860388928:AAF93dLHDpcLtl8Gh7o0zj7nRVoa8bTOwlo");
//        public static string convertTo = "none";
//        static string convertFrom = "none";
//        static  bool IsAChoosed = false;
//        static bool IsBChoosed = false;
//        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//        {

//            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
//            Console.WriteLine($"Уникальный идентификатор пользователя: {update.Id}");
//            Console.WriteLine($"Уникальный идентификатор сообщения: {update.Message.MessageId}");
//            Console.WriteLine($"Имя пользователья: {update.Message.From.FirstName}");
//            Console.WriteLine($"Текст сообщения: {update.Message.Text}");


//            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
//            {

//                var message = update.Message;

//                var convertFromKeyboard = new ReplyKeyboardMarkup(
//                     new[]
//                     {
//                        new[]
//                        {
//                            new KeyboardButton("из NZD"),
//                            new KeyboardButton("из USD")
//                        },
//                        new[]
//                        {
//                            new KeyboardButton("из GBP"),
//                            new KeyboardButton("из EUR")
//                        }
//                     }
//                 );

//                var convertToKeyboard1 = new ReplyKeyboardMarkup(
//                     new[]
//                     {
//                        new[]
//                        {
//                            new KeyboardButton("В USD"),
//                            new KeyboardButton("/start")
//                        },
//                     }
//                 );

//                var convertToKeyboard2 = new ReplyKeyboardMarkup(
//                     new[]
//                     {
//                        new[]
//                        {
//                            new KeyboardButton("В GBP"),
//                            new KeyboardButton("В NZD")
//                        },
//                        new[]
//                        {
//                            new KeyboardButton("В EUR"),
//                            new KeyboardButton("/start")
//                        },
//                     }
//                 );

//                switch (message.Text.ToLower())
//                {
//                    case "/start":
//                        await botClient.SendTextMessageAsync(message.Chat, "Выберите валюту для конвертации", replyMarkup: convertFromKeyboard);
//                        break;
//                    case "из eur":
//                        convertFrom = "EUR";
//                        IsAChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, "Выберите валюту в которую будет произведена конвертация", replyMarkup: convertToKeyboard1);
//                        break;
//                    case "из gbp":
//                        convertFrom = "GBP";
//                        IsAChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, "Выберите валюту в которую будет произведена конвертация", replyMarkup: convertToKeyboard1);
//                        break;
//                    case "из usd":
//                        convertFrom = "USD";
//                        IsAChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, "Выберите валюту в которую будет произведена конвертация", replyMarkup: convertToKeyboard2);
//                        break;
//                    case "из nzd":
//                        convertFrom = "NZD";
//                        IsAChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, "Выберите валюту в которую будет произведена конвертация", replyMarkup: convertToKeyboard1);
//                        break;
//                    case "в eur":
//                        convertTo = "EUR";
//                        IsBChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, $"Напишите сумму для конвертации");
//                        break;
//                    case "в nzd":
//                        convertTo = "NZD";
//                        IsBChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, $"Напишите сумму для конвертации");
//                        break;
//                    case "в usd":
//                        convertTo = "USD";
//                        IsBChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, $"Напишите сумму для конвертации");
//                        break;
//                    case "в gbp":
//                        convertTo = "GBP";
//                        IsBChoosed = true;
//                        await botClient.SendTextMessageAsync(message.Chat, $"Напишите сумму для конвертации");
//                        break;
//                }

//                if(int.TryParse(message.Text, out int count) && IsAChoosed && IsBChoosed)
//                { 
//                    await botClient.SendTextMessageAsync(message.Chat, $"Результат конвертации {convertFrom} в {convertTo} - {count * CurrentRates.rates[convertFrom+convertTo]}{convertTo}");
//                }


//            }
//        }

//        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//        {
//            string json = Newtonsoft.Json.JsonConvert.SerializeObject(exception);

//            update = JsonSerializer.Deserialize<Update>(json);
//        }


//        static void Main(string[] args)
//        {
//            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

//            var cts = new CancellationTokenSource();
//            var cancellationToken = cts.Token;
//            var receiverOptions = new ReceiverOptions
//            {
//                AllowedUpdates = { },
//            };

//bot.StartReceiving(
//    HandleUpdateAsync,
//    HandleErrorAsync,
//    receiverOptions,
//    cancellationToken
//);
//            using (Parser parser = new Parser())
//            {
//                parser.Main();
//            }
//            Timer timer = new Timer(1000*60*60*12);
//            timer.Elapsed += OnTimerElapsed;
//            timer.Start();


//            Console.ReadLine();


//        }
//        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
//        {
//            CurrentRates.rates.Clear();
//            using (Parser parser = new Parser())
//            {
//                parser.Main();
//            }
//        }
//    }
//}
//using Telegram.Bot;
//using Telegram.Bot.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace TelegramGameBot
//{
//    class Program
//    {
//        private static ITelegramBotClient bot;
//        private static int secretNumber;
//        private static bool isGameActive = false;
//        private static int trysCount = 0;

//        private static GameState currentState = GameState.None;

//        private enum GameState
//        {
//            None,
//            GuessNumber,
//            RockPaperScissors
//        }

//        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Type == UpdateType.Message)
//            {
//                var message = update.Message;

//                switch (currentState)
//                {
//                    case GameState.None:
//                        await HandleNoneStateAsync(botClient, message);
//                        break;
//                    case GameState.GuessNumber:
//                        await HandleGuessNumberStateAsync(botClient, message);
//                        break;
//                    case GameState.RockPaperScissors:
//                        await HandleRockPaperScissorsStateAsync(botClient, message);
//                        break;
//                }
//            }
//        }

//        private static async Task HandleNoneStateAsync(ITelegramBotClient botClient, Message message)
//        {
//            if (message.Text == "/start")
//            {
//                var replyMarkup = new ReplyKeyboardMarkup(new[]
//                {
//                    new KeyboardButton("Угадай число"),
//                    new KeyboardButton("Камень, ножницы, бумага"),
//                });

//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Выбери игру:", replyMarkup: replyMarkup);
//            }
//            else if (message.Text == "Угадай число")
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Я загадал число от 1 до 200. Попробуй угадать!");
//                secretNumber = new Random().Next(1, 201);
//                isGameActive = true;
//                currentState = GameState.GuessNumber;
//            }
//            else if (message.Text == "Камень, ножницы, бумага")
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Давай поиграем в 'Камень, ножницы, бумага'. Введи свой выбор: камень, ножницы или бумага.");
//                currentState = GameState.RockPaperScissors;
//            }
//        }

//        private static async Task HandleGuessNumberStateAsync(ITelegramBotClient botClient, Message message)
//        {

//            if (int.TryParse(message.Text, out int guess))
//            {
//                trysCount++;
//                if (guess == secretNumber)
//                {
//                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Поздравляю! Ты угадал число за {trysCount} попыток!");
//                    isGameActive = false;
//                    currentState = GameState.None;
//                }
//                else
//                {
//                    string response = guess < secretNumber ? "Попробуй число больше!" : "Попробуй число меньше!";
//                    await botClient.SendTextMessageAsync(message.Chat.Id, response);
//                }
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, введи число.");
//            }
//        }

//        private static async Task HandleRockPaperScissorsStateAsync(ITelegramBotClient botClient, Message message)
//        {
//            string[] options = { "камень", "ножницы", "бумага" };

//            string userChoice = message.Text.ToLower();

//            if (Array.IndexOf(options, userChoice) != -1)
//            {
//                string botChoice = options[new Random().Next(options.Length)];

//                string result = DetermineRockPaperScissorsWinner(userChoice, botChoice);

//                await botClient.SendTextMessageAsync(message.Chat.Id, $"Ты выбрал {userChoice}. Я выбрал {botChoice}. {result}");

//                currentState = GameState.None;
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, выбери камень, ножницы или бумагу.");
//            }
//        }

//        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//        {

//        }

//        private static string DetermineRockPaperScissorsWinner(string userChoice, string botChoice)
//        {
//            if (userChoice == botChoice)
//            {
//                return "Ничья!";
//            }
//            else if (
//                (userChoice == "камень" && botChoice == "ножницы") ||
//                (userChoice == "ножницы" && botChoice == "бумага") ||
//                (userChoice == "бумага" && botChoice == "камень")
//            )
//            {
//                return "Ты победил!";
//            }
//            else
//            {
//                return "Ты проиграл!";
//            }
//        }

//        static void Main(string[] args)
//        {
//            bot = new TelegramBotClient("5860388928:AAF93dLHDpcLtl8Gh7o0zj7nRVoa8bTOwlo");

//            var cts = new CancellationTokenSource();
//            var cancellationToken = cts.Token;
//            var receiverOptions = new ReceiverOptions
//            {
//                AllowedUpdates = { },
//            };

//            bot.StartReceiving(
//                HandleUpdateAsync,
//                HandleErrorAsync,
//                receiverOptions,
//                cancellationToken
//            );

//            Console.WriteLine($"Бот {bot.GetMeAsync().Result.Username} запущен. Нажмите Enter для выхода.");
//            Console.ReadLine();
//        }
//    }
//}

//using System;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Telegram.Bot;
//using Telegram.Bot.Args;
//using Telegram.Bot.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace TelegramGameBot
//{
//    class Program
//    {
//        private static ITelegramBotClient bot;
//        private static int secretNumber;
//        private static bool isGameActive = false;
//        private static int trysCount = 0;

//        private static GameState currentState = GameState.None;

//        private enum GameState
//        {
//            None,
//            GuessNumber,
//            RockPaperScissors
//        }
//        private static async Task<string> SearchGoogleAsync(string query)
//        {
//            string apiKey = "AIzaSyBlUO0cxK-8YonaliFoZBidITx8B4hMRvM";
//            string cx = "b005765fc218449c2";

//            using (HttpClient httpClient = new HttpClient())
//            {
//                string apiUrl = $"https://www.googleapis.com/customsearch/v1?q={query}&key={apiKey}&cx={cx}";

//                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

//                if (response.IsSuccessStatusCode)
//                {
//                    string content = await response.Content.ReadAsStringAsync();
//                    dynamic result = JsonConvert.DeserializeObject(content);

//                    // Process the search result as needed
//                    // For example, extract titles, links, etc.
//                    // Here, I'm just returning the first result's link for simplicity
//                    return result.items[0].link;
//                }
//                else
//                {
//                    // Handle the error
//                    return "Error in search request";
//                }
//            }
//        }

//        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//        {
//            if (update.Type == UpdateType.Message)
//            {
//                var message = update.Message;

//                if (message.Text.StartsWith("/поиск"))
//                {
//                    await HandleSearchAsync(botClient, message);
//                    return;
//                }

//                switch (currentState)
//                {
//                    case GameState.None:
//                        await HandleNoneStateAsync(botClient, message);
//                        break;
//                    case GameState.GuessNumber:
//                        await HandleGuessNumberStateAsync(botClient, message);
//                        break;
//                    case GameState.RockPaperScissors:
//                        await HandleRockPaperScissorsStateAsync(botClient, message);
//                        break;
//                }
//            }
//        }

//        private static async Task HandleNoneStateAsync(ITelegramBotClient botClient, Message message)
//        {
//            if (message.Text == "/start")
//            {
//                var replyMarkup = new ReplyKeyboardMarkup(new[]
//                {
//                    new KeyboardButton("Угадай число"),
//                    new KeyboardButton("Камень, ножницы, бумага"),
//                });

//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Выбери игру:", replyMarkup: replyMarkup);
//            }
//            else if (message.Text == "Угадай число")
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Я загадал число от 1 до 200. Попробуй угадать!");
//                secretNumber = new Random().Next(1, 201);
//                isGameActive = true;
//                currentState = GameState.GuessNumber;
//            }
//            else if (message.Text == "Камень, ножницы, бумага")
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Давай поиграем в 'Камень, ножницы, бумага'. Введи свой выбор: камень, ножницы или бумага.");
//                currentState = GameState.RockPaperScissors;
//            }
//        }

//        private static async Task HandleGuessNumberStateAsync(ITelegramBotClient botClient, Message message)
//        {
//            if (int.TryParse(message.Text, out int guess))
//            {
//                trysCount++;
//                if (guess == secretNumber)
//                {
//                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Поздравляю! Ты угадал число за {trysCount} попыток!");
//                    isGameActive = false;
//                    currentState = GameState.None;
//                }
//                else
//                {
//                    string response = guess < secretNumber ? "Попробуй число больше!" : "Попробуй число меньше!";
//                    await botClient.SendTextMessageAsync(message.Chat.Id, response);
//                }
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, введи число.");
//            }
//        }

//        private static async Task HandleRockPaperScissorsStateAsync(ITelegramBotClient botClient, Message message)
//        {
//            string[] options = { "камень", "ножницы", "бумага" };

//            string userChoice = message.Text.ToLower();

//            if (Array.IndexOf(options, userChoice) != -1)
//            {
//                string botChoice = options[new Random().Next(options.Length)];

//                string result = DetermineRockPaperScissorsWinner(userChoice, botChoice);

//                await botClient.SendTextMessageAsync(message.Chat.Id, $"Ты выбрал {userChoice}. Я выбрал {botChoice}. {result}");

//                currentState = GameState.None;
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, выбери камень, ножницы или бумагу.");
//            }
//        }

//        private static async Task HandleSearchAsync(ITelegramBotClient botClient, Message message)
//        {
//            string query = message.Text.Substring("/поиск".Length).Trim();

//            if (!string.IsNullOrEmpty(query))
//            {
//                string searchResult = await SearchGoogleAsync(query);
//                await botClient.SendTextMessageAsync(message.Chat.Id, searchResult);
//            }
//            else
//            {
//                await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, укажите поисковый запрос.");
//            }
//        }

//        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//        {
//            // Обработка ошибок
//        }

//        static async Task Main(string[] args)
//        {
//            bot = new TelegramBotClient("5860388928:AAF93dLHDpcLtl8Gh7o0zj7nRVoa8bTOwlo");

//            var cts = new CancellationTokenSource();
//            var cancellationToken = cts.Token;
//            var receiverOptions = new ReceiverOptions
//            {
//                AllowedUpdates = { },
//            };

//            bot.StartReceiving(
//                HandleUpdateAsync,
//                HandleErrorAsync,
//                receiverOptions,
//                cancellationToken
//            );

//            Console.WriteLine($"Бот {bot.GetMeAsync().Result.Username} запущен. Нажмите Enter для выхода.");
//            Console.ReadLine();
//        }
//    }
//}

//class Program
//{

//    private static Dictionary<string, string> translationDictionary = new Dictionary<string, string>
//    {
//        {"abandon", "покидать"},
//        {"ability", "способность"},
//        {"absolute", "абсолютный"},
//        {"accept", "принимать"},
//        {"access", "доступ"},
//        {"zebra", "зебра"},
//        {"zero", "ноль"},
//        {"apple", "яблоко"},
//        {"book", "книга"},
//        {"cat", "кошка"},
//        {"dog", "собака"},
//        {"elephant", "слон"},
//        {"flower", "цветок"},
//        {"green", "зеленый"},
//        {"house", "дом"},
//        {"internet", "интернет"},
//        {"jazz", "джаз"},
//        {"kangaroo", "кенгуру"},
//        {"lamp", "лампа"},
//        {"mountain", "гора"},
//        {"notebook", "ноутбук"},
//        {"orange", "апельсин"},
//        {"penguin", "пингвин"},
//        {"queen", "королева"},
//        {"rainbow", "радуга"},
//        {"sun", "солнце"},
//        {"table", "стол"},
//        {"umbrella", "зонтик"},
//        {"victory", "победа"},
//        {"waterfall", "водопад"},
//        {"xylophone", "ксилофон"},
//        {"yoga", "йога"},
//        {"zeppelin", "цеппелин"},
//        {"oxygen", "кислород"},
//        {"understand", "понимать"},
//        {"joy", "радость"},
//        {"kingdom", "королевство"},
//        {"language", "язык"},
//        {"magnet", "магнит"},
//        {"note", "заметка"},
//        {"bicycle", "велосипед"},
//        {"chocolate", "шоколад"},
//        {"diamond", "бриллиант"},
//        {"fireworks", "фейерверк"},
//        {"guitar", "гитара"},
//        {"happiness", "счастье"},
//        {"island", "остров"},
//        {"jungle", "джунгли"},
//        {"kiwi", "киви"},
//        {"lemon", "лимон"},
//        {"moon", "луна"},
//        {"ocean", "океан"},
//        {"parrot", "попугай"},
//        {"quasar", "квазар"},
//        {"rocket", "ракета"},
//        {"sunset", "закат"},
//        {"telescope", "телескоп"},
//        {"unicorn", "единорог"},
//        {"volcano", "вулкан"},
//        {"whale", "кит"},
//        {"xylograph", "ксилография"},
//        {"yogurt", "йогурт"},
//        {"zephyr", "зефир"},
//        {"illusion", "иллюзия"},
//        {"keyboard", "клавиатура"},
//        {"lighthouse", "маяк"},
//        {"mushroom", "гриб"},
//        {"nebula", "туманность"},
//        {"orchid", "орхидея"},
//        {"pyramid", "пирамида"},
//        {"quokka", "квокка"},
//        {"sapphire", "сапфир"},
//        {"trampoline", "батут"},
//        {"ultraviolet", "ультрафиолетовый"},
//        {"vortex", "вихрь"},
//        {"whisper", "шепот"},
//        {"xenon", "ксенон"},
//        {"cascade", "водопад"},
//        {"zeal", "рвение"},
//        {"quill", "перо"},
//        {"navigate", "навигировать"},
//        {"vivid", "яркий"},
//        {"jubilant", "ликующий"},
//        {"luminous", "светящийся"},
//        {"melody", "мелодия"},
//        {"reverie", "мечтание"},
//        {"triumph", "триумф"},
//        {"whimsical", "капризный"},
//        {"breeze", "ветерок"},
//        {"serenity", "безмятежность"},
//        {"quench", "утолять"},
//        {"migrate", "мигрировать"},
//        {"whistle", "свистеть"}
//    };

//    private static ITelegramBotClient bot;
//    private static int mode = 0;
//    private static int trys = 0;
//    private static int correct = 0;
//    private static int max = translationDictionary.Count;

//    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//    {
//        if (update.Type == UpdateType.Message)
//        {
//            var message = update.Message;

//            switch (message.Text.ToLower())
//            {
//                case "/start":
//                    var replyMarkup = new ReplyKeyboardMarkup(new[]
//                    {
//                        new KeyboardButton("русский -> английский"),
//                        new KeyboardButton("английский -> русский"),
//                    });
//                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Выбери режим тренажера:", replyMarkup: replyMarkup);
//                    break;
//                case "русский -> английский":
//                    mode = 1;
//                    await HandleTranslationsGameAsync(botClient, message);
//                    break;
//                case "английский -> русский":
//                    mode = 0;
//                    await HandleTranslationsGameAsync(botClient, message);
//                    break;
//                default:
//                    // Обработка ответов пользователя в режиме игры
//                    await HandleUserResponseAsync(botClient, message);
//                    break;
//            }
//        }
//        else if (update.Type == UpdateType.CallbackQuery)
//        {
//            // Handle callback queries if needed
//        }
//    }

//    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//    {
//        // Handle errors if needed
//    }

//    private static async Task HandleTranslationsGameAsync(ITelegramBotClient botClient, Message message)
//    {
//        var random = new Random();
//        var wordList = new List<string>(translationDictionary.Keys);

//        var randomWordIndex = random.Next(wordList.Count);
//        var wordToTranslate = wordList[randomWordIndex];
//        var correctTranslation = translationDictionary[wordToTranslate];

//        await botClient.SendTextMessageAsync(message.Chat.Id, $"Переведите слово: {wordToTranslate}");
//    }

//    private static async Task HandleUserResponseAsync(ITelegramBotClient botClient, Message message)
//    {
//        var userResponse = message.Text.ToLower();

//        // Проверяем правильность ответа пользователя
//        if (translationDictionary.ContainsValue(userResponse))
//        {
//            await botClient.SendTextMessageAsync(message.Chat.Id, "Правильно! 👍");
//            correct++;
//        }
//        else
//        {
//            await botClient.SendTextMessageAsync(message.Chat.Id, $"Неправильно. Попробуйте еще раз.");
//        }

//        trys++;

//        if (trys < max)
//        {
//            // Продолжаем игру
//            await HandleTranslationsGameAsync(botClient, message);
//        }
//        else
//        {
//            // Завершаем игру и выводим результат
//            await botClient.SendTextMessageAsync(message.Chat.Id, $"Игра завершена. Правильных ответов: {correct}/{max}");
//        }
//    }

//    static async Task Main(string[] args)
//    {
//        bot = new TelegramBotClient("5860388928:AAF93dLHDpcLtl8Gh7o0zj7nRVoa8bTOwlo");  // Replace with your actual Telegram bot token

//        var cts = new CancellationTokenSource();
//        var cancellationToken = cts.Token;
//        var receiverOptions = new ReceiverOptions
//        {
//            AllowedUpdates = { },
//        };

//        bot.StartReceiving(
//            HandleUpdateAsync,
//            HandleErrorAsync,
//            receiverOptions,
//            cancellationToken
//        );

//        Console.WriteLine($"Бот {bot.GetMeAsync().Result.Username} запущен. Нажмите Enter для выхода.");
//        Console.ReadLine();

//    }
//}

using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramGameBot
{
    class Program
    {
        private static ITelegramBotClient bot;
        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                switch (message.Text.ToLower())
                {
                    case "/s":
                        Console.WriteLine(message.Text);
                        await GetFactAsync(botClient, message.Chat.Id);
                        Console.WriteLine(message.Text);
                        break;
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                // Handle callback queries if needed
            }
        }

        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            
        }

        private static async Task GetFactAsync(ITelegramBotClient botClient, long chatId)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("1");

            // Получаем список статей Википедии
            string url = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BB%D1%83%D0%B6%D0%B5%D0%B1%D0%BD%D0%B0%D1%8F:%D0%A1%D0%BB%D1%83%D1%87%D0%B0%D0%B9%D0%BD%D0%B0%D1%8F_%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0";
            HttpResponseMessage response = client.GetAsync(url).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            // Ищем первое предложение в тексте статьи
            int start;

            if (responseBody.Contains(". "))
            {
                start = responseBody.IndexOf(". ");
            }
            else
            {
                start = 0;
            }
            Console.WriteLine("2");

            string fact = responseBody.Substring(0, start);
            Console.WriteLine("3");

            botClient.SendTextMessageAsync(chatId, fact);

            Console.WriteLine(fact);
        }

        static async Task Main(string[] args)
        {
            bot = new TelegramBotClient(""); 

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.WriteLine($"Бот {bot.GetMeAsync().Result.Username} запущен. Нажмите Enter для выхода.");
            Console.ReadLine();

        }
    }
}


