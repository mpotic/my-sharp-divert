namespace MySharpDivert
{
    public class Response : IResponse
	{
		public Response()
		{
		}

		public Response(bool isSuccessful)
		{
			IsSuccessful = isSuccessful;
		}

		public Response(bool isSuccessful, string errorMessage)
		{
			IsSuccessful = isSuccessful;
			ErrorMessage = errorMessage;
		}

		public bool IsSuccessful { get; }

		public string ErrorMessage { get; }
	}
}
