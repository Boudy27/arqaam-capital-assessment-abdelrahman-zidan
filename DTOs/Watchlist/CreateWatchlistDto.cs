using System.ComponentModel.DataAnnotations;
public class CreateWatchlistDto
{
    
    [Required(ErrorMessage = "Symbol is required.")]
    [StringLength(10, MinimumLength = 1, 
    ErrorMessage = "Symbol must be between 1 and 10 characters.")]
    public string Symbol { get; set; }

    [Required]
    [Range(typeof(decimal), "0.01", "999999999",
        ErrorMessage = "Target price must be greater than zero.")]
    public decimal TargetPrice { get; set; }

    [StringLength(250, ErrorMessage = "Note must be 250 characters or less.")]
    public string? Note { get; set; }
}