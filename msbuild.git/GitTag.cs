using Microsoft.Build.Framework;

namespace msbuild.git
{
    public class GitTag : Git
    {
        /// <summary>
        /// -f
        /// --force
        /// Replace an existing tag with the given name(instead of failing)
        /// </summary>
        public bool Force { get; set; } = false;

        /// <summary>
        /// -d
        /// --delete
        /// Delete existing tags with the given names.
        /// </summary>
        public bool Delete { get; set; } = false;

        /// <summary>
        /// -a
        /// --annotate
        /// Make an unsigned, annotated tag object
        /// Annotated tags are meant for release while lightweight tags are meant for private or temporary object labels.
        /// For this reason, some git commands for naming objects (like git describe) will ignore lightweight tags by default.
        /// </summary>
        public bool Annotate { get; set; } = true;

        /// <summary>
        /// Tag string to set
        /// </summary>
        [Required]
        public string Tag { get; set; }

        /// <summary>
        /// Optional commit message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Optional commit/object to tag.
        /// Git tags the workingdir's parent revision if not specified
        /// </summary>
        public string Commit { get; set; } = string.Empty;

        protected override bool ValidateParameters()
        {
            if (!base.ValidateParameters())
                return false;

            if (Delete)
            {
                Annotate = false;
                Message = string.Empty;
                Commit = string.Empty;
            }

            return true;
        }

        protected override string GenerateCommandLineCommands()
        {
            string cmd = "tag";
            if (Force) cmd += " --force";
            if (Delete) cmd += " --delete";
            if (Annotate)
            {
                cmd += " --annotate";
                if (Message.Length == 0)
                {
                    Message = "Tag added automatically by msbuild";
                    cmd += string.Format(" --message \"{0}\"", Message);
                }
            }
            cmd += string.Format(" \"{0}\"", Tag);
            if (Commit.Length > 0)
                cmd += string.Format(" \"{0}\"", Commit);

            return cmd;
        }
    }
}
