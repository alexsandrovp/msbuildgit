using Microsoft.Build.Framework;

namespace msbuild.git
{
    public class GitGetSHA : Git
    {
        /// <summary>
        /// What to get the SHA1 of.
        /// Defaults to HEAD. Can be a branch name.
        /// </summary>
        public string Branch { get; set; } = "HEAD";

        [Output]
        public string SHA1 { get; set; } = string.Empty;

        protected override string GenerateCommandLineCommands()
        {
            return string.Format("rev-parse \"{0}\"", Branch);
        }

        protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
        {
            if (!string.IsNullOrWhiteSpace(singleLine))
            {
                base.LogEventsFromTextOutput(singleLine, messageImportance);
                SHA1 = singleLine;
            }
        }
    }
}
