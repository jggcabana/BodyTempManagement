namespace BodyTemp.WebAPI.ViewModels.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public object Data { get; set; }
    }
}
