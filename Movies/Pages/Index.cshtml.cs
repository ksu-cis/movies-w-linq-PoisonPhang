using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> mpaa { get; set; } = new List<string>();

        [BindProperty]
        public float? minIMDB { get; set; }

        [BindProperty]
        public float? maxIMDB { get; set; }

        [BindProperty]
        public string sort { get; set; }



        public void OnGet()
        {
            Movies = MovieDatabase.All;
            Movies = Movies.OrderBy(movie => movie.Title);
        }

        public void OnPost()
        {
            Movies = MovieDatabase.All;

            if (search != null)
            {
                // Movies = MovieDatabase.Search(Movies, search);
                Movies = Movies.Where(movie => movie.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if(mpaa.Count != 0)
            {
                // Movies = MovieDatabase.FilterByMPAA(Movies, mpaa);
                Movies = Movies.Where(movie => mpaa.Contains(movie.MPAA_Rating));
            }

            if(minIMDB != null)
            {
                // Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)minIMDB);
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && movie.IMDB_Rating >= minIMDB);
            }

            if (maxIMDB != null)
            {
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && movie.IMDB_Rating <= maxIMDB);
            }

            switch(sort)
            {
                case "title":
                    Movies = Movies.OrderBy(movie => movie.Title);
                    break;
                case "director":
                    Movies = Movies
                        .Where(movie => movie.Director != null)
                        .OrderBy(movie => movie.Director);
                    break;
                case "mpaa":
                    Movies = Movies
                        .Where(movie => movie.MPAA_Rating != null)
                        .OrderBy(movie => movie.MPAA_Rating);
                    break;
                case "imdb":
                    Movies = Movies
                        .Where(movie => movie.IMDB_Rating != null)
                        .OrderBy(movie => movie.IMDB_Rating);
                    break;
                case "rt":
                    Movies = Movies
                        .Where(movie => movie.Rotten_Tomatoes_Rating != null)
                        .OrderBy(movie => movie.Rotten_Tomatoes_Rating);
                    break;
            }
        }
    }
}
