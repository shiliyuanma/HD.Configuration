// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using HD.Configuration.FileExtensions.Abstractions;

namespace HD.Configuration.FileExtensions.Internal
{
    internal class InMemoryFileInfo : FileInfoBase
    {
        private InMemoryDirectoryInfo _parent;

        public InMemoryFileInfo(string file, InMemoryDirectoryInfo parent)
        {
            FullName = file;
            Name = Path.GetFileName(file);
            _parent = parent;
        }

        public override string FullName { get; }

        public override string Name { get; }

        public override DirectoryInfoBase ParentDirectory
        {
            get
            {
                return _parent;
            }
        }
    }
}
