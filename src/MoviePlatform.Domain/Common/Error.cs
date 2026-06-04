namespace MoviePlatform.Domain.Common;

public sealed class Error
{
	public string Code { get; }
	public string Description { get; }

	private Error(string code, string description)
	{
		Code = code;
		Description = description;
	}

	public static readonly Error None = new(string.Empty, string.Empty);
	public static readonly Error NullValue = new("General.Null", "A null value was provided.");

	public static Error New(string code, string description) => new(code, description);
	public override string ToString() => Code;
}
