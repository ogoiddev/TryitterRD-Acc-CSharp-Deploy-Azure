namespace TryitterRD.Dtos
{
    public class ErrorResponseDTO
    {
        public int Status { get; set; }
        public string Description { get; set; }
        public  List<string> Errors { get; set; }
    }
}
