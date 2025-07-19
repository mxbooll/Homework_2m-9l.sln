namespace Homework_2m_9l.Configurations;

public class PostgresConfig
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string GetConnectionString() =>
        $"Server={Host}:{Port};" +
        $"Database={Database};" +
        $"User Id={Username};" +
        $"Password={Password};";
}