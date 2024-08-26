namespace Notifications.Application.Utils
{
    public static class Response
    {
        public static ApiResponse<T> Success<T>(T value, string title = "Ok", string message = "success")
        {
            return new ApiResponse<T>()
            {
                Values = value,
                Title = title,
                Message = message,
                Error = false
            };

        }

        public static ApiResponse<T> Error<T>(T value, string title = "Error", string message = "there was an error, contact an admon")
        {
            return new ApiResponse<T>()
            {
                Values = value,
                Title = title,
                Message = message,
                Error = true
            };

        }
    }


}
