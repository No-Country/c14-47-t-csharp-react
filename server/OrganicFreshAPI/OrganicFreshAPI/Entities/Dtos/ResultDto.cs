namespace OrganicFreshAPI.Entities.Dtos;

public class ResultDto<TResponse>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public TResponse? Response { get; set; }

}