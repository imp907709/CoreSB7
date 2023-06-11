﻿namespace CoreSBShared.Registrations
{
    /// <summary>
    ///     Models for settings with option pattern registration
    /// </summary>
    public class Connections
    {
        public static string SectionName => RegistrationStrings.ConnectionsSectionName;

        public string? MSSQL { get; set; }
        public string? MSSQLLOCAL { get; set; }
        public string? DOCKERMSSQL { get; set; }
        public string? AZUREMSSQL { get; set; }
    }

    public class MongoConnection
    {
        public static string SectionName => RegistrationStrings.MongoSectionName;
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class ElasticConenction
    {
        public static string SectionName => RegistrationStrings.ElasticSectionName;
        public string? ConnectionString { get; set; }
    }


    /// <summary>
    ///     Connections and vars from app settings register for option pattern
    /// </summary>
    public static class ConnectionsRegister
    {
        public static Connections Connections { get; set; } = new();
        public static MongoConnection MongoConnection { get; set; } = new();
        public static ElasticConenction ElasticConenction { get; set; } = new();
    }
}
