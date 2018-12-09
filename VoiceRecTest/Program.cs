using System;
using System.Speech.Recognition;

namespace VoiceRecTest
{
    class Program
    {


        static void Main(string[] args)
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

            Choices commands = new Choices();
            commands.Add(new string[] { "say hello", "print my name", "hello world" });
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
                case "say hello":
                    Console.WriteLine("Hello!");
                    break;
                case "print my name":
                    Console.WriteLine("Chris?!");
                    break;
                case "hello world":
                    Console.WriteLine("Hello you from the world!");
                    break;
            }
        }
    }
}
