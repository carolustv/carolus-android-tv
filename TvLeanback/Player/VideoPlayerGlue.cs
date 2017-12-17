using System;
using Android.Content;
using Android.Support.V17.Leanback.Media;
using Android.Support.V17.Leanback.Widget;
using Com.Google.Android.Exoplayer2;

namespace TvLeanback.Player
{
    public class VideoPlayerGlue : PlaybackTransportControlGlue
    {
        public interface OnActionClickedListener
        {
            void OnPrevious();

            void OnNext();
        }

        private readonly OnActionClickedListener actionListener;

        private PlaybackControlsRow.SkipPreviousAction skipPreviousAction;
        private PlaybackControlsRow.SkipNextAction skipNextAction;
        private PlaybackControlsRow.FastForwardAction fastForwardAction;
        private PlaybackControlsRow.RewindAction rewindAction;

        private PlaybackControlsRow.RepeatAction repeatAction;
        private PlaybackControlsRow.ThumbsUpAction thumbsUpAction;
        private PlaybackControlsRow.ThumbsDownAction thumbsDownAction;

        public VideoPlayerGlue(
            Context context,
            LeanbackPlayerAdapter playerAdapter,
            OnActionClickedListener actionListener)
            : base(context, playerAdapter)
        {
            this.actionListener = actionListener;

            skipPreviousAction = new PlaybackControlsRow.SkipPreviousAction(context);
            skipNextAction = new PlaybackControlsRow.SkipNextAction(context);
            fastForwardAction = new PlaybackControlsRow.FastForwardAction(context);
            rewindAction = new PlaybackControlsRow.RewindAction(context);

            thumbsUpAction = new PlaybackControlsRow.ThumbsUpAction(context);
            thumbsUpAction.Index = PlaybackControlsRow.ThumbsUpAction.IndexOutline;
            thumbsDownAction = new PlaybackControlsRow.ThumbsDownAction(context);
            thumbsDownAction.Index = PlaybackControlsRow.ThumbsDownAction.IndexOutline;
            repeatAction = new PlaybackControlsRow.RepeatAction(context);
        }

        protected override void OnCreatePrimaryActions(ArrayObjectAdapter adapter)
        {
            base.OnCreatePrimaryActions(adapter);

            adapter.Add(skipPreviousAction);
			adapter.Add(rewindAction);
            adapter.Add(fastForwardAction);
            adapter.Add(skipNextAction);
        }

        protected override void OnCreateSecondaryActions(ArrayObjectAdapter adapter)
        {
            base.OnCreateSecondaryActions(adapter);
            adapter.Add(thumbsDownAction);
            adapter.Add(thumbsUpAction);
            adapter.Add(repeatAction);
        }

        public override void OnActionClicked(Android.Support.V17.Leanback.Widget.Action action)
        {
            if (ShouldDispatchAction(action)) {
                return;
            }
            base.OnActionClicked(action);
        }

        private bool ShouldDispatchAction(Android.Support.V17.Leanback.Widget.Action action)
        {
            return action == rewindAction
                    || action == fastForwardAction
                    || action == thumbsDownAction
                    || action == thumbsUpAction
                    || action == repeatAction;
        }
    }
}
