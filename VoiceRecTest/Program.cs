using System;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using Choices = Microsoft.Speech.Recognition.Choices;
using Grammar = Microsoft.Speech.Recognition.Grammar;
using GrammarBuilder = Microsoft.Speech.Recognition.GrammarBuilder;
using RecognizeMode = Microsoft.Speech.Recognition.RecognizeMode;
using SpeechRecognitionEngine = Microsoft.Speech.Recognition.SpeechRecognitionEngine;
using SpeechRecognizedEventArgs = Microsoft.Speech.Recognition.SpeechRecognizedEventArgs;

namespace VoiceRecTest
{
    class Program
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
            string pvParam, uint fWinIni);

        public static string Mine { get; set; }
        public static int Emil { get; set; }
        public static string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}";

        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            Choices commands = new Choices();
            commands.Add(new string[] { "hello computer", "say my name", "open chrome", "what day is it today", "put on a hot wallpaper", "put on a country wallpaper", "put on bathman wallpaper", "play me a cool song", "qvamma", "payday payday", "i told him", "nein", "email", "name count", "mine mine", "put on some christmas music", "69", "yes", "Eskil", "Thank you", "That's what she said" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;

            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            Console.WriteLine("Waiting for voice command.");
            Console.ReadKey();
        }

        private static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "hello computer":
                    Console.WriteLine("Hello?");
                    SoundPlayer player8 = new SoundPlayer($@"{ path }\desktop\Hello.wav");
                    player8.Play();
                    break;
                case "say my name":
                    Console.WriteLine("Chris?! Is that you?");
                    break;
                case "open chrome":
                    Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                    Console.WriteLine("Here yah go!");
                    break;
                case "what day is it today":
                    synthesizer.SpeakAsync("It's " + DateTime.Now.DayOfWeek);
                    Console.WriteLine("It's " + DateTime.Now.DayOfWeek);
                    break;
                case "put on a hot wallpaper":
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri("https://github.com/MrChickenBacon/HotPink/raw/master/Hotpink/Back.PNG"), $@"{path}\desktop\Hot Wallpaper.PNG");
                    }
                    SystemParametersInfo(0x0014, 0, $@"{path}\desktop\Hot Wallpaper.PNG", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "put on a country wallpaper":
                    SystemParametersInfo(0x0014, 0, $@"{path}\pictures\country.jpg", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "put on bathman wallpaper":
                    SystemParametersInfo(0x0014, 0, $@"{path}\pictures\bathman.jpg", 0x0001);
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
                    SoundPlayer player3 = new SoundPlayer($@"{ path }\desktop\ka-ching.wav");
                    player3.Play();
                    break;
                case "i told him":
                    Console.WriteLine("Yeah!");
                    SoundPlayer player4 = new SoundPlayer($@"{ path }\desktop\Mmm.wav");
                    player4.Play();
                    break;
                case "mine mine":
                    Console.WriteLine("Mine?");
                    SoundPlayer player5 = new SoundPlayer($@"{ path }\desktop\mine.wav");
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
                case "nein":
                    Console.WriteLine("Nine?");
                    SoundPlayer player6 = new SoundPlayer($@"{ path }\desktop\nein.wav");
                    player6.Play();
                    break;
                case "put on some christmas music":
                    Console.WriteLine("Jingle");
                    Process.Start("https://www.youtube.com/watch?v=QOAkVCigk5Y");
                    break;
                case "69":
                    synthesizer.SpeakAsync("6 9 six to the nine. YOYOYO, hey to the ho!");
                    break;
                case "yes":
                    synthesizer.SpeakAsync("no!");
                    break;
                case "Eskil":
                    synthesizer.SpeakAsync("He's that guitar man right?");
                    break;
                case "Thank you":
                    synthesizer.SpeakAsync("Your welcome!");
                    break;
                case "That's what she said":
                    Console.WriteLine("Yee");
                    SoundPlayer player7 = new SoundPlayer($@"{ path }\desktop\crowd laughter.wav");
                    player7.Play();
                    break;
            }
        }
    }
}
