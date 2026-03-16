using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Repositories;

namespace AppointmentBookingSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUserRepository _userRepository;

    public DoctorService(IDoctorRepository doctorRepository, IUserRepository userRepository)
    {
        _doctorRepository = doctorRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return await _doctorRepository.GetAllAsync();
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _doctorRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateDoctorAsync(CreateDoctorRequest request)
    {
        int? userId = null;
        
        if (!string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = AuthService.HashPassword(request.Password),
                Email = request.Email,
                Role = "Doctor",
                CreatedAt = DateTime.UtcNow
            };
            userId = await _userRepository.CreateAsync(user);
        }

        var doctor = new Doctor
        {
            Name = request.Name,
            Specialization = request.Specialization,
            Email = request.Email,
            Phone = request.Phone,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        return await _doctorRepository.CreateAsync(doctor);
    }

    public async Task<bool> UpdateDoctorAsync(int id, UpdateDoctorRequest request)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor == null) return false;

        doctor.Name = request.Name;
        doctor.Specialization = request.Specialization;
        doctor.Email = request.Email;
        doctor.Phone = request.Phone;

        return await _doctorRepository.UpdateAsync(doctor);
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await _doctorRepository.DeleteAsync(id);
    }
}
