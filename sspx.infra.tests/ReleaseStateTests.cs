using sspx.core.entities;
using sspx.web.Helpers.ReleaseState;
using System.Collections.Generic;
using Xunit;

namespace sspx.infra.tests
{
    public class ReleaseStateTests
    {
        private static readonly List<RoleTypes> authorRole = new List<RoleTypes> { RoleTypes.Author };
        private static readonly List<RoleTypes> adminRole = new List<RoleTypes> { RoleTypes.Admin };
        private static readonly List<RoleTypes> editorRole = new List<RoleTypes> { RoleTypes.Editor };
        private static readonly List<RoleTypes> editorReviewerRoles = new List<RoleTypes> { RoleTypes.Editor, RoleTypes.Reviewer };
        private static readonly List<RoleTypes> modelerRole = new List<RoleTypes> { RoleTypes.Modeler };
        private static readonly List<RoleTypes> reviewerRole = new List<RoleTypes> { RoleTypes.Reviewer };

        [Fact]
        public void GivenDraftState_AuthorCanEdit()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Draft);
            var actual = checker.PermissionIsAllowed(authorRole, ProtocolPermissionTypes.EditAll);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDraftState_AdminCanView()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Draft);
            var actual = checker.PermissionIsAllowed(adminRole, ProtocolPermissionTypes.View);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDraftState_ModelerShouldNotView()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Draft);
            var actual = checker.PermissionIsAllowed(modelerRole, ProtocolPermissionTypes.View);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenModelingState_ModelerCanEditProtocol()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ModelingInProgress);
            var actual = checker.PermissionIsAllowed(modelerRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenModelingState_ReviewerShouldNotEditProtocol()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ModelingInProgress);
            var actual = checker.PermissionIsAllowed(reviewerRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenModelingState_AuthorShouldNotEditProtocol()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ModelingInProgress);
            var actual = checker.PermissionIsAllowed(authorRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDeprecatedState_AdminCanStillEditProtocol()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Deprecated);
            var actual = checker.PermissionIsAllowed(adminRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDeprecatedState_ReviewerCanStillView()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Deprecated);
            var actual = checker.PermissionIsAllowed(reviewerRole, ProtocolPermissionTypes.View);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDeprecatedState_EditorShouldNotComment()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.Deprecated);
            var actual = checker.PermissionIsAllowed(editorRole, ProtocolPermissionTypes.CreateEditViewComments);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNormalReleaseState_EditorOrReviewerShouldNotEditAll()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.NormalRelease);
            var actual = checker.PermissionIsAllowed(editorReviewerRoles, ProtocolPermissionTypes.EditAll);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNormalReleaseState_AdminCanStillEditProtocol()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.NormalRelease);
            var actual = checker.PermissionIsAllowed(adminRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNormalReleaseState_ModelerCanStillComment()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.NormalRelease);
            var actual = checker.PermissionIsAllowed(modelerRole, ProtocolPermissionTypes.CreateEditViewComments);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenReleaseCandidate3_ReleaseState_ModelerCanStillComment()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ReleaseCandidate3);
            var actual = checker.PermissionIsAllowed(modelerRole, ProtocolPermissionTypes.CreateEditViewComments);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenReleaseCandidate10_ReleaseState_AuthorCanStillEdit()
        {
            var expected = true;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ReleaseCandidate10);
            var actual = checker.PermissionIsAllowed(authorRole, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenCommunityTechnologyPreview6_ReleaseState_EditorShouldNotComment()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.CommunityTechnologyPreview6);
            var actual = checker.PermissionIsAllowed(editorRole, ProtocolPermissionTypes.CreateEditViewComments);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenReleaseCandidate3_ReleaseState_EditorOrReviewerShouldNotEdit()
        {
            var expected = false;

            IReleaseStateChecker checker = ReleaseStateCheckerFactory.GetChecker(ReleaseStateTypes.ReleaseCandidate3);
            var actual = checker.PermissionIsAllowed(editorReviewerRoles, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        // TODO CS2:
    }
}
