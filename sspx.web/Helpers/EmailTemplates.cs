
/*--
 *    Userid    User story  Description
 * 1. mathisi   SSP-38      Email notifcation for Assigned reviewer in workflow 
 --*/
namespace sspx.web.Helpers
{
    // These emails should be limited to "simple" HTML so they will be...
    //     1) friendly for email clients -- https://hackernoon.com/how-to-build-a-dynamic-email-template-in-under-10-minutes-1fe85ad40e35
    //     2) readable as plain text if all the HTML is removed
    public static class EmailTemplates
    {
        public const string ACCOUNT_CONFIRMATION = @"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body>
                    <img src='https://cap.objects.frb.io/images/social/cap-default-social-image-facebook.jpg' alt='CAP logo' width='350' height='184'>
                    <hr />
                    <p>
                        Dear [FIRST_NAME],
                    </p>
                    <p>
                        This confirmation contains information required for you to access your SSP User account.
                    </p>
                    <p>
                        Please verify your email address by clicking this link:<br />
                        <a href='[EMAIL_CONFIRMATION_LINK]'>
                            [EMAIL_CONFIRMATION_LINK]
                        </a>
                    </p>
                    <p>
                        Then you may log in as User ID: <strong>[USER_ID]</strong>
                    </p>
                    <p>
                        If you need further assistance, please contact at <a href='mailto:capecc@cap.org'>capecc@cap.org</a> or call 847-832-7700.
                    </p>
                    <p>
                        Please do not reply to this email; it is generated from an unattended messaging service.
                    </p>
                    <p>
                        Thank you.
                    </p>
                    <table style='color:#FFFFFF;background-color:#2D2D2D;' cellpadding='10' cellspacing='0' border='0' width='100%'>
                        <tr>
                            <td valign='top'>
                            <p>
                                College of American Pathologists
                            </p>
                            <p>
                                325 Waukegan Road<br />
                                Northfield, IL 60093<br />
                                800-323-4040 | 001 847-832-7000
                            </p>
                            </td>
                            <td align='right' valign='bottom'>
                                &copy; College of American Pathologists. All rights reserved.
                            </td>
                        </tr>
                    </table>
                </body>
            </html>
        ";

        public const string USER_REQUESTED_ONLINE_ACCESS = @"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body>
                    <p>
                        [FIRST_NAME] [LAST_NAME] has requested online access for UserID <strong>[USER_ID]</strong>.
                    </p>
                    <p>
                        Please visit the [APPROVAL_LINK] if you would like to approve their account.
                    </p>
                    <table style='color:#FFFFFF;background-color:#2D2D2D;' cellpadding='10' cellspacing='0' border='0' width='100%'>
                        <tr>
                            <td valign='top'>
                            <p>
                                College of American Pathologists
                            </p>
                            <p>
                                325 Waukegan Road<br />
                                Northfield, IL 60093<br />
                                800-323-4040 | 001 847-832-7000
                            </p>
                            </td>
                            <td align='right' valign='bottom'>
                                &copy; College of American Pathologists. All rights reserved.
                            </td>
                        </tr>
                    </table>
                </body>
            </html>
        ";

        public const string RESET_PASSWORD = @"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body>
                    <img src='https://cap.objects.frb.io/images/social/cap-default-social-image-facebook.jpg' alt='CAP logo' width='350' height='184'>
                    <hr />
                    <p>
                        Please click here to reset your password:
                    </p>
                    <p>
                        <a href='[PASSWORD_RESET_URL]'>
                            [PASSWORD_RESET_URL]
                        </a>
                    </p>
                    <p>
                        If you need further assistance, please contact at <a href='mailto:capecc@cap.org'>capecc@cap.org</a> or call 847-832-7700.
                    </p>
                    <p>
                        Please do not reply to this email; it is generated from an unattended messaging service.
                    </p>
                    <p>
                        Thank you.
                    </p>
                    <table style='color:#FFFFFF;background-color:#2D2D2D;' cellpadding='10' cellspacing='0' border='0' width='100%'>
                        <tr>
                            <td valign='top'>
                            <p>
                                College of American Pathologists
                            </p>
                            <p>
                                325 Waukegan Road<br />
                                Northfield, IL 60093<br />
                                800-323-4040 | 001 847-832-7000
                            </p>
                            </td>
                            <td align='right' valign='bottom'>
                                &copy; College of American Pathologists. All rights reserved.
                            </td>
                        </tr>
                    </table>
                </body>
            </html>
        ";
        /*--Email notifcation for Assigned reviewer in workflow--*/
        //Start - SSP-38
        public const string ASSIGN_REVIEWER = @"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body>
                    <img src='https://cap.objects.frb.io/images/social/cap-default-social-image-facebook.jpg' alt='CAP logo' width='350' height='184'>
                    <hr />
                    <p>
                        Dear [FIRST_NAME],
                    </p>
                    <p>
                        I would appreciate it  very much if you would review the [Protocol_Name] protocol written by [Primary_Author_Name] and complete your comments by [EndDate].
                    </p>
                    <p>
                        In reviewing this version, please consider the following:<br />
                        [Custome_Message]<br/>
                        <a href='[Access_Protocol]'>
                            [Access Protocol]
                        </a>
                    </p>
                    <p>
                    </p>
                    <p>
                        If you feel that you cannot complete a timely review of this version, please contact [Author_Email] so that I may arrange other assistance with this review. Thank You.
                    </p>
                    <p>
			Sincerely,</br>
			[Author_Name]
                    </p>							
                    <p>
                        If you need further assistance, please contact at <a href='mailto:capecc@cap.org'>capecc@cap.org</a> or call 847-832-7700.
                    </p>
                    <p>
                        Please do not reply to this email; it is generated from an unattended messaging service.
                    </p>
                    <p>
                        Thank you.
                    </p>
                    <table style='color:#FFFFFF;background-color:#2D2D2D;' cellpadding='10' cellspacing='0' border='0' width='100%'>
                        <tr>
                            <td valign='top'>
                            <p>
                                College of American Pathologists
                            </p>
                            <p>
                                325 Waukegan Road<br />
                                Northfield, IL 60093<br />
                                800-323-4040 | 001 847-832-7000
                            </p>
                            </td>
                            <td align='right' valign='bottom'>
                                &copy; College of American Pathologists. All rights reserved.
                            </td>
                        </tr>
                    </table>
                </body>
            </html>
        ";
        //End - SSP-38
    }
}
