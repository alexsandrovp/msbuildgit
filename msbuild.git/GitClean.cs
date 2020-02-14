using Microsoft.Build.Framework;

namespace msbuild.git
{
    public class GitClean : Git
    {
        /// <summary>
        /// -d
        /// Remove untracked directories in addition to untracked files.
        /// If an untracked directory is managed by a different Git repository,
        /// it is not removed.
        /// </summary>
        public bool UntrackedDirectories { get; set; } = true;

        /// <summary>
        /// -f
        /// --force
        /// If the Git configuration variable clean.requireForce is not set to false,
        /// git clean will refuse to delete files or directories unless given -f, -n or -i.
        /// Git will refuse to delete directories with .git sub directory or file.
        /// </summary>
        public bool Force { get; set; } = true;

        /// <summary>
        /// -q
        /// --quiet
        /// Be quiet, only report errors, but not the files that are successfully removed.
        /// </summary>
        public bool Quiet { get; set; } = true;

        /// <summary>
        /// -x
        /// Don’t use the standard ignore rules read from .gitignore (per directory) and $GIT_DIR/info/exclude,
        /// but do still use the ignore rules given with -e options. This allows removing all untracked files,
        /// including build products. This can be used (possibly in conjunction with git reset)
        /// to create a pristine working directory to test a clean build.
        /// </summary>
        public bool EvenIgnoredEntries { get; set; } = true;

        /// <summary>
        /// -X
        /// Remove only files ignored by Git.
        /// This may be useful to rebuild everything from scratch, but keep manually created files.
        /// </summary>
        public bool OnlyIgnoredEntries { get; set; } = false;

        /// <summary>
        /// -e <pattern>
        /// --exclude=<pattern>
        /// In addition to those found in .gitignore (per directory) and $GIT_DIR/info/exclude,
        /// also consider these patterns to be in the set of the ignore rules in effect.
        /// </summary>
        public string Exclude { get; set; } = string.Empty;

        protected override bool ValidateParameters()
        {
            if (!base.ValidateParameters())
                return false;

            if (OnlyIgnoredEntries)
            {
                Log.LogMessage(MessageImportance.Normal, "Using option OnlyIgnoredEntries superceeds EvenIgnoredEntries");
                EvenIgnoredEntries = false;
            }

            return true;
        }

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "clean";
            if (Quiet) cmd += " --quiet";
            if (Force) cmd += " --force";
            if (UntrackedDirectories) cmd += " -d";
            if (OnlyIgnoredEntries) cmd += " -X";
            if (EvenIgnoredEntries) cmd += " -x";
            if (Exclude.Length > 0)
            {
                cmd += " --exclude=\"{0}\"";
                cmd = string.Format(cmd, Exclude);
            }

            return cmd;
        }
    }
}
