namespace msbuild.git
{
    public class GitPush : Git
    {
        public bool Tags { get; set; } = false;
        public string Target { get; set; } = "origin";
        public string Branch { get; set; } = "HEAD";

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "push";
            if (Tags) cmd += " --tags";
            cmd += string.Format(" \"{0}\" \"{1}\"", Target, Branch);
            return cmd;
        }
    }
}
