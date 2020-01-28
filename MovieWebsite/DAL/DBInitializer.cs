using MovieWebsite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(MyDBContext db)
        {
            db.Database.EnsureCreated();

            // Look for any students.
            if (db.Movies.Any())
            {
                return;   // DB has been seeded
            }

            List<Movie> movies = new List<Movie>
            {
                new Movie
                {
                    Title="The Lord of the Rings: The Fellowship of the Ring",
                    Description="A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                    Runtime="178",
                    ReleaseDate=DateTime.Parse("2001-12-19"),
                    Original=true,
                    Format=FormatEnum.DVD,
                    Owner=OwnerEnum.MS,
                    RegisseurID=1
                }
            };
            foreach(var movie in movies)
            {
                db.Movies.Add(movie);
            }
            db.SaveChanges();
            List<Actor> actors = new List<Actor>
            {
                new Actor
                {
                    FirstName="Ian",
                    LastName="McKellen"
                },
                new Actor
                {
                    FirstName="Orlando",
                    LastName="Bloom"
                }
            };
            foreach(var a in actors)
            {
                db.Actors.Add(a);
            }
            db.SaveChanges();
            List<Regisseur> regisseurs = new List<Regisseur>
            {
                new Regisseur
                {
                    FirstName="Peter",
                    LastName="Jackson"
                }
            };
            foreach(var r in regisseurs)
            {
                db.Regisseurs.Add(r);
            }
            db.SaveChanges();

            List<Genre> genres = new List<Genre>
            {
                new Genre
                {
                    Name="Fantasy"
                },
                new Genre
                {
                    Name="Drama"
                },
                new Genre
                {
                    Name="Adventure"
                }
            };
            foreach(var g in genres)
            {
                db.Genres.Add(g);
            }
            db.SaveChanges();

            List<ActorMovie> actorMovies = new List<ActorMovie>
            {
                new ActorMovie{ActorID = 1, MovieID = 1},
                new ActorMovie{ActorID=2, MovieID=1}
            };
            foreach(var am in actorMovies)
            {
                db.ActorMovies.Add(am);
            }
            db.SaveChanges();
            List<GenreMovie> genreMovies = new List<GenreMovie>
            {
                new GenreMovie{MovieID=1, GenreID=1},
                new GenreMovie{MovieID=1, GenreID=2},
                new GenreMovie{MovieID=1, GenreID=3}
            };
            foreach(var gm in genreMovies)
            {
                db.GenreMovies.Add(gm);
            }
            db.SaveChanges();

            //var students = new Student[]
            //{
            //    new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
            //    new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
            //    new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
            //    new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
            //    new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
            //    new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
            //    new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01")},
            //    new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}
            //};
            //foreach (Student s in students)
            //{
            //    context.Students.Add(s);
            //}
            //context.SaveChanges();

            //var courses = new Course[]
            //{
            //    new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //    new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //    new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //    new Course{CourseID=1045,Title="Calculus",Credits=4},
            //    new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //    new Course{CourseID=2021,Title="Composition",Credits=3},
            //    new Course{CourseID=2042,Title="Literature",Credits=4}
            //};
            //foreach (Course c in courses)
            //{
            //    context.Courses.Add(c);
            //}
            //context.SaveChanges();

            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            //    new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //    new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //    new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //    new Enrollment{StudentID=3,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //    new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //    new Enrollment{StudentID=6,CourseID=1045},
            //    new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            //};
            //foreach (Enrollment e in enrollments)
            //{
            //    context.Enrollments.Add(e);
            //}
            //context.SaveChanges();
        }
    }
}
