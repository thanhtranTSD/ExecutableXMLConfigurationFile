using PostBuildEventer.Action;
using PostBuildEventer.Factories;
using PostBuildEventer.XML;
using System;
using System.IO;
using System.Xml;

namespace PostBuildEventer
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    {
                        NotifyAndExit("Please input Configuration file in argument. Press any key to exit.");
                        break;
                    }
                case 1:
                    {
                        string fileName = args[0].ToString();
                        Run(fileName);
                        break;
                    }
                default:
                    {
                        NotifyAndExit("There are too many arguments. Press any key to exit.");
                        break;
                    }
            }
        }
        #region Private Functions
        private static void Initialze()
        {
            ActionFactory.Instance().Initialize();
        }

        private static void NotifyAndExit(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static void Run(string configFileName)
        {
            if (false == File.Exists(configFileName))
            {
                NotifyAndExit(String.Format("{0} does not exist. Press any key to exit.", configFileName));
            }
            Initialze();

            XmlDocument xmlContent = XMLManager.LoadXmlFile(configFileName);
            ActionManager.ExecuteAllAction(xmlContent, true);
            Console.ReadLine();
        }
        #endregion
    }
}
