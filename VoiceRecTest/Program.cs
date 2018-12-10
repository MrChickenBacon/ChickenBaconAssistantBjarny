using System;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace VoiceRecTest
{
    class Program
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
            string pvParam, uint fWinIni);

        public static string Mine { get; set; }
        public static int Emil { get; set; }

        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            Choices commands = new Choices();
            commands.Add(new string[] { "hello computer", "say my name", "open chrome", "what day is it today", "download a hot wallpaper", "play me a cool song", "qvamma", "payday", "i told him", "nein", "email", "name count", "mine mine", "put on some christmas music", "69", "yes", "Eskil" });
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
                    Console.WriteLine("Hello! Chris");
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
                case "download a hot wallpaper":
                    var user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri("https://github.com/MrChickenBacon/HotPink/raw/master/Hotpink/Back.PNG"), $@"{user}\desktop\Hot Wallpaper.PNG");
                    }
                    SystemParametersInfo(0x0014, 0, $@"{user}\desktop\Hot Wallpaper.PNG", 0x0001);
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
                case "payday":
                    Console.WriteLine("Payday!");
                    var user1 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    SoundPlayer player3 = new SoundPlayer($@"{ user1 }\desktop\ka-ching.wav");
                    player3.Play();
                    break;
                case "i told him":
                    Console.WriteLine("Yeah!");
                    var user3 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    SoundPlayer player4 = new SoundPlayer($@"{ user3 }\desktop\Mmm.wav");
                    player4.Play();
                    break;
                case "mine mine":
                    Console.WriteLine("Mine?");
                    var user4 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    SoundPlayer player5 = new SoundPlayer($@"{ user4 }\desktop\mine.wav");
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
                    var user5 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    SoundPlayer player6 = new SoundPlayer($@"{ user5 }\desktop\nein.wav");
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
            }
        }
    }
}
