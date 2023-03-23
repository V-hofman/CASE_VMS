namespace CASE_VMS_Backend.Courses.Exceptions
{
    public class AlreadySignedInException : Exception
    {
        public AlreadySignedInException() { }

        public AlreadySignedInException(string message) : base(message) { }

        public AlreadySignedInException(string message, Exception inner) : base(message, inner) { }
    }
}
