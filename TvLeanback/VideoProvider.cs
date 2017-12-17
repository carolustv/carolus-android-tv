using System;
using System.Json;

using Android.Util;
using Android.Content;

using Java.IO;
using Java.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvLeanback
{
	public class VideoProvider
    {
        private const string TAG = "VideoProvider";

		public static Dictionary<String, IList<Movie>> MovieList {
			get;
			private set;
		}

		public static Context mContext {
			private get;
			set;
		}

        public static Dictionary<String, IList<Movie>> BuildMedia(Context ctx, string url)
        {
            if (null != MovieList)
            {
                return MovieList;
            }
            MovieList = new Dictionary<String, IList<Movie>>();

            var movies = new VideoProvider().ParseURL(url);
            string title, videoUrl, bgImageUrl, cardImageUrl, studio = "";
            var category_name = "Movies";
            var categoryList = new List<Movie>();
            foreach (var movie in movies)
            {
                string description = "this is a video";
                title = movie.Title;
                videoUrl = movie.VideoUrl;
                bgImageUrl = movie.BackgroundImage.ToString();
                cardImageUrl = movie.CardImage.ToString();
                studio = "unknown";

                categoryList.Add(BuildMovieInfo(category_name, title, description, studio,
                    videoUrl, cardImageUrl,
                    bgImageUrl));
            }
            MovieList.Add(category_name, categoryList);

            return MovieList;
        }

        protected MovieJson[] ParseURL (string urlstring)
		{
			Log.Debug (TAG, "Parse URL: " + urlstring);
			InputStream inputStream = null;

			try {
				Java.Net.URL url = new Java.Net.URL (urlstring);
				var urlConnection = url.OpenConnection ();
				inputStream = new BufferedInputStream (urlConnection.InputStream);
				var reader = new BufferedReader (new InputStreamReader (
					             urlConnection.InputStream, "iso-8859-1"), 8);
				var sb = new StringBuilder ();
				string line = null;
				while ((line = reader.ReadLine ()) != null) {
					sb.Append (line);
				}
				var json = sb.ToString ();
                var response = JsonConvert.DeserializeObject<MoviesResponse> (json);
                return response.Results;
			} catch (Exception e) {
				Log.Debug (TAG, "Failed to parse the json for media list", e);
				return null;
			} finally {
				if (null != inputStream) {
					try {
						inputStream.Close ();
					} catch (IOException e) {
						Log.Debug (TAG, "Json feed closed", e);
					}
				}
			}
		}

		private static Movie BuildMovieInfo (string category, string title, string description, 
		                                     string studio, string videoUrl, string cardImageUrl,
		                                     string bgImageUrl)
		{
			var movie = new Movie ();
			movie.Id = Movie.count;
			Movie.incCount ();
			movie.Title = title.Replace ("\"", "");
			movie.Description = description.Replace ("\"", "");
			movie.Studio = studio.Replace ("\"", "");
			movie.Category = category.Replace ("\"", "");
			movie.CardImageUrl = cardImageUrl.Replace ("\"", "");
			movie.BgImageUrl = bgImageUrl.Replace ("\"", "");
			movie.VideoUrl = videoUrl.Replace ("\"", "");
			return movie;
		}
	}
}

