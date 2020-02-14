namespace msbuild.git
{
    public class GitPush : Git
    {
        public bool Tags { get; set; }
        public string Target { get; set; } = "origin";
        public string Commit { get; set; } = "HEAD";

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "push";
            if (Tags) cmd += " --tags";
            cmd += string.Format(" \"{0}\" \"{1}\"", Target, Commit);
            return cmd;
        }
    }
}
