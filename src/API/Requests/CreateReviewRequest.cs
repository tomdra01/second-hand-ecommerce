namespace API.Requests;

public class CreateReviewRequest
{
    public string SellerId { get; set; } = string.Empty;
    public string ReviewerId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}