using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppointmentBookingSystem.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;

    public AppointmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<IEnumerable<AppointmentResponse>> GetByUserIdAsync(int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT a.Id, a.SlotId, d.Name as DoctorName, d.Specialization, 
                    s.StartTime, s.EndTime, a.PatientName, a.PatientEmail, 
                    a.PatientPhone, a.Status, a.Notes, a.CreatedAt
                    FROM Appointments a
                    INNER JOIN Doctors d ON a.DoctorId = d.Id
                    INNER JOIN AppointmentSlots s ON a.SlotId = s.Id
                    WHERE a.UserId = @UserId
                    ORDER BY s.StartTime DESC";
        return await connection.QueryAsync<AppointmentResponse>(sql, new { UserId = userId });
    }

    public async Task<IEnumerable<AppointmentResponse>> GetByDoctorIdAsync(int doctorId)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT a.Id, a.SlotId, d.Name as DoctorName, d.Specialization, 
                    s.StartTime, s.EndTime, a.PatientName, a.PatientEmail, 
                    a.PatientPhone, a.Status, a.Notes, a.CreatedAt
                    FROM Appointments a
                    INNER JOIN Doctors d ON a.DoctorId = d.Id
                    INNER JOIN AppointmentSlots s ON a.SlotId = s.Id
                    WHERE a.DoctorId = @DoctorId
                    ORDER BY s.StartTime";
        return await connection.QueryAsync<AppointmentResponse>(sql, new { DoctorId = doctorId });
    }

    public async Task<AppointmentResponse?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT a.Id, a.SlotId, d.Name as DoctorName, d.Specialization, 
                    s.StartTime, s.EndTime, a.PatientName, a.PatientEmail, 
                    a.PatientPhone, a.Status, a.Notes, a.CreatedAt
                    FROM Appointments a
                    INNER JOIN Doctors d ON a.DoctorId = d.Id
                    INNER JOIN AppointmentSlots s ON a.SlotId = s.Id
                    WHERE a.Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<AppointmentResponse>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Appointment appointment)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Appointments (SlotId, UserId, DoctorId, PatientName, PatientEmail, PatientPhone, Status, Notes, CreatedAt) 
                    VALUES (@SlotId, @UserId, @DoctorId, @PatientName, @PatientEmail, @PatientPhone, @Status, @Notes, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(sql, appointment);
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        using var connection = new SqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "UPDATE Appointments SET Status = @Status WHERE Id = @Id",
            new { Id = id, Status = status });
        return affected > 0;
    }
}
