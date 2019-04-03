using System.Collections.Generic;

namespace JotunShard.Configuration
{
    public interface IConfigurationServer
    {
        IEnumerable<KeyValuePair<string, string>> QuerySettings();
    }
}