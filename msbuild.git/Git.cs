using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System.Text.RegularExpressions;

namespace msbuild.git
{
    public class Git : ToolTask
    {
        public string GitHome { get; set; } = "";
        [Required]
        public string Repository { get; set; }

        protected override string ToolName => "git.exe";

        protected override string GenerateFullPathToTool()
        {
            return Path.Combine(GitHome, ToolName);
        }

        protected override string GetWorkingDirectory()
        {
            return Repository;
        }

        protected override bool ValidateParameters()
        {
            if (!base.ValidateParameters())
                return false;

            if (GitHome.Length > 0)
            {
                var githome = new DirectoryInfo(GitHome);
                if (!githome.Exists)
                {
                    Log.LogError("GitHome does not exist: {0}", GitHome);
                    return false;
                }
                GitHome = githome.FullName;
            }

            var repo = new DirectoryInfo(Repository);
            if (!repo.Exists)
            {
                Log.LogError("Repository does not exist: {0}", Repository);
                return false;
            }

            Repository = repo.FullName;
            Log.LogMessage(MessageImportance.Low, "Running git on repo: " + Repository);

            return true;
        }

        private static Regex urlpwdRgx = new Regex(@"://.*?@");
        private static string urlpwdReplace = "://********@";

        protected override void LogToolCommand(string message)
        {
            //remove sensitive information (passwords) from the command print (tool call)
            message = urlpwdRgx.Replace(message, urlpwdReplace);
            Log.LogMessage(MessageImportance.High, "{0} >> {1}", Repository, message);
        }

        protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
        {
            //remove sensitive information (passwords) from the command print (tool output)
            //or make other decisions with it, like printing errors in red

            if (singleLine == null) singleLine = "";
            singleLine = urlpwdRgx.Replace(singleLine, urlpwdReplace);

            if (singleLine.StartsWith("fatal:")) Log.LogError(singleLine);
            else if (singleLine.StartsWith("git:")) Log.LogWarning(singleLine);
            else Log.LogMessage(MessageImportance.Normal, singleLine);
        }
    }
}
