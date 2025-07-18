using System.ComponentModel.DataAnnotations;

namespace Homework_2m_9l.Entities.Users;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(256)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string Phone { get; set; } = string.Empty;
}