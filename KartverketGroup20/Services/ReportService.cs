using Dapper;
using KartverketGroup20.Models;
using MySqlConnector;
using System;
using System.Data;

namespace KartverketGroup20.Services
{
    // Tjenesten lar deg utføre CRUD-operasjoner (Create, Read, Update, Delete) på Report tabell
    public class ReportService
    {
        // Definerer en variabel av type IConfigration som brukes til å hente "connection string" fra "appsettings.json"
        private readonly IConfiguration _configuration;

        // Definerer en variabel som beholder "connection string" som hentes fra "appsettings.json" under nøkkelen "DefaultConnection"
        private readonly string? _connectionString;

        public ReportService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // Get MariaDB Connection
        // Definerer en property som returnerer en ny instans av en "database connection" (MySqlConnection) ved å bruke "connection string".
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        // Setter inn en ny rad i Report tabell
        public void AddReport(string description, string geoJson)
        {
            // Denne konstruksjonen brukes for å sikre at ressursen, i dette tilfellet "dbConnection":
            // blir korrekt håndtert OG frigjort etter bruk. 
            using (IDbConnection dbConnection = Connection)
            {
                // Dapper bruker parameteriserte verdier for å unngå SQL-injeksjon.
                // Verdiene @Description og @GeoJson blir fylt med parameterne fra metoden.
                string query = @"INSERT INTO Report (Description, GeoJson) 
                             VALUES (@Description, @GeoJson)";
                dbConnection.Execute(query, new { Description = description, GeoJson = geoJson });
            }
        }

        // Henter alle rader fra Report tabellen.
        public IEnumerable<Report> GetAllReport(string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT * FROM Report";
                return dbConnection.Query<Report>(query);
            }
        }

        // Returnerer én enkelt Report basert på dens unike Id
        public Report GetReportById(int id, string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM Report WHERE Id = @Id";

                // QuerySingleOrDefault returnerer én rad, eller null hvis ingen match finnes.
                // Dette er nyttig for operasjoner som oppdatering eller sletting, hvor man typisk vil hente en spesifikk rad.
                return dbConnection.QuerySingleOrDefault<Report>(query, new { Id = id });
            }
        }

        //  Oppdaterer en eksisterende Report rad i databasen basert på Id
        public void UpdateReport(int id, string description, string geoJsonData, string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                // Oppdaterer verdiene i Description og GeoJson kolonnene for raden som har den spesifikke Id.
                string query = @"UPDATE Report 
                             SET Description = @Description, GeoJson = @GeoJson 
                             WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id, Description = description, GeoJson = geoJsonData });
            }
        }

        // Sletter en eksisterende Report rad basert på dens Id
        public void DeleteReport(int id, string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                // Fjerner raden med gitt Id fra Report tabellen
                string query = "DELETE FROM Report WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}