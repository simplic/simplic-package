﻿using System.Collections.Generic;

namespace Simplic.Package.Application
{
    public class GridStructureConfiguration : IApplicationConfiguration
    {
        public IList<StackItem> Stacks { get; set; } = new List<StackItem>();
    }
}