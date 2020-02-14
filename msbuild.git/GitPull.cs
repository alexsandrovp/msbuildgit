namespace msbuild.git
{
    public class GitPull : Git
    {
        /// <summary>
        /// -f
        /// --force
        /// When git fetch is used with <rbranch>:<lbranch> refspec,
        /// it refuses to update the local branch <lbranch> unless the remote branch <rbranch> it fetches is a descendant of <lbranch>.
        /// This option overrides that check.
        /// </summary>
        public bool Force { get; set; } = false;

        /// <summary>
        /// -q
        /// --quiet
        /// This is passed to both underlying git-fetch to squelch reporting of during transfer, and underlying git-merge to squelch output during merging.
        /// </summary>
        public bool Quiet { get; set; } = false;

        /// <summary>
        /// -v
        /// --verbose
        /// Pass --verbose to git-fetch and git-merge.
        /// </summary>
        public bool Verbose { get; set; } = false;

        /// <summary>
        /// -r
        /// --rebase[= false | true | preserve | interactive]
        /// When true, rebase the current branch on top of the upstream branch after fetching.
        /// If there is a remote-tracking branch corresponding to the upstream branch and the upstream branch was rebased since last fetched,
        /// the rebase uses that information to avoid rebasing non-local changes.
        /// When set to preserve, rebase with the --preserve-merges option passed to git rebase so that locally created merge commits will not be flattened.
        /// When false, merge the current branch into the upstream branch.
        /// When interactive, enable the interactive mode of rebase.
        /// See pull.rebase, branch.<name>.rebase and branch.autoSetupRebase in git-config(1) if you want to make git pull always use --rebase instead of merging.
        /// Note
        /// This is a potentially dangerous mode of operation.
        /// It rewrites history, which does not bode well when you published that history already.Do not use this option unless you have read git-rebase(1) carefully.
        /// </summary>
        public bool Rebase { get; set; } = false;

        /// <summary>
        /// Where to pull from.
        /// "origin" by default
        /// </summary>
        public string Origin { get; set; } = "origin";

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "pull";
            if (Quiet) cmd += " --quiet";
            if (Force) cmd += " --force";
            if (Verbose) cmd += " --verbose";
            if (Rebase) cmd += " --rebase";
            if (Origin.Length > 0) cmd += string.Format(" \"{0}\"", Origin);
            return cmd;
        }
    }
}
