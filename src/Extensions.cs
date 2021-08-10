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
            Action<TFileConfigurationSource> provideFileSource,
            Func<ExtendableEnvironmentVariablesConfigurationSource> provideEnvironmentSource = null)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .Add(provideEnvironmentSource?.Invoke() ?? new ExtendableEnvironmentVariablesConfigurationSource())
                .Add(provideServerSource)
                .Add(provideFileSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddNetworkPipeline<TServerConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource,
            Func<ExtendableEnvironmentVariablesConfigurationSource> provideEnvironmentSource = null)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            => configurationBuilder
                .Add(provideEnvironmentSource?.Invoke() ?? new ExtendableEnvironmentVariablesConfigurationSource())
                .Add(provideServerSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddAgentPipeline<TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TFileConfigurationSource> provideFileSource,
            Func<ExtendableEnvironmentVariablesConfigurationSource> provideEnvironmentSource = null)
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .Add(provideEnvironmentSource?.Invoke() ?? new ExtendableEnvironmentVariablesConfigurationSource())
                .Add(provideFileSource)
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });

        public static IConfigurationBuilder AddUtilityPipeline(
            this IConfigurationBuilder configurationBuilder,
            Func<ExtendableEnvironmentVariablesConfigurationSource> provideEnvironmentSource = null)
            => configurationBuilder
                .Add(provideEnvironmentSource?.Invoke() ?? new ExtendableEnvironmentVariablesConfigurationSource())
                .Add<CommandLineConfigurationSource>(cmdSource =>
                {
                    cmdSource.Args = Environment.GetCommandLineArgs();
                });
    }
}