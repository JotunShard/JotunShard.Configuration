namespace JotunShard.Configuration.Consul
{
    public class ConsulConfigurationSource : ServerConfigurationSource
    {
        public override IConfigurationServerProvider ServerProvider { get; }
            = new ConsulConfigurationServerProvider();

        public ConsulConfigurationSource() { }
    }
}