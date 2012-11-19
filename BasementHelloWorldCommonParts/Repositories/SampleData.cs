using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.Repositories
{
    public class SampleData
    {
        public static Dictionary<string, string> defaultLanguages = new Dictionary<string, string>()
        {
             {"de","Deutsch"},
             {"en","English"},
             {"fr", "Français"},
             {"ru", "Русский"}
        };

        public static Dictionary<LanguageWord, string> defaultTranslations()
        {
            Dictionary<LanguageWord, String> ausgabe = new Dictionary<LanguageWord, string>();

            foreach (KeyValuePair<string,string> lang in defaultLanguages)
            {
                    switch (lang.Key)
                    {
                        case "de":
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_SelectLanguage, lang.Key), "Ausgewählte Sprache verwenden");
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_TellUserName, lang.Key), "Name mitteilen");
                            ausgabe.Add(new LanguageWord(NeededWords.helloUserMessage, lang.Key), "Hallo " + Const_String.userNamePlaceholder + "!");
                            ausgabe.Add(new LanguageWord(NeededWords.initialGreetingText, lang.Key), "Hallo! Ich heiße " + Const_String.robotNamePlaceholder + ". Und wie ist dein Name?");
                            ausgabe.Add(new LanguageWord(NeededWords.no, lang.Key), "Nein");
                            ausgabe.Add(new LanguageWord(NeededWords.questionForChatingAgain, lang.Key), "Möchten wir uns nochmal unterhalten?");
                            ausgabe.Add(new LanguageWord(NeededWords.yes, lang.Key), "Ja");
                            break;
                        case "en":
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_SelectLanguage, lang.Key), "Use selected language");
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_TellUserName, lang.Key), "Report name");
                            ausgabe.Add(new LanguageWord(NeededWords.helloUserMessage, lang.Key), "Hello " + Const_String.userNamePlaceholder + "!");
                            ausgabe.Add(new LanguageWord(NeededWords.initialGreetingText, lang.Key), "Hello! My name is " + Const_String.robotNamePlaceholder + ". And what is your name?");
                            ausgabe.Add(new LanguageWord(NeededWords.no, lang.Key), "No");
                            ausgabe.Add(new LanguageWord(NeededWords.questionForChatingAgain, lang.Key), "Are we going to chat again?");
                            ausgabe.Add(new LanguageWord(NeededWords.yes, lang.Key), "Yes");
                            break;
                        case "fr":
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_SelectLanguage, lang.Key), "Utilisez un langage choisi");
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_TellUserName, lang.Key), "Dites le nom");
                            ausgabe.Add(new LanguageWord(NeededWords.helloUserMessage, lang.Key), "Salut " + Const_String.userNamePlaceholder + "!");
                            ausgabe.Add(new LanguageWord(NeededWords.initialGreetingText, lang.Key), "Salut! Je m'appelle " + Const_String.robotNamePlaceholder + ".Et quel est votre nom?");
                            ausgabe.Add(new LanguageWord(NeededWords.no, lang.Key), "Non");
                            ausgabe.Add(new LanguageWord(NeededWords.questionForChatingAgain, lang.Key), "Allons-nous pour discuter à nouveau?");
                            ausgabe.Add(new LanguageWord(NeededWords.yes, lang.Key), "Oui");
                            break;
                        case "ru":
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_SelectLanguage, lang.Key), "Применить выбранный язык");
                            ausgabe.Add(new LanguageWord(NeededWords.actionExplanation_TellUserName, lang.Key), "Сообщить имя");
                            ausgabe.Add(new LanguageWord(NeededWords.helloUserMessage, lang.Key), "Привет " + Const_String.userNamePlaceholder + "!");
                            ausgabe.Add(new LanguageWord(NeededWords.initialGreetingText, lang.Key), "Привет! Меня зовут " + Const_String.robotNamePlaceholder + ". А тебя как зовут?");
                            ausgabe.Add(new LanguageWord(NeededWords.no, lang.Key), "Нет");
                            ausgabe.Add(new LanguageWord(NeededWords.questionForChatingAgain, lang.Key), "Будем ещё общаться?");
                            ausgabe.Add(new LanguageWord(NeededWords.yes, lang.Key), "Да");
                            break;
                        default:
                            throw new NotImplementedException();
                    }

            }

            return ausgabe;

        }
    }
}
