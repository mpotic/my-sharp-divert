namespace MySharpDivert
{
    public interface IResponse
	{
		bool IsSuccessful { get; set; }

		string ErrorMessage { get; set; }
	}
}
