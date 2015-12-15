
using CommandLine;
using PostBuildEventer.Action;
using PostBuildEventer.Class;
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
            CommandLineOption options = new CommandLineOption();

            ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings
            {
                MutuallyExclusive = true,
                CaseSensitive = true,
                HelpWriter = Console.Error
            });

            bool success = parser.ParseArguments(args, options);
            if (true == success)
            {
                Run(options.FileName, options.Overwrite);
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

        private static void Run(string configFileName, bool overwrite = false)
        {
            if (false == File.Exists(configFileName))
            {
                NotifyAndExit(String.Format("{0} does not exist. Press any key to exit.", configFileName));
            }
            Initialze();

            XmlDocument xmlContent = XMLManager.LoadXmlFile(configFileName);
            ActionManager.ExecuteAllAction(xmlContent, overwrite);
            Console.ReadLine();
        }
        #endregion
    }
}
