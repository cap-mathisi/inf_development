using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.data;
using sspx.web.Models;
using sspx.web.Services;

namespace sspx.web.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizedUsers(this IServiceCollection services, IConfiguration configuration)
        {
            var useFakeUsers = InMemoryDataToggle.UseInMemory(configuration["SSPX_USE_FAKE_DATA"], configuration["SSPX_LOGIN_FAKE_USERS"]);
            if (useFakeUsers == true)
            {
                services.AddDefaultIdentity<IdentitySSPxUser>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                    .AddPasswordValidator<UsernameInPasswordValidator<IdentitySSPxUser>>()
                    .AddPasswordValidator<NoAsteriskPasswordValidator<IdentitySSPxUser>>();

                // Singleton should only be used for dev/test environment -- using it so we can do CRUD with in-memory data
                services.AddSingleton<IUserStore<IdentitySSPxUser>, InMemoryUsersIdentity>();
                services.AddSingleton<ISSPxUserRepository, InMemoryUsersSSPxRepository>();
                services.AddSingleton<IAdminPermissions, InMemoryAdminPermissions>();
                services.AddSingleton<IProtocolPermissionRepository, InMemoryProtocolPermissionRepository>();
                services.AddSingleton<IRoleRepository, InMemoryRoleRepository>();
                services.AddSingleton<ISpecialtyRepository, InMemorySpecialtyRepository>();
                services.AddSingleton<IUserTypeRepository, InMemoryUserTypeRepository>();
                services.AddSingleton<IVendorRepository, InMemoryVendorRepository>();
            }
            else
            {
                services.AddDefaultIdentity<IdentitySSPxUser>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddPasswordValidator<UsernameInPasswordValidator<IdentitySSPxUser>>()
                    .AddPasswordValidator<NoAsteriskPasswordValidator<IdentitySSPxUser>>();

                services.AddScoped<ISSPxUserRepository, UserSSPxRepository>();
                services.AddScoped<IAdminPermissions, AdminPermissions>();
                services.AddScoped<IProtocolPermissionRepository, ProtocolPermissionRepository>();
                services.AddScoped<IRoleRepository, RoleRepository>();
                services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
                services.AddScoped<IUserTypeRepository, UserTypeRepository>();
                services.AddScoped<IVendorRepository, VendorRepository>();
            }

            // TODO CS2:
            services.AddSingleton<IPagePermissions, InMemoryPagePermissions>();

            return services;
        }

        public static IServiceCollection AddSSPxAdminSearchData(this IServiceCollection services, IConfiguration configuration)
        {
            var useFakeData = InMemoryDataToggle.UseInMemory(configuration["SSPX_USE_FAKE_DATA"], configuration["SSPX_ADMIN_SEARCH_FAKE_DATA"]);
            if (useFakeData == true)
            {
                // Singleton should only be used for dev/test environment -- using it so we can do CRUD with in-memory data
                services.AddSingleton<IAdminRepository<Protocol>, InMemoryProtocolRepository>();
                services.AddSingleton<IProtocolWithGroupRepository, InMemoryProtocolWithGroupRepository>();
                services.AddSingleton<IProtocolGroupRepository, InMemoryProtocolGroupRepository>();
                services.AddSingleton<IProtocolVersionRepository, InMemoryProtocolVersionRepository>();
                services.AddSingleton<IQualificationRepository, InMemoryQualificationRepository>();
                services.AddSingleton<IAdminRepository<Standard>, InMemoryStandardRepository>();
            }
            else
            {
                services.AddScoped<IAdminRepository<Protocol>, ProtocolRepository>();
                services.AddScoped<IProtocolWithGroupRepository, ProtocolWithGroupRepository>();
                services.AddScoped<IProtocolGroupRepository, ProtocolGroupRepository>();
                services.AddScoped<IProtocolVersionRepository, ProtocolVersionRepository>();
                services.AddScoped<IQualificationRepository, QualificationRepository>();
                services.AddScoped<IAdminRepository<Standard>, StandardRepository>();
            }

            return services;
        }

        public static IServiceCollection AddSSPxNavMenuData(this IServiceCollection services, IConfiguration configuration)
        {
            var useFakeNavMenuData = InMemoryDataToggle.UseInMemory(configuration["SSPX_USE_FAKE_DATA"], configuration["SSPX_NAVMENU_FAKE_DATA"]);
            if (useFakeNavMenuData == true)
            {
                services.AddScoped<INavMenuData, InMemoryNavMenuData>();
            }
            else
            {
                services.AddScoped<INavMenuData, NavMenuData>();
            }

            return services;
        }

        public static IServiceCollection AddSSPxProtocolData(this IServiceCollection services, IConfiguration configuration)
        {
            var useFakeProtocolData = InMemoryDataToggle.UseInMemory(configuration["SSPX_USE_FAKE_DATA"], configuration["SSPX_PROTOCOL_FAKE_DATA"]);
            if (useFakeProtocolData == true)
            {
                // Singleton should only be used for dev/test environment
                services.AddSingleton<IProtocolIndexData, InMemoryProtocolIndexData>();
            }
            else
            {
                services.AddScoped<IProtocolIndexData, ProtocolIndexData>();
            }

            return services;
        }
    }
}
