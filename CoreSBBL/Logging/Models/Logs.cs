using System.Collections.Generic;
using System.Linq;
using CoreSBBL.Logging.Models.DAL.TC;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;

// Type containing ids like {idInt, idString, id Object}
namespace CoreSBBL.Logging.Models.DAL.TC
{
    // DOMAIN DAL TC
    // model containing domain fields, and id impl details like (int, string obj )
    // but not infrastructure - (like EF nav props, elk and mongo tags)
    public class TagDalIntTc : CoreDalint, ICoreTag
    {
        public int Index { get; set; }
        public string Text { get; set; }
    }
    
    // Labels assumed to work with constants
    public class LabelDalIntTc : CoreDalint
    {
        public string Text { get; set; } = DefaultModelValues.Logging.LoggingLabelDefault;
    }
    
    
    
    // DOMAIN DAL TC INFR
    // models contain domain logic, db level
    // db infr specific (like EF nav props, elk and mongo tags)
    
    //Logs Tag model for EF
    public class LogsTagDALEfTc : TagDalIntTc
    {
        public ICollection<LogsDALEf> Loggings { get; set; }
    }
    
    // Logs EF model
    public class LogsDALEfTc : CoreDalint
    {
        // The log text itself
        public string? Message { get; set; }

        public int? LabelId { get; set; }

        // To distinguish logging by types
        public LabelDalIntTc? Label { get; set; }

        // To add more granularity to search
        // further string tagging for elastic and mongo 
        public virtual ICollection<LogsTagDALEfTc> Tags { get; set; } = new List<LogsTagDALEfTc>
        {
            new() {Index = 1, Text = DefaultModelValues.Logging.LoggingLabelDefault},
            new() {Index = 2, Text = DefaultModelValues.Logging.LoggingLabelError}
        };
    }
    public class LogsDALEf : LogsDALEfTc
    {
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



    // Models for mongo
    // DOMAIN DAL TC
    public class LabelMongo : LabelDalIntTc
    {
    }
    
    public class TagMongo : TagDalIntTc
    {
    }
    public class LogsMongo : CoreDalObj
    {
        public string? Message { get; set; }
        
        public LabelMongo? Label { get; set; }
        public IList<TagMongo> Tags { get; set; }
    }

  

    // Models for elastic
    public class LabelElk : LabelDalIntTc
    {
    }
    public class TagElk : TagDalIntTc
    {
    }
    public class LogsElk : CoreElastic
    {
        public string? Message { get; set; }
        
        public LabelElk? Label { get; set; }
        public IList<TagElk> Tags { get; set; }
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
