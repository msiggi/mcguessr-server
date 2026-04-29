using System.Text.Json;
using McguesSr.Models;

namespace McguesSr.Services;

public class LeaderboardService
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private static readonly JsonSerializerOptions _opts = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public LeaderboardService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "leaderboard.json");
        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]");
    }

    public async Task<List<LeaderboardEntry>> GetTopTenAsync()
    {
        await _lock.WaitAsync();
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<LeaderboardEntry>>(json, _opts) ?? [];
        }
        finally { _lock.Release(); }
    }

    public async Task AddScoreAsync(string name, int score)
    {
        await _lock.WaitAsync();
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            var data = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json, _opts) ?? [];
            data.Add(new LeaderboardEntry { Name = name, Score = score });
            data = data.OrderByDescending(e => e.Score).Take(10).ToList();
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(data, _opts));
        }
        finally { _lock.Release(); }
    }
}
