namespace msbuild.git
{
    public class GitCommit : Git
    {
        /// <summary>
        /// Optional commit message
        /// </summary>
        public string Message { get; set; } = "Auto-commit from msbuild";

        protected override string GenerateCommandLineCommands()
        {
            return string.Format("commit --message \"{0}\"", Message);
        }
    }
}
