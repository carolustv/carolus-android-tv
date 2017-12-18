using System.Collections.Generic;

namespace TvLeanback.Models
{
    public class Playlist
    {

        IList<Movie> playlist;
        int currentPosition;

        public Playlist()
        {
            playlist = new List<Movie>();
            currentPosition = 0;
        }

        public void Clear()
        {
            playlist.Clear();
        }

        public void Add(Movie movie)
        {
            playlist.Add(movie);
        }


        public void SetCurrentPosition(int currentPosition)
        {
            this.currentPosition = currentPosition;
        }

        public int Size()
        {
            return playlist.Count;
        }

        public Movie Next()
        {
            if ((currentPosition + 1) < size())
            {
                currentPosition++;
                return playlist[currentPosition];
            }
            return null;
        }

        public Movie Previous()
        {
            if (currentPosition - 1 >= 0)
            {
                currentPosition--;
                return playlist[currentPosition];
            }
            return null;
        }
    }
}
