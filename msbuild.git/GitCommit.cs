namespace msbuild.git
{
    public class GitCommit : Git
    {
        /// <summary>
        /// Optional commit message
        /// </summary>
        public string Message { get; set; } = "Auto-commit from build job";

        protected override string GenerateCommandLineCommands()
        {
            return string.Format("commit --message \"{0}\"", Message);
        }
    }
}
