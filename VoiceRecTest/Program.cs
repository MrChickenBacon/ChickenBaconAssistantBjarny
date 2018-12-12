using System;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

namespace VoiceRecTest
{
    class Program
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
            string pvParam, uint fWinIni);

        public static string Mine { get; set; }
        public static int Emil { get; set; }
        public static string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}";

        static Random random = new Random();

        static int Numbers()
        {
            int number = random.Next(1, 550);
            return number;
        }

        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            Engine();
            SoundPlayer player0 = new SoundPlayer($@"{Path}\desktop\sounds\start.wav");
            player0.Play();
            Thread.Sleep(1000);
            Console.WriteLine("Waiting for voice input.");
            Console.ReadKey();
        }

        private static void Engine()
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            var gBuilder = CommandsGrammarBuilder();
            Grammar grammar = new Grammar(gBuilder);
            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private static GrammarBuilder CommandsGrammarBuilder()
        {
            Choices commands = new Choices();
            commands.Add(new string[]
            {
                "bjarny", "hello computer", "put on country wallpaper", "put on bathman wallpaper", "say my name",
                "open up browser chrome", "what day is it today",
                "download a hot wallpaper", "play me a cool song", "qvamma", "payday payday", "i told him", "mine mine",
                /*"nein nein nein nein",*/ "email", "name count", "put on some christmas music", "yes", "eskil", "nice",
                "that's what she said", "play mario medley", "play chill music", "crowd goes wild",
                "where can i get this code?",
                "what's your name", "eh", "hear crickets?", "open visual studio", "open V S Code", "open my git hub",
                "new random wallpaper"
            });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            return gBuilder;
        }

        private static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "hello computer":
                    SoundPlayer player7 = new SoundPlayer($@"{ Path }\desktop\sounds\hello.wav");
                    player7.Play();
                    break;
                case "what's your name":
                    synthesizer.SpeakAsync("It's me, Mario!");
                    Thread.Sleep(1000);
                    synthesizer.SpeakAsync("Nono. I kid. My name is Chickenbacon, Assistant.");
                    Thread.Sleep(1000);
                    synthesizer.SpeakAsync("Or just call me Bjarny");
                    break;
                case "say my name":
                    synthesizer.SpeakAsync("ChickenBacon. Our master creator");
                    break;
                case "bjarny":
                    synthesizer.SpeakAsync("How may i help you today?");
                    break;
                case "open up browser chrome":
                    Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                    Console.WriteLine("Here yah go!");
                    break;
                case "what day is it today":
                    synthesizer.SpeakAsync("It's " + DateTime.Now.DayOfWeek);
                    Console.WriteLine("It's " + DateTime.Now.DayOfWeek);
                    break;
                case "download a hot wallpaper":
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri("https://github.com/MrChickenBacon/HotPink/raw/master/Hotpink/Back.PNG"), $@"{Path}\desktop\Hot Wallpaper.PNG");
                    }
                    SystemParametersInfo(0x0014, 0, $@"{Path}\desktop\Hot Wallpaper.PNG", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "new random wallpaper":
                    SystemParametersInfo(0x0014, 0, $@"{Path}\desktop\wallpapers\{Numbers()}.jpg", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "put on bathman wallpaper":
                    SystemParametersInfo(0x0014, 0, $@"{Path}\desktop\wallpapers\bathman.jpg", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "put on country wallpaper":
                    SystemParametersInfo(0x0014, 0, $@"{Path}\desktop\wallpapers\country.jpg", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "play me a cool song":
                    try
                    {
                        Console.WriteLine("Oh! It's Party time!");
                        SoundPlayer player1 = new SoundPlayer("https://github.com/MrChickenBacon/Surge/raw/master/town.wav");
                        player1.Play();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    break;
                case "qvamma":
                    SoundPlayer player2 = new SoundPlayer(@"C:\Windows\media\tada.wav");
                    player2.Play();
                    break;
                case "payday payday":
                    Console.WriteLine("Payday!");
                    SoundPlayer player3 = new SoundPlayer($@"{ Path }\desktop\sounds\ka-ching.wav");
                    player3.Play();
                    break;
                case "i told him":
                    Console.WriteLine("Yeah!");
                    SoundPlayer player4 = new SoundPlayer($@"{ Path }\desktop\sounds\Mmm.wav");
                    player4.Play();
                    break;
                case "mine mine":
                    Console.WriteLine("Mine?");
                    SoundPlayer player5 = new SoundPlayer($@"{ Path }\desktop\sounds\mine.wav");
                    player5.Play();
                    break;
                case "email":
                    Console.WriteLine("Emil?");
                    synthesizer.SpeakAsync("pling");
                    Emil++;
                    break;
                case "name count":
                    Console.WriteLine(Emil);
                    synthesizer.SpeakAsync("Emil has been said " + Emil + " times");
                    break;
                case "nein nein nein nein":
                    Console.WriteLine("Nine!");
                    SoundPlayer player6 = new SoundPlayer($@"{ Path }\desktop\sounds\nein.wav");
                    player6.Play();
                    break;
                case "put on some christmas music":
                    Console.WriteLine("Jingle");
                    Process.Start("https://www.youtube.com/watch?v=QOAkVCigk5Y");
                    break;
                case "yes":
                    Console.WriteLine("No");
                    synthesizer.SpeakAsync("No");
                    break;
                //case "no":
                //    Console.WriteLine("No");
                //    synthesizer.SpeakAsync("yes");
                //    Thread.Sleep(1000);
                //    synthesizer.SpeakAsync("Ah! you got me!");
                //    break;
                case "eskil":
                    synthesizer.SpeakAsync("He's that guitar man right?");
                    break;
                case "nice":
                    Console.WriteLine("Nice");
                    SoundPlayer player8 = new SoundPlayer($@"{ Path }\desktop\sounds\nice.wav");
                    player8.Play();
                    break;
                case "that's what she said":
                    Console.WriteLine("Yey");
                    SoundPlayer player9 = new SoundPlayer($@"{ Path }\desktop\sounds\crowd laughter.wav");
                    player9.Play();
                    break;
                case "play mario medley":
                    Console.WriteLine("Playing.");
                    SoundPlayer player10 = new SoundPlayer($@"{ Path }\desktop\sounds\mario medley.wav");
                    player10.Play();
                    break;
                case "play chill music":
                    Console.WriteLine("Playing.");
                    SoundPlayer player11 = new SoundPlayer($@"{ Path }\desktop\sounds\chillmusic.wav");
                    player11.Play();
                    break;
                case "eh":
                    Console.WriteLine("Nope.");
                    SoundPlayer player12 = new SoundPlayer($@"{ Path }\desktop\sounds\nope.wav");
                    player12.Play();
                    break;
                case "crowd goes wild":
                    Console.WriteLine("Wild crowd.");
                    SoundPlayer player13 = new SoundPlayer($@"{ Path }\desktop\sounds\Applause.wav");
                    player13.Play();
                    break;
                case "hear crickets?":
                    Console.WriteLine("Crickets.");
                    SoundPlayer player14 = new SoundPlayer($@"{ Path }\desktop\sounds\crickets.wav");
                    player14.Play();
                    break;
                case "where can i get this code?":
                    Console.WriteLine("https://github.com/MrChickenBacon/ChickenBacon-Assistant");
                    synthesizer.SpeakAsync("Look for ChickenBacon Assistant on github. Go to https://github.com/MrChickenBacon/ChickenBacon-Assistant");
                    break;
                case "open visual studio":
                    Console.WriteLine("Opening VS");
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Visual Studio 2017");
                    synthesizer.SpeakAsync("Code away!");
                    break;
                case "open V S Code":
                    Console.WriteLine("Opening VS Code");
                    Process.Start(@"C:\Users\Get Academy\AppData\Local\Programs\Microsoft VS Code\Code.exe");
                    synthesizer.SpeakAsync("Code away!");
                    break;
                case "open my git hub":
                    Process.Start("https://github.com/MrChickenBacon?tab=repositories");
                    synthesizer.SpeakAsync("Opening");
                    break;
            }
        }
    }
}
