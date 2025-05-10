namespace ArtGalleryAPI.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static BaseResponse<T> SuccessResult(T data) => new BaseResponse<T> { Success = true, Data = data };
        public static BaseResponse<T> FailureResult(string errorMessage) => new BaseResponse<T> { Success = false, ErrorMessage = errorMessage };
    }
}
