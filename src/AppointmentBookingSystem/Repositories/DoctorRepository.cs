using AppointmentBookingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppointmentBookingSystem.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly string _connectionString;

    public DoctorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Doctor>("SELECT * FROM Doctors");
    }

    public async Task<Doctor?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Doctor>(
            "SELECT * FROM Doctors WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> CreateAsync(Doctor doctor)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Doctors (Name, Specialization, Email, Phone, UserId, CreatedAt) 
                    VALUES (@Name, @Specialization, @Email, @Phone, @UserId, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(sql, doctor);
    }

    public async Task<bool> UpdateAsync(Doctor doctor)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"UPDATE Doctors 
                    SET Name = @Name, Specialization = @Specialization, 
                        Email = @Email, Phone = @Phone 
                    WHERE Id = @Id";
        var affected = await connection.ExecuteAsync(sql, doctor);
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Doctors WHERE Id = @Id", new { Id = id });
        return affected > 0;
    }
}
