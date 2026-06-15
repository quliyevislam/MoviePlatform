using System.Text.Json.Serialization;
using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserCreateMovie;

public record UserCreateMovieCommand : ICommand<int>
{
	public int UserId { get; init; }
	public string Title { get; init; }
	public string Description { get; init; }
	public string Genre { get; init; }
	public DateOnly ReleaseDate { get; init; }

	[JsonConstructor]
	public UserCreateMovieCommand(int userId, string title, string description, string genre, DateOnly releaseDate)
	{
		UserId = userId;
		Title = title.Trim();
		Description = description.Trim();
		Genre = genre.Trim();
		ReleaseDate = releaseDate;
	}
}
