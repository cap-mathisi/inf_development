using sspx.core.entities;
using sspx.infra.data;
using System;
using System.Collections.Generic;

namespace sspx.infra.tests
{
    public class UserDataFixture : SSPDataFixture, IDisposable
    {
        public UserDataFixture()
        {
            Console.WriteLine("setting up test user data");
            Users = DBHelper.ExecuteQueryObjectWithMapper(SSPxTestConfig.SSPxConnectionString, _setupUserTestData, ObjectHelper.UserMapper);
        }

        public List<User> Users { get; }

        public new void Dispose()
        {
            Console.WriteLine("cleaning up test user data");
            DBHelper.ExecuteNonQuery(SSPxTestConfig.SSPxConnectionString, _cleanUserTestData);

            base.Dispose();
        }

        private static string _setupUserTestData = @"
	        INSERT INTO [dbo].[SSPx_Users] (
		         userid
                ,DefaultNamespaceKey
		        ,firstname
		        ,middlename
		        ,lastname
		        ,email
		        ,workphone
		        ,homephone
		        ,cellphone
		        ,UserTypeKey
		        ,qualifications
                ,VendorKey
                ,Specialties
	        )
	        VALUES (
		         'test_unit_1'
                ,1000043
		        ,'Unit Test'
		        ,'temporary'
		        ,'User One'
		        ,'test_unit_1@cap.org'
		        ,''
		        ,''
		        ,''
		        ,6
		        ,'Other'
                ,NULL
                ,'Breast Pathology, Cytopathology'
	        )

            INSERT INTO [dbo].[SSPx_Users] (
		         userid
                ,DefaultNamespaceKey
		        ,firstname
		        ,middlename
		        ,lastname
		        ,email
		        ,workphone
		        ,homephone
		        ,cellphone
		        ,UserTypeKey
		        ,qualifications
                ,VendorKey
                ,Specialties
	        )
	        VALUES (
		        'test_unit_2'
                ,1000043
		        ,'Unit Test'
		        ,'temporary'
		        ,'User Two'
		        ,'test_unit_2@cap.org'
		        ,''
		        ,''
		        ,''
		        ,2
		        ,'Other'
                ,NULL
                ,'Cytopathology'
	        )

            SELECT	u.[UsersKey], u.[UserID], u.[FirstName], u.[MiddleName], u.[LastName], u.[Email], u.[WorkPhone], u.[HomePhone],
		            u.[CellPhone], u.[UserTypeKey], COALESCE(u.[Qualifications], '') AS Qualifications, u.[VendorKey],
                    COALESCE(u.[Specialties], '') AS Specialties, u.[Active]
	        FROM [dbo].[SSPx_Users] u (NOLOCK)
	        WHERE u.Active = 1 AND u.UserID LIKE 'test_unit%'
            ORDER BY u.UserId
        ";

        // TODO CS2:
        // DELETE FROM[dbo].[SSP_UserAspNetUser]
        // WHERE UserCKey IN(SELECT UserCKey FROM[dbo].[SSP_User] u (NOLOCK) WHERE u.UserId LIKE 'test_unit%' )
        private static string _cleanUserTestData = @"
		    DELETE FROM [dbo].SSPx_Users
		    WHERE UserId LIKE 'test_unit%'
        ";
    }
}
