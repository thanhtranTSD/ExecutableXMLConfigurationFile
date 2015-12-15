
using CommandLine;
using CommandLine.Text;

namespace PostBuildEventer.Class
{
    public class CommandLineOption
    {
        [Option("f", "file", Required = true, HelpText = "Input the XML actions configuration file name.")]
        public string FileName { get; set; }

        [Option("o", "overwrite", DefaultValue = false, HelpText = "Overwrite if the files already exist in the destination folder. Default is False.")]
        public bool Overwrite { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            HelpText help = new HelpText
            {
                Heading = new HeadingInfo("PostBuild.exe", "Version 1.0"),
                Copyright = new CopyrightInfo("Virtium", 2015),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine("Usage:");
            help.AddOptions(this);
            return help;
        }
    }
}
