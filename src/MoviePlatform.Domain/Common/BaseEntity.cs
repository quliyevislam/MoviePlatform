namespace MoviePlatform.Domain.Common;

public abstract class BaseEntity<TId> where TId : struct
{
	public TId Id { get; protected set; }

	public override bool Equals(object? obj)
	{
		if (obj is not BaseEntity<TId> other)
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return Id.Equals(other.Id);
	}

	public override int GetHashCode() => Id.GetHashCode();

	public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
	{
		if (left is null && right is null)
		{
			return true;
		}

		if (left is null || right is null)
		{
			return false;
		}

		return left.Equals(right);
	}

	public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right) => !(left == right);
}
