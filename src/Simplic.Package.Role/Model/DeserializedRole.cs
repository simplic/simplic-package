﻿using System;

namespace Simplic.Package.Role
{
    public class DeserializedRole : IContent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public string Description { get; set; }
    }
}