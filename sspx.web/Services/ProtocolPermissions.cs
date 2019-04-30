using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Helpers.ReleaseState;

namespace sspx.web.Services
{
    public class ProtocolPermissions : IProtocolPermissions
    {
        private IProtocolPermissionRepository _permissionRepository;
        private IProtocolVersionRepository _protocolVersionRepository;
        private IRoleRepository _roleRepository;
        private IConfiguration _temporaryConfig;

        public ProtocolPermissions(IProtocolVersionRepository protocolVersionRepository, IProtocolPermissionRepository permissionRepository, IRoleRepository roleRepository, IConfiguration temporaryConfig)
        {
            _permissionRepository = permissionRepository;
            _protocolVersionRepository = protocolVersionRepository;
            _roleRepository = roleRepository;
            _temporaryConfig = temporaryConfig;
        }

        public bool HasPermission(int userKey, int protocolKey, ProtocolPermissionTypes permission)
        {
            // TODO CS2:
            var permissionsBypass = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);
            if (permissionsBypass)
            {
                return true;
            }

            var latestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(protocolKey);
            if(latestProtocolVersion == null)
            {
                return false;
            }

            var globalReleaseState = latestProtocolVersion.ReleaseStatesKey;

            var roleEntities = _roleRepository.ListForUserProtocol(userKey, protocolKey);
            var roles = roleEntities.Select(r => Role.ToRoleTypes(r)).ToList();

            IReleaseStateChecker releaseStateChecker = ReleaseStateCheckerFactory.GetChecker(globalReleaseState);
            if(releaseStateChecker.PermissionIsAllowed(roles, permission) == false)
            {
                return false;
            }

            // Normal roles/permissions apply if we made it past the Global Release State check
            var permissionsAvailableToUser = _permissionRepository.ListForUserProtocol(userKey, protocolKey);
            foreach (var permissionAvailableToUser in permissionsAvailableToUser)
            {
                if(permission == ProtocolPermission.ToProtocolPermissionType(permissionAvailableToUser))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
