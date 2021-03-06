// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace HD.Configuration.Abstractions
{
    /// <summary>
    /// Represents the root of an <see cref="IConfiguration"/> hierarchy.
    /// </summary>
    public interface IConfigurationRoot : IConfiguration
    {
        /// <summary>
        /// Force the configuration values to be reloaded from the underlying <see cref="IConfigurationProvider"/>s.
        /// </summary>
        void Reload();
    }
}
