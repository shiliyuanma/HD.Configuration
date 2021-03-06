// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using HD.Configuration.FileExtensions.Abstractions;

namespace HD.Configuration.FileExtensions.Internal
{
    public interface IPatternContext
    {
        void Declare(Action<IPathSegment, bool> onDeclare);

        bool Test(DirectoryInfoBase directory);

        PatternTestResult Test(FileInfoBase file);

        void PushDirectory(DirectoryInfoBase directory);

        void PopDirectory();
    }
}
