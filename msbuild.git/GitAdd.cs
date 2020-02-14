namespace msbuild.git
{
    public class GitAdd : Git
    {
        /// <summary>
        /// -f
        /// --force
        /// Allow adding otherwise ignored files.
        /// </summary>
        public bool Force { get; set; } = false;

        public string Path { get; set; } = ".";

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "add";
            if (Force) cmd += " --force";
            cmd += string.Format(" -- \"{0}\"", Path);
            return cmd;
        }
    }
}
