using System;
using System.Net.Http.Json;
using SchoolLibrary;

namespace Client.Services;

public class StudentService
{
    private readonly HttpClient _httpClient;

    public StudentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get students by school
    public async Task<List<Student>?> GetStudentsBySchoolAsync(string school)
    {
        return await _httpClient.GetFromJsonAsync<List<Student>>($"/api/students/school/{school}");
    }

    // Get student count by school
    public async Task<List<SchoolCount>?> GetStudentCountBySchoolAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SchoolCount>>("/api/students/count-by-school");
    }

    // Model for school count
    public class SchoolCount
    {
        public string? School { get; set; }
        public int Count { get; set; }
    }
}

