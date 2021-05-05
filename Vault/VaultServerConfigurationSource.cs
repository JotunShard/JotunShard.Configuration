namespace JotunShard.Configuration.Vault
{
    public class VaultServerConfigurationSource : ServerConfigurationSource
    {
        public override IConfigurationServerProvider ServerProvider { get; }
            = new VaultServerConfigurationServerProvider();

        public VaultServerConfigurationSource() { }
    }
}