using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using System;

namespace JotunShard.Configuration
{
    public static class Extensions
    {
        public static IConfigurationBuilder AddFullPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource,
            Action<TFileConfigurationSource> provideFileSource)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideServerSource)
                .Add(provideFileSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddNetworkPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideServerSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddAgentPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TFileConfigurationSource> provideFileSource)
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideFileSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddUtilityPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder)
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });
    }
}