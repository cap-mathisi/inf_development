namespace sspx.web.Services
{
    public interface IPagePermissions
    {
        bool HasPermission(int userKey, string path);
    }
}
