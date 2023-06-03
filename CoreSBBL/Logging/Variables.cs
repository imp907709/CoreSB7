

// Domain Logging specific strings 

using CoreSBBL.Logging.Models;

namespace CoreSBBL.Logging
{
    public class DefaultConfigurationValues
    {
        public static string DefaultElasticIndex => "logging";
    }
    
    public class DefaultModelValues
    {
        /// <summary>
        /// Strings for labels and tags
        /// </summary>
        public class Logging
        {
            public static string LoggingLabelDefault => "Default";
            public static string LoggingLabelError => "Error";
            
            public static string LoggingSystemTag => "System";
            
            public static string MessageEmpty =>  "empty";
        }
        
        /// <summary>
        /// Tags themselves
        /// </summary>
        public class LoggingTags
        {
            public static LoggingTag Default => new LoggingTag() {index = 1, Text = Logging.LoggingLabelDefault};
            public static LoggingTag System => new LoggingTag() {index = 2, Text = Logging.LoggingSystemTag};
        }
    }
}
