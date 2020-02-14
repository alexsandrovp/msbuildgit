using Microsoft.Build.Framework;

namespace msbuild.git
{
    public class GitCheckout : Git
    {
        /// <summary>
        /// -f
        /// --force
        /// When switching branches, proceed even if the index or the working tree differs from HEAD. This is used to throw away local changes.
        /// When checking out paths from the index, do not fail upon unmerged entries; instead, unmerged entries are ignored.
        /// </summary>
        public bool Force { get; set; } = false;

        /// <summary>
        /// -q
        /// --quiet
        /// Quiet, suppress feedback messages.
        /// </summary>
        public bool Quiet { get; set; } = false;

        /// <summary>
        /// -t
        /// --track
        /// When creating a new branch, set up "upstream" configuration. See "--track" in git-branch(1) for details.
        /// If no -b option is given, the name of the new branch will be derived from the remote-tracking branch,
        /// by looking at the local part of the refspec configured for the corresponding remote,
        /// and then stripping the initial part up to the "*".
        /// This would tell us to use "hack" as the local branch when branching off of "origin/hack"
        /// (or "remotes/origin/hack", or even "refs/remotes/origin/hack").
        /// If the given name has no slash, or the above guessing results in an empty name,
        /// the guessing is aborted.You can explicitly give a name with -b in such a case.
        /// </summary>
        public bool Track { get; set; } = false;

        /// <summary>
        /// What to checkout. Can be a commit or a branch name.
        /// </summary>
        [Required]
        public string Branch { get; set; }

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "checkout";
            if (Quiet) cmd += " --quiet";
            if (Force) cmd += " --force";
            if (Track) cmd += " --track";
            cmd += string.Format(" \"{0}\"", Branch);
            return cmd;
        }
    }
}
