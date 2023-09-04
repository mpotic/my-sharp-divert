namespace MySharpDivert
{
    public interface IResponse
	{
		bool IsSuccessful { get; }

		string ErrorMessage { get; }
	}
}
