using Robust.Shared.Configuration;

namespace Content.Shared._Gabystation.CCVar;

[CVarDefs]
public sealed partial class GabyCVars
{
        /// <summary>
        /// Enables alternate job titles for players.
        /// </summary>
        public static readonly CVarDef<bool> ICAlternateJobTitlesEnable =
            CVarDef.Create("ic.alternate_job_titles_enable", true, CVar.SERVER | CVar.REPLICATED);

}
