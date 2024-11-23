using Dapper;
using System.Collections.Generic;
using System.Data;
using KartverketGroup20.Models;
using KartverketGroup20.Data.Enum;

namespace WebApplication1.Data
{
    public class ReportService
    {
        private readonly IDbConnection _dbConnection;

        public ReportService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Lagrer ny rapport i databasen
        public void AddReport(string description, string geoJson, string userId)
        {
            string query = @"INSERT INTO Reports (Description, GeoJson, UserId) 
                             VALUES (@Description, @GeoJson, @UserId)";
            _dbConnection.Execute(query, new { Description = description, GeoJson = geoJson, UserId = userId });
        }

        // Returnerer alle rapporter basert på userId
        public IEnumerable<Report> GetAllReport(string userId)
        {
            string query = @"SELECT * FROM Reports WHERE UserId = @UserId";
            return _dbConnection.Query<Report>(query, new { UserId = userId });
        }

        // Returnerer en spesifikk rapport basert på Id og userId
        public Report GetReportById(int id, string userId)
        {
            string query = "SELECT * FROM Reports WHERE Id = @Id AND UserId = @UserId";
            return _dbConnection.QuerySingleOrDefault<Report>(query, new { Id = id, UserId = userId });
        }

        // Oppdaterer en eksisterende rapport basert på Id og userId for en vanlig bruker
        public void UpdateReport(int id, string description, string geoJsonData, string userId)
        {
            string query = @"UPDATE Reports 
                             SET Description = @Description, GeoJson = @GeoJson 
                             WHERE Id = @Id AND UserId = @UserId";
            Console.WriteLine(query);
            _dbConnection.Execute(query, new { Id = id, Description = description, GeoJson = geoJsonData, UserId = userId });
        }

        // Oppdaterer en eksisterende rapport basert på Id og userId for en administrator
        public void UpdateReportAdmin(int id, string userId, string feedback, Status status)
        {
            string query = @"UPDATE Reports
                            SET Status = @Status, Feedback = @Feedback
                            WHERE Id = @Id";
            Console.WriteLine(query);
            _dbConnection.Execute(query, new { Id = id, UserId = userId, Status = status, Feedback = feedback });
        }

        // Sletter en eksisterende rapport basert på Id og userId
        public void DeleteReport(int id, string userId)
        {
            string query = "DELETE FROM Reports WHERE Id = @Id AND UserId = @UserId";
            _dbConnection.Execute(query, new { Id = id, UserId = userId });
        }
    }
}