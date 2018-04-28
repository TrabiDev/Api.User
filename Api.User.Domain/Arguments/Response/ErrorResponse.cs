using System.Collections.Generic;

namespace Api.User.Domain.Arguments.Response
{
    public class ErrorResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
