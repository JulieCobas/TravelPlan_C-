namespace projet_csharp_travel_plan_frontend.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

