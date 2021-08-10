using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace JotunShard.Configuration
{
    public class ExtendableEnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        private readonly string _prefix;
        private readonly ImmutableList<IEnvironmentSettingTranslator> _translators;

        public ExtendableEnvironmentVariablesConfigurationProvider(
            string prefix = null,
            IEnumerable<IEnvironmentSettingTranslator> translators = null)
        {
            _prefix = prefix ?? string.Empty;
            _translators = translators?.ToImmutableList()
                           ?? ImmutableList<IEnvironmentSettingTranslator>.Empty;
        }

        public override void Load() => Data = Environment.GetEnvironmentVariables()
            .Cast<DictionaryEntry>()
            .SelectMany(ConvertToAppSetting)
            .ToDictionary(
                appEntry => (string)appEntry.Key,
                appEntry => (string)appEntry.Value,
                StringComparer.OrdinalIgnoreCase);

        private IEnumerable<DictionaryEntry> ConvertToAppSetting(DictionaryEntry entry)
        {
            var key = (string)entry.Key;
            if (key.StartsWith(_prefix))
            {
                return new[]
                {
                    new DictionaryEntry(key.Substring(_prefix.Length), entry.Value)
                };
            }
            return _translators.Where(configurator => configurator.AcceptsKey(key))
                .SelectMany(configurator => configurator.Translate(entry));
        }
    }
}