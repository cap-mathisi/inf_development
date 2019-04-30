using sspx.core.entities;
using System;

namespace sspx.web.Helpers.ReleaseState
{
    public static class ReleaseStateCheckerFactory
    {
        public static IReleaseStateChecker GetChecker(ReleaseStateTypes releaseState)
        {
            switch (releaseState)
            {
                case ReleaseStateTypes.Deprecated:
                    // TODO CS2: 
                    // confirm with CAP that Deprecated is what they meant by "locked" per DR in https://trello.com/c/DPdb8aQX
                    return new DeprecatedReleaseStateChecker();
                case ReleaseStateTypes.Draft:
                    return new DraftReleaseStateChecker();
                case ReleaseStateTypes.ModelingInProgress:
                    return new ModelingReleaseStateChecker();
                case ReleaseStateTypes.NormalRelease:
                    return new NormalReleaseStateChecker();
                case ReleaseStateTypes.NULL:
                    return new NullReleaseStateChecker();
                case ReleaseStateTypes.CommunityTechnologyPreview1:
                case ReleaseStateTypes.CommunityTechnologyPreview2:
                case ReleaseStateTypes.CommunityTechnologyPreview3:
                case ReleaseStateTypes.CommunityTechnologyPreview4:
                case ReleaseStateTypes.CommunityTechnologyPreview5:
                case ReleaseStateTypes.CommunityTechnologyPreview6:
                case ReleaseStateTypes.CommunityTechnologyPreview7:
                case ReleaseStateTypes.CommunityTechnologyPreview8:
                case ReleaseStateTypes.CommunityTechnologyPreview9:
                case ReleaseStateTypes.CommunityTechnologyPreview10:
                case ReleaseStateTypes.ReleaseCandidate1:
                case ReleaseStateTypes.ReleaseCandidate2:
                case ReleaseStateTypes.ReleaseCandidate3:
                case ReleaseStateTypes.ReleaseCandidate4:
                case ReleaseStateTypes.ReleaseCandidate5:
                case ReleaseStateTypes.ReleaseCandidate6:
                case ReleaseStateTypes.ReleaseCandidate7:
                case ReleaseStateTypes.ReleaseCandidate8:
                case ReleaseStateTypes.ReleaseCandidate9:
                case ReleaseStateTypes.ReleaseCandidate10:
                    return new ReviewQA_ReleaseStateChecker();

                default:
                    throw new NotSupportedException();
            }

        }
    }
}
