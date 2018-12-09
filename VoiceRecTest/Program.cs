using System;
using System.Diagnostics;
using System.Net;
using System.Speech.Recognition;

namespace VoiceRecTest
{
    class Program
    {


        static void Main(string[] args)
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

            Choices commands = new Choices();
            commands.Add(new string[] { "hello", "say my name", "open chrome", "what day is it today", "download a cool wallpaper" });
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
                case "hello":
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
                    Console.WriteLine("It's" + DateTime.Now.DayOfWeek);
                    break;
                case "download a cool wallpaper":
                    using (WebClient client = new WebClient())
                    {
                        var user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        client.DownloadFile(new Uri("https://github.com/MrChickenBacon/HotPink/raw/master/Hotpink/Back.PNG"), $@"{user}\desktop\Cool Wallpaper.PNG");
                    }
                    Console.WriteLine("I've placed it on the desktop for you :)");
                    break;
            }
        }
    }
}