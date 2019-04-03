using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration
{
    public abstract class ServerConfigurationSource : IConfigurationSource
    {
        public abstract IConfigurationServerProvider ServerProvider { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ServerConfigurationProvider(ServerProvider, builder.Build());
    }
}