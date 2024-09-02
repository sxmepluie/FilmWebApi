using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore.Config
{
    public class FilmConfig : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasData(
              new Film
              {
                  FilmId = 1,
                  FilmTitle = "Star Wars",
                  Description = "Star Wars is a space opera saga created by George Lucas. The series explores themes of good vs. evil, the power of the Force, and the journey of heroes in a galaxy far, far away.",
                  Year = 1977,
                  Time = 121,
                  Rate = 0,
                  DirectorId = 1,
                  DirectorName = "George Lucas"
              },
              new Film
              {
                  FilmId = 2,
                  FilmTitle = "The Godfather",
                  Description = "The Godfather is a crime film directed by Francis Ford Coppola. The movie tells the story of the powerful Italian-American crime family of Don Vito Corleone.",
                  Year = 1972,
                  Time = 175,
                  Rate = 0,
                  DirectorId = 2,
                  DirectorName = "Francis Ford Coppola"
              },

              new Film
              {
                  FilmId = 3,
                  FilmTitle = "The Matrix",
                  Description = "The Matrix is a science fiction film directed by the Wachowskis. It depicts a dystopian future in which reality as perceived by most humans is actually a simulated reality called 'the Matrix'.",
                  Year = 1999,
                  Time = 136,
                  Rate = 0,
                  DirectorId = 3,
                  DirectorName = "The Wachowskis"
              }

             );
        }
    }
}
