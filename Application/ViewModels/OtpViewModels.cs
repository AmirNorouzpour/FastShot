namespace Application.ViewModels
{
    public class OtpReturn
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    public class OtpReturnRes
    {
        public OtpReturn? @return { get; set; }
    }
}
