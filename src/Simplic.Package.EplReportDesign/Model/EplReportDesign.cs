﻿using Newtonsoft.Json;
using System;

namespace Simplic.Package.EplReportDesign
{
    public class EplReportDesign : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public string ReportContent { get; set; }
        public int PrinterHeadWidth { get; set; }
    }
}