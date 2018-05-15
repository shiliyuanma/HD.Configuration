// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace HD.Configuration.FileExtensions.Physical
{
    internal class Clock : IClock
    {
        public static readonly Clock Instance = new Clock();

        private Clock()
        {
        }

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
