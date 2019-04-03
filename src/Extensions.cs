using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using System;

namespace JotunShard.Configuration
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddDefaultPipeline(
            this IConfigurationBuilder configurationBuilder,
            Func<ServerConfigurationSource> provideServerSource = null,
            Func<FileConfigurationSource> provideFileSource = null,
            Action<CommandLineConfigurationSource> configureCommandLineSource = null)
            => configurationBuilder
                .AddEnvironmentVariables()
                .AddIfNeeded(provideServerSource)
                .AddIfNeeded(provideFileSource)
                .AddIfNeeded(configureCommandLineSource);

        public static IConfigurationBuilder AddIfNeeded<TConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TConfigurationSource> configure)
            where TConfigurationSource : IConfigurationSource, new()
            => configure != null
                ? configurationBuilder.Add(configure)
                : configurationBuilder;

        public static IConfigurationBuilder AddIfNeeded<TConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Func<TConfigurationSource> provide)
            where TConfigurationSource : IConfigurationSource
            => provide != null
                ? configurationBuilder.Add(provide())
                : configurationBuilder;
    }
}