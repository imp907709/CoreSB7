using System.Collections.Generic;
using System.Linq;
using CoreSBBL.Logging.Models.DAL.TC;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;

// Type containing ids like {idInt, idString, id Object}
namespace CoreSBBL.Logging.Models.DAL.TC
{
    // Type contaioning ids, ids contain type name in it
    /// <summary>
    ///     Logging core model, as template for all layers
    ///     (logging not assumed to have some differences by layers,
    ///     cause it is project wide ProofOfWork model with no real intense business domain behaviour )
    /// </summary>
    public class LogsDALEfTc : CoreDalint
    {
        // The log text itself
        public string? Message { get; set; }

        public int? LabelId { get; set; }

        // To distinguish logging by types
        public LogsLabelDALEfTc? Label { get; set; }

        // To add more granularity to search
        // further string tagging for elastic and mongo 
        public virtual ICollection<LogsTagDALEfTc> Tags { get; set; } = new List<LogsTagDALEfTc>
        {
            new() {Index = 1, Text = DefaultModelValues.Logging.LoggingLabelDefault},
            new() {Index = 2, Text = DefaultModelValues.Logging.LoggingLabelError}
        };
    }

    public class LabelTagEF : CoreDalint, ICoreTag
    {
        public int Index { get; set; }
        public string Text { get; set; }
    }
    
    // Labels assumed to work with constants
    public class LogsLabelDALEfTc : CoreDalint
    {
        public static string Text { get; set; } = DefaultModelValues.Logging.LoggingLabelDefault;
    }

    public class LogsTagDALEfTc : LabelTagEF
    {
        public ICollection<LogsDALEf> Loggings { get; set; }
    }

    // Enum replacement
    public class LogsTagEnumDALEfTc
    {
        public IList<LogsTagDALEfTc> Tags { get; set; } = new List<LogsTagDALEfTc>
        {
            new() {Index = 1, Text = DefaultModelValues.Logging.LoggingLabelDefault},
            new() {Index = 2, Text = DefaultModelValues.Logging.LoggingLabelError}
        };

        public virtual LogsTagDALEfTc ToGet(int idx)
        {
            return Tags?.FirstOrDefault(s => s?.Index == idx);
        }

        public virtual LogsTagDALEfTc ToGet(string txt)
        {
            return Tags?.FirstOrDefault(s => s?.Text == txt);
        }
    }

    
    public class LogsDALEf : LogsDALEfTc
    {
    }

    
    
    // Models for mongo 
    public class LogsMongo : CoreDalObj
    {
        public string? Message { get; set; }
        
        public LabelMongo? Label { get; set; }
        public IList<TagMongo> Tags { get; set; }
    }

    public class LabelMongo : CoreDalObj
    {
        public string Text { get; set; }
    }
    
    public class TagMongo : CoreDalObj
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    // Models for elastic
    public class LogsElastic : CoreElastic
    {
        public string Message { get; set; }
    }
}


// Generic ids ICoreDal<TKey>
// like: ICoreDal<int> ICoreDal<string>
namespace CoreSBBL.Logging.Models.DAL.GN
{
    public interface ILogsDALCore: ICoreCreated
    {
        public string? Message { get; set; } 
    }
    
    public class LogsDALEfGn : CoreDalIntg, ILogsDALCore
    {
        public string? Message { get; set; }
    }
}


// Business layer models
namespace CoreSBBL.Logging.Models.TC.BL
{
    public class LogsBL : LogsDALEfTc
    {
    }
}

// Contollers and API layer models
namespace CoreSBBL.Logging.Models.TC.API
{
    public class LogsAPI
    {
        public string Message { get; set; } = DefaultModelValues.Logging.MessageEmpty;
    }
}
