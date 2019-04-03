using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration
{
    public interface IConfigurationServerProvider
    {
        IConfigurationServer Create(IConfiguration connectionConfiguration);
    }
}
