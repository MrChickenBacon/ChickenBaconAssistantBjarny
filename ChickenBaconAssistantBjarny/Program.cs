using System;
using System.Globalization;
using System.Media;
using System.Speech.Recognition;
using System.Threading;

namespace ChickenBaconAssistantBjarny
{
    class Program
    {
        public static string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}";

        public static SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));

        static void Main(string[] args)
        {
            SpeechRecoEngine();
            using (SoundPlayer player = new SoundPlayer($@"{ Path }\desktop\sounds\start.wav"))
            {
                player.Play();
            }
            Thread.Sleep(1000);
            Console.WriteLine("Waiting for voice input.");
            Console.ReadKey();
        }

        public static void SpeechRecoEngine()
        {
            var gBuilder = CommandsGrammarBuilder();
            Grammar grammar = new Grammar(gBuilder);
            recEngine.LoadGrammarAsync(grammar);
            try
            {
                recEngine.SetInputToDefaultAudioDevice();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine("Please plug in a microphone.");
                Console.ReadLine();
                throw;
            }
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            recEngine.RecognizeAsync(RecognizeMode.Single);
        }

        private static GrammarBuilder CommandsGrammarBuilder()
        {
            Choices commands = new Choices();
            commands.Add(new string[]
            {
                "Hey Bjarny"
            });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            return gBuilder;
        }

        private static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Hey Bjarny":
                    Console.WriteLine("Hey");
                    using (SoundPlayer player = new SoundPlayer($@"C:\Windows\media\Speech On.wav"))
                    {
                        player.Play();
                    }
                    RealCommands.SpeechRecoEngine2();
                    break;
            }
        }
    }
}
