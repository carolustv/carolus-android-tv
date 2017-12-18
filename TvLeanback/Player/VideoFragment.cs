using Android.OS;
using Android.Support.V17.Leanback.App;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Util;
using TvLeanback.Models;
using TvLeanback.Player;

namespace TvLeanback
{
    public class PlaybackFragment : VideoFragment
    {
        private const int UPDATE_DELAY = 16;

        VideoPlayerGlue playerGlue;
        SimpleExoPlayer player;
        TrackSelector trackSelector;
        LeanbackPlayerAdapter playerAdapter;
        PlaylistActionListener playlistActionListener;
        VideoLoaderCallbacks videoLoaderCallbacks;
        Playlist playlist;

        Movie movie;

        public PlaybackFragment()
        {
        }

        public override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            movie = (Movie)Activity.Intent.GetParcelableExtra("Movie");
            playlist = new Playlist();

            videoLoaderCallbacks = new VideoLoaderCallbacks(playlist);

            // Loads the playlist.
            var args = new Bundle();
            args.PutString("Category", "Movies");
            LoaderManager.InitLoader(2, args, videoLoaderCallbacks);
        }

        public override void OnStart()
        {
            base.OnStart();

            if (Util.SdkInt > 23) {
                initializePlayer();
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            if ((Util.SdkInt <= 23 || player == null))
            {
                initializePlayer();
            }
        }

        public override void OnPause()
        {
            base.OnPause();

            if (playerGlue != null && playerGlue.IsPlaying)
            {
                playerGlue.Pause();
            }
            if (Util.SdkInt <= 23)
            {
                releasePlayer();
            }
        }

        public override void OnStop()
        {
            base.OnStop();
            if (Util.SdkInt > 23)
            {
                releasePlayer();
            }
        }

        public void Play(Movie movie)
        {
            playerGlue.Title = movie.Title;
            playerGlue.Play();
        }

        void initializePlayer()
        {
            var bandwidthMeter = new DefaultBandwidthMeter();
            var videoTrackSelectionFactory =
                    new AdaptiveTrackSelection.Factory(bandwidthMeter);
            trackSelector = new DefaultTrackSelector(videoTrackSelectionFactory);

            player = ExoPlayerFactory.NewSimpleInstance(Activity, trackSelector);
            playerAdapter = new LeanbackPlayerAdapter(Activity, player, UPDATE_DELAY);
            playlistActionListener = new PlaylistActionListener(this, playlist);
            playerGlue = new VideoPlayerGlue(Activity, playerAdapter, playlistActionListener);
            playerGlue.Host = new VideoFragmentGlueHost(this);
            //playerGlue.IsReadyForPlayback();

            Play(movie);

            //var rowsAdapter = initializeRelatedVideosRow();
            //Adapter = rowsAdapter;
        }


        void releasePlayer()
        {
            if (player != null)
            {
                player.Release();
                player = null;
                trackSelector = null;
                playerGlue = null;
                playerAdapter = null;
                playlistActionListener = null;
            }
        }
    }
}
