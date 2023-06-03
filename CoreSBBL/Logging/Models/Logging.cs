using System.Collections.Generic;
using CoreSBShared.Universal.Infrastructure.Models;

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
    public class LoggingCore : EntityEF
    {
        // The log text itself
        public string? Message { get; set; }

        // To distinguish logging by types
        public LoggingLabel? Label { get; set; } = new ();

        // To add more granularity to search
        // further string tagging for elastic and mongo 
        public LoggingTag? Tags { get; set; } = new ();
    }

    // Labels assumed to work with constants
    public class LoggingLabel : EntityEF
    {
        public static string Text { get; set; } = DefaultModelValues.Logging.LoggingLabelDefault;
    }

    // Enum replacement
    public class LoggingTag : Tag
    {
        public new IList<Tag> Tags { get; set; } = new List<Tag>()
        {
            DefaultModelValues.LoggingTags.Default, DefaultModelValues.LoggingTags.System,
        };
    }

    public class LoggingDAL : LoggingCore
    {
    }

    public class LoggingBL : LoggingCore
    {
    }

    public class LoggingAPI : LoggingCore
    {
    }
    
    
    public class LoggingMongo : EntityMongo
    {
        public string Message { get; set; }
    }
    
    public class LoggingElastic : EntityElastic
    {
        public string Message { get; set; }
    }
}
