namespace SharedLibrary.DTO_s
{
    public class ErrorDTO
    {
        public List<String> Errors { get; private set; } = new List<String>();
        public bool IsShow { get; private set; }
        public ErrorDTO(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }
        public ErrorDTO(List<string> error, bool isShow)
        {
            Errors = error;
            IsShow = isShow;
        }
    }
}
