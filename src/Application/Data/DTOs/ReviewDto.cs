namespace Application.Data.DTOs;

public class ReviewDto
{
    public string SellerId { get; set; } = string.Empty;
    public string ReviewerId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}