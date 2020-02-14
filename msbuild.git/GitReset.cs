namespace msbuild.git
{
    public class GitReset : Git
    {
        public string Mode { get; set; } = "hard";
        public string Commit { get; set; } = string.Empty;

        /// <summary>
        /// -q
        /// --quiet
        /// Be quiet, only report errors.
        /// </summary>
        public bool Quiet { get; set; } = true;

        protected override bool ValidateParameters()
        {
            if (!base.ValidateParameters())
                return false;

            if (Mode == null) Mode = string.Empty;
            Mode = Mode.ToLower();

            switch (Mode)
            {
                case "soft":
                case "mixed":
                case "hard":
                case "merge":
                case "keep":
                    break;
                default:
                    Log.LogError("wrong mode for git reset: {0}", Mode);
                    return false;
            }

            return true;
        }

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "reset --" + Mode;
            if (Quiet) cmd += " --quiet";
            if (Commit.Length > 0)
                cmd += string.Format(" \"{0}\"", Commit);

            return cmd;
        }
    }
}
