using System;
using System.Net.Http.Json;
using System.Text.Json;
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
        try
        {
            var response = await _httpClient.GetAsync("/api/students/count-by-school");
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadFromJsonAsync<List<SchoolCount>>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Request Error: {ex.Message}");
            return null; 
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Parsing Error: {ex.Message}");
            return null; 
        }
    }

    // Model for school count
    public class SchoolCount
    {
        public string? School { get; set; }
        public int Count { get; set; }
    }
}

