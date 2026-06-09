using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviePlatform.Domain.Movies;
using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Infrastructure.Persistence.Configurations;

internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
	public void Configure(EntityTypeBuilder<Movie> builder)
	{
		builder.ToTable(
			"movies",
			table =>
			{
				table.HasCheckConstraint(
					"CK_movies_title_not_empty",
					"length(title) > 0"
				);

				table.HasCheckConstraint(
					"CK_movies_average_rating_range",
					$"average_rating = 0 OR "
					+ "(average_rating >= {MovieConstants.ReviewScore.MinScore} AND "
					+ "average_rating <= {MovieConstants.ReviewScore.MaxScore})"
				);

				table.HasCheckConstraint(
					"CK_movies_review_count_not_negative",
					"review_count >= 0"
				);
			}
		);

		builder.HasKey(movie => movie.Id);

		builder
			.Property(movie => movie.Id)
			.HasColumnName("movie_id")
			.ValueGeneratedOnAdd()
			.HasConversion(movieId => movieId.Value, value => MovieId.Create(value).Value);

		builder
			.Property(movie => movie.UserId)
			.HasColumnName("user_id")
			.IsRequired()
			.HasConversion(userId => userId.Value, value => UserId.Create(value).Value);

		builder
			.Property(movie => movie.Title)
			.HasColumnName("title")
			.IsRequired()
			.HasMaxLength(MovieConstants.Title.MaxLength)
			.HasConversion(title => title.Value, value => Title.Create(value).Value);

		builder
			.Property(movie => movie.Description)
			.HasColumnName("description")
			.HasMaxLength(MovieConstants.Description.MaxLength)
			.HasConversion(description => description.Value, value => Description.Create(value).Value);

		builder
			.Property(movie => movie.Genre)
			.HasColumnName("genre")
			.HasConversion(genre => genre.ToString(), value => (Genre) Enum.Parse(typeof(Genre), value));

		builder
			.Property(movie => movie.ReleaseDate)
			.HasColumnName("release_date")
			.HasColumnType("date")
			.HasConversion(releaseDate => releaseDate.Value, value => ReleaseDate.Create(value, DateTime.UtcNow).Value);

		builder
			.Property(movie => movie.AverageRating)
			.HasColumnName("average_rating")
			.HasPrecision(MovieConstants.ReviewScore.MaxDigitsPrecision, MovieConstants.ReviewScore.DecimalPlacesScale)
			.HasConversion(averageRating => averageRating.Value, value => AverageRating.Create(value).Value);

		builder
			.Property(movie => movie.ReviewCount)
			.HasColumnName("review_count")
			.HasConversion(reviewCount => reviewCount.Value, value => ReviewCount.Create(value).Value);

			ConfigureComments(builder);
			ConfigureReviews(builder);
	}

	public static void ConfigureComments(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsMany(
			movie => movie.Comments,
			commentBuilder =>
			{
				commentBuilder.ToTable(
					"comments",
					table =>
					{
						table.HasCheckConstraint(
							"CK_comment_content_not_empty",
							"length(content) > 0"
						);
					}
				);

				commentBuilder.HasKey(comment => comment.Id);

				commentBuilder
					.Property(comment => comment.Id)
					.HasColumnName("comment_id")
					.ValueGeneratedOnAdd()
					.HasConversion(commentId => commentId.Value, value => CommentId.Create(value).Value);

				commentBuilder
					.Property(comment => comment.UserId)
					.HasColumnName("user_id")
					.IsRequired()
					.HasConversion(userId => userId.Value, value => UserId.Create(value).Value);

				commentBuilder
					.Property(comment => comment.MovieId)
					.IsRequired()
					.HasColumnName("movie_id");

				commentBuilder
					.WithOwner()
					.HasForeignKey(comment => comment.MovieId);

				commentBuilder
					.Property(comment => comment.Content)
					.HasColumnName("content")
					.IsRequired()
					.HasMaxLength(MovieConstants.CommentContent.MaxLength)
					.HasConversion(content => content.Value, value => CommentContent.Create(value).Value);

				commentBuilder
					.Property(comment => comment.CreatedAtUtc)
					.HasColumnName("created_at_utc");
			}
		);
	}

	public static void ConfigureReviews(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsMany(
			movie => movie.Reviews,
			reviewBuilder =>
			{
				reviewBuilder.ToTable(
					"reviews",
					table =>
					{
						table.HasCheckConstraint(
							"CK_review_score_range",
							$"score >= {MovieConstants.ReviewScore.MinScore} AND score <= {MovieConstants.ReviewScore.MaxScore}"
						);
					}
				);

				reviewBuilder
					.HasIndex(review => new { review.MovieId, review.UserId })
					.IsUnique()
					.HasDatabaseName("IX_reviews_movie_id_user_id_unique");

				reviewBuilder.HasKey(review => review.Id);

				reviewBuilder
					.Property(review => review.Id)
					.HasColumnName("review_id")
					.ValueGeneratedOnAdd()
					.HasConversion(reviewId => reviewId.Value, value => ReviewId.Create(value).Value);

				reviewBuilder
					.Property(review => review.UserId)
					.HasColumnName("user_id")
					.IsRequired()
					.HasConversion(userId => userId.Value, value => UserId.Create(value).Value);

				reviewBuilder
					.Property(review => review.MovieId)
					.IsRequired()
					.HasColumnName("movie_id");

				reviewBuilder
					.WithOwner()
					.HasForeignKey(review => review.MovieId);

				reviewBuilder
					.Property(review => review.Score)
					.HasColumnName("score")
					.HasPrecision(MovieConstants.ReviewScore.MaxDigitsPrecision, MovieConstants.ReviewScore.DecimalPlacesScale)
					.HasConversion(score => score.Value, value => ReviewScore.Create(value).Value);

				reviewBuilder
					.Property(review => review.CreatedAtUtc)
					.HasColumnName("created_at_utc");
			}
		);
	}
}
