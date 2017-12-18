using TvLeanback.Models;

namespace TvLeanback.Player
{
    public class PlaylistActionListener : VideoPlayerGlue.OnActionClickedListener
    {
        Playlist playlist;
        PlaybackFragment fragment;

        public PlaylistActionListener(PlaybackFragment fragment, Playlist playlist)
        {
            this.fragment = fragment;
            this.playlist = playlist;
        }

        public void OnPrevious()
        {
            fragment.Play(playlist.Previous());
        }

        public void OnNext()
        {
            fragment.Play(playlist.Next());
        }
    }
}
