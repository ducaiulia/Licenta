using AllShare.Services.Utils;

namespace AllShare.Services
{
    public class ServiceResult<T>
    {
        public ResultType ResultType { get; set; }
        public T Result { get; set; }
    }
}
