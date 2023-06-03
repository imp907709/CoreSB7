﻿using System.Collections.Generic;
using System.Linq;
using CoreSBShared.Universal.Infrastructure.Models;
using CoreSBBL.Logging;

/// <summary>
/// Logging Core models, layers
/// </summary>
namespace CoreSBBL.Logging.Models
{
    /// <summary>
    /// Logging core model, as template for all layers
    /// (logging not assumed to have some differences by layers,
    /// cause it is project wide ProofOfWork model with no real intense business domain behaviour )
    /// </summary>
    public class LogsCore : EntityEF
    {
        // The log text itself
        public string? Message { get; set; }

        // To distinguish logging by types
        public LogsLabelDALEF? Label { get; set; } = new ();

        // To add more granularity to search
        // further string tagging for elastic and mongo 
        public virtual ICollection<LogsTagDALEF> Tags { get; set; } = new LogsTagEnumDALEF().Tags;
    }

    // Labels assumed to work with constants
    public class LogsLabelDALEF : EntityEF
    {
        public static string Text { get; set; } = DefaultModelValues.Logging.LoggingLabelDefault;
    }

    public class LogsTagDALEF : Tag
    {
        public ICollection<LogsDALEF> Loggings { get; set; }
        
    }
    
    // Enum replacement
    public class LogsTagEnumDALEF
    {
        public IList<LogsTagDALEF> Tags { get; set; } = new List<LogsTagDALEF>()
        {
            DefaultModelValues.LoggingTags.Default, DefaultModelValues.LoggingTags.System,
        };
        
        public virtual LogsTagDALEF ToGet(int idx) => this.Tags?.FirstOrDefault(s => s?.index == idx);
        public virtual LogsTagDALEF ToGet(string txt) => this.Tags?.FirstOrDefault(s => s?.Text == txt);

    }

    public class LogsDALEF : LogsCore
    {
        public int? LabelId { get; set; }
        
        public override ICollection<LogsTagDALEF> Tags { get; set; } 
    }

    public class LogsBL : LogsCore
    {
    }

    public class LogsAPI
    {
        public string Message { get; set; } = DefaultModelValues.Logging.MessageEmpty;
    }
    
    
    public class LogsMongo : EntityMongo
    {
        public string Message { get; set; }
    }
    
    public class LogsElastic : EntityElastic
    {
        public string Message { get; set; }
    }
}