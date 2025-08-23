using System.ComponentModel.DataAnnotations;

namespace MyLMS.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}