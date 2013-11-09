using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScaryStories.Services;

namespace ScaryStories.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SpeechSynthesizerService service=new SpeechSynthesizerService();
            service.Speak("Привет. Как дела мудило. Я тебя в ротан ебал.");
        }
    }
}
