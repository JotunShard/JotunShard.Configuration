using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using System;

namespace JotunShard.Configuration
{
    public static class Extensions
    {
#if NETSTANDARD1_3

        public static IConfigurationBuilder AddDefaultPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource = null,
            Action<TFileConfigurationSource> provideFileSource = null,
            Action<CommandLineConfigurationSource> configureCommandLineSource = null)
            where TServerConfigurationSource : ServerConfigurationSource, new()
            where TFileConfigurationSource : FileConfigurationSource, new()
            => configurationBuilder
                .AddEnvironmentVariables()
                .Add(provideServerSource)
                .Add(provideFileSource)
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

#endif

#if NETSTANDARD2_0

        public static IConfigurationBuilder AddDefaultPipeline<TServerConfigurationSource, TFileConfigurationSource>(
            this IConfigurationBuilder configurationBuilder,
            Action<TServerConfigurationSource> provideServerSource = null,
            Action<TFileConfigurationSource> provideFileSource = null)
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

#endif
    }
}