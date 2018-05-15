// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using HD.Configuration.Abstractions;
using HD.Configuration.FileExtensions;
using System;

namespace HD.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public static class FileConfigurationExtensions
    {
        private static string FileProviderKey = "FileProvider";
        private static string FileLoadExceptionHandlerKey = "FileLoadExceptionHandler";

        /// <summary>
        /// Sets the default <see cref="IFileProvider"/> to be used for file-based providers.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="fileProvider">The default file provider instance.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder SetFileProvider(this IConfigurationBuilder builder, IFileProvider fileProvider)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (fileProvider == null)
            {
                throw new ArgumentNullException(nameof(fileProvider));
            }

            builder.Properties[FileProviderKey] = fileProvider;
            return builder;
        }

        /// <summary>
        /// Gets the default <see cref="IFileProvider"/> to be used for file-based providers.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IFileProvider GetFileProvider(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            object provider;
            if (builder.Properties.TryGetValue(FileProviderKey, out provider))
            {
                return builder.Properties[FileProviderKey] as IFileProvider;
            }

#if NET451
            var stringBasePath = AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") as string
                ?? AppDomain.CurrentDomain.BaseDirectory
                ?? string.Empty;
            return new PhysicalFileProvider(stringBasePath);
#else
            return new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty);
#endif
        }

        /// <summary>
        /// Sets the FileProvider for file-based providers to a PhysicalFileProvider with the base path.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="basePath">The absolute path of file-based providers.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder SetBasePath(this IConfigurationBuilder builder, string basePath)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (basePath == null)
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            return builder.SetFileProvider(new PhysicalFileProvider(basePath));
        }

        /// <summary>
        /// Sets a default action to be invoked for file-based providers when an error occurs.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="handler">The Action to be invoked on a file load exception.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder SetFileLoadExceptionHandler(this IConfigurationBuilder builder, Action<FileLoadExceptionContext> handler)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Properties[FileLoadExceptionHandlerKey] = handler;
            return builder;
        }

        /// <summary>
        /// Gets the default <see cref="IFileProvider"/> to be used for file-based providers.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static Action<FileLoadExceptionContext> GetFileLoadExceptionHandler(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            object handler;
            if (builder.Properties.TryGetValue(FileLoadExceptionHandlerKey, out handler))
            {
                return builder.Properties[FileLoadExceptionHandlerKey] as Action<FileLoadExceptionContext>;
            }
            return null;
        }
    }
}