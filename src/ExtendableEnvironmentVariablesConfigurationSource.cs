using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace JotunShard.Configuration
{
    public class ExtendableEnvironmentVariablesConfigurationSource : IConfigurationSource
    {
        public string Prefix { get; set; }

        public IEnumerable<IEnvironmentSettingTranslator> Translators { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ExtendableEnvironmentVariablesConfigurationProvider(Prefix, Translators);
    }
}