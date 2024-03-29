﻿using System.Collections;
using System.Collections.Generic;

namespace JotunShard.Configuration
{
    public interface IEnvironmentSettingTranslator
    {
        bool AcceptsKey(string key);

        IEnumerable<DictionaryEntry> Translate(DictionaryEntry entry);
    }

    public sealed class DefaultEnvironmentSettingTranslator : IEnvironmentSettingTranslator
    {
        public static IEnvironmentSettingTranslator Default { get; } = new DefaultEnvironmentSettingTranslator();

        public bool AcceptsKey(string key) => true;

        public IEnumerable<DictionaryEntry> Translate(DictionaryEntry entry)
        {
            yield return entry;
        }
    }
}