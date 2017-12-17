using System;
using Android.Support.V17.Leanback.App;
using Com.Google.Android.Exoplayer2;

namespace TvLeanback
{
    public class PlaybackFragment : VideoFragment
    {
        private const int UPDATE_DELAY = 16;

        //private VideoPlayerGlue mPlayerGlue;
        private SimpleExoPlayer mPlayer;
        //private TrackSelector mTrackSelector;

        private Movie mMovie;

        public PlaybackFragment()
        {
        }

        public override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnStop()
        {
            base.OnStop();
        }
    }
}
