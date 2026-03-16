using AppointmentBookingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppointmentBookingSystem.Repositories;

public class AppointmentSlotRepository : IAppointmentSlotRepository
{
    private readonly string _connectionString;

    public AppointmentSlotRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsByDoctorAsync(int doctorId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<AppointmentSlot>(
            "SELECT * FROM AppointmentSlots WHERE DoctorId = @DoctorId AND IsAvailable = 1 AND StartTime > GETDATE()",
            new { DoctorId = doctorId });
    }

    public async Task<AppointmentSlot?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<AppointmentSlot>(
            "SELECT * FROM AppointmentSlots WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> CreateAsync(AppointmentSlot slot)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO AppointmentSlots (DoctorId, StartTime, EndTime, IsAvailable, CreatedAt) 
                    VALUES (@DoctorId, @StartTime, @EndTime, @IsAvailable, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(sql, slot);
    }

    public async Task<bool> UpdateAvailabilityAsync(int id, bool isAvailable)
    {
        using var connection = new SqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "UPDATE AppointmentSlots SET IsAvailable = @IsAvailable WHERE Id = @Id",
            new { Id = id, IsAvailable = isAvailable });
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "DELETE FROM AppointmentSlots WHERE Id = @Id", new { Id = id });
        return affected > 0;
    }
}
