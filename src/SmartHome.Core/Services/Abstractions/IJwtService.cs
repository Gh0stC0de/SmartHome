namespace SmartHome.Core.Services.Abstractions
{
    /// <summary>
    ///     Service for JASON web token.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        ///     Gets a new JASON web token.
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>JASON web token as string</returns>
        string GetNewToken(string userId);
    }
}