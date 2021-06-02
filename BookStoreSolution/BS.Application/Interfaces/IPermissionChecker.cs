namespace BS.Application.Interfaces
{
    public interface IPermissionChecker
    {
        bool HasClaim(string requiredClaim);
    }
}