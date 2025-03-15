using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;

    public RedisService(IConfiguration configuration)
    {
        var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
        _db = redis.GetDatabase();
    }
    
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var json = await _db.StringGetAsync(key).ConfigureAwait(false);
        //var r = !json.IsNullOrEmpty ? JsonSerializer.Deserialize<T>(json.ToString()) : default;
        //return r;
        if (json.IsNullOrEmpty) return default;
        using var document = JsonDocument.Parse(json.ToString());
        var jsonString = document.RootElement.GetRawText();
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    public async Task<T?> UpdateAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        if (!await _db.KeyExistsAsync(key).ConfigureAwait(false)) return default;
        
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json).ConfigureAwait(false);
        return value;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value);
        if (expiration.HasValue)
            await _db.StringSetAsync(key, json, expiration.Value).ConfigureAwait(false);
        else
            await _db.StringSetAsync(key, json).ConfigureAwait(false);
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _db.KeyDeleteAsync(key).ConfigureAwait(false);
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _db.KeyExistsAsync(key).ConfigureAwait(false);
    }


}