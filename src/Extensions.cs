using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using System;

namespace JotunShard.Configuration
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddFullPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,//AppDomain
            Action<TServerConfigurationSource> provideServerSource,
            Action<TFileConfigurationSource> provideFileSource,
            Action<CommandLineConfigurationSource> configureCommandLineSource)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideServerSource)
                .Add(provideFileSource)
                .Add(configureCommandLineSource);

        public static IConfigurationBuilder AddNetworkPipeline<TServerConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource,
            Action<CommandLineConfigurationSource> configureCommandLineSource)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideServerSource)
                .Add(configureCommandLineSource);

        public static IConfigurationBuilder AddAgentPipeline<TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TFileConfigurationSource> provideFileSource,
            Action<CommandLineConfigurationSource> configureCommandLineSource)
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideFileSource)
                .Add(configureCommandLineSource);

        public static IConfigurationBuilder AddUtilityPipeline(
            this IConfigurationBuilder configurationBuilder,
            Action<CommandLineConfigurationSource> configureCommandLineSource)
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(configureCommandLineSource);

        public static IConfigurationBuilder Add<TSource>(
            this IConfigurationBuilder builder,
            Action<TSource> configureSource)
            where TSource : IConfigurationSource, new()
        {
            var source = new TSource();
            configureSource?.Invoke(source);
            return builder.Add(source);
        }
    }
}