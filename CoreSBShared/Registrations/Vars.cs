namespace CoreSBShared.Registrations
{
    /// <summary>
    /// App settings sections and keys string constants
    /// </summary>
    public class RegistrationStrings
    {
        public static string ConnectionsSectionName => "Connections";
        
        public static string MSSQL => "MSSQL";
        public static string DOCKERMSSQL => "DOCKERMSSQL";
        public static string AZUREMSSQL => "AZUREMSSQL";

        
        public static string MongoSectionName => "Mongo";
        public static string MongoConnetionsString => "ConnectionString";
        
        
        public static string ElasticSectionName => "Elastic";
        public static string ElasticConnetionsString => "ConnectionString";
    }
}