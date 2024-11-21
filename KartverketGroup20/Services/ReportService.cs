﻿using Dapper;
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

        // Inserts a new record into the GeoChanges table
        public void AddReport(string description, string geoJson, string userId)
        {
            string query = @"INSERT INTO Reports (Description, GeoJson, UserId) 
                             VALUES (@Description, @GeoJson, @UserId)";
            _dbConnection.Execute(query, new { Description = description, GeoJson = geoJson, UserId = userId });
        }

        // Retrieves all records from the GeoChanges table for a specific user
        public IEnumerable<Report> GetAllReport(string userId)
        {
            string query = @"SELECT * FROM Reports WHERE UserId = @UserId";
            return _dbConnection.Query<Report>(query, new { UserId = userId });
        }

        // Retrieves a single GeoChange by its unique Id for a specific user
        public Report GetReportById(int id, string userId)
        {
            string query = "SELECT * FROM Reports WHERE Id = @Id AND UserId = @UserId";
            return _dbConnection.QuerySingleOrDefault<Report>(query, new { Id = id, UserId = userId });
        }

        // Updates an existing GeoChange record in the database based on Id and UserId
        public void UpdateReport(int id, string description, string geoJsonData, string userId)
        {
            string query = @"UPDATE Reports 
                             SET Description = @Description, GeoJson = @GeoJson 
                             WHERE Id = @Id AND UserId = @UserId";
            Console.WriteLine(query);
            _dbConnection.Execute(query, new { Id = id, Description = description, GeoJson = geoJsonData, UserId = userId });
        }

        //Updates report as an admin
        public void UpdateReportAdmin(int id, string userId, string feedback, Status status)
        {
            string query = @"UPDATE Reports
                            SET Status = @Status, Feedback = @Feedback
                            WHERE Id = @Id";
            Console.WriteLine(query);
            _dbConnection.Execute(query, new { Id = id, UserId = userId, Status = status, Feedback = feedback });
        }

        // Deletes an existing GeoChange record based on its Id and UserId
        public void DeleteReport(int id, string userId)
        {
            string query = "DELETE FROM Reports WHERE Id = @Id AND UserId = @UserId";
            _dbConnection.Execute(query, new { Id = id, UserId = userId });
        }
    }
}