using ApiWithAuthentication.Constants;

namespace ApiWithAuthentication.Configurations
{
    public class JwtConfiguration
    {
        public string JwtKey { get; }
        public int LifeTimeInSeconds { get; }

        public JwtConfiguration(IConfiguration configuration)
        {
            JwtKey = configuration[ConfigurationConstants.JwtKey] ?? throw new ArgumentNullException(nameof(ConfigurationConstants.JwtKey));
            LifeTimeInSeconds = int.Parse(configuration[ConfigurationConstants.LifeTimeInSeconds]);
        }
    }
}
