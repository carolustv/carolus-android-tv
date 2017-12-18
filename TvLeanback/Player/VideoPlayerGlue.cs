using Android.Content;
using Android.Support.V17.Leanback.Media;
using Android.Support.V17.Leanback.Widget;
using Java.Util.Concurrent;

namespace TvLeanback.Player
{
    public class VideoPlayerGlue : PlaybackTransportControlGlue
    {
        public interface OnActionClickedListener
        {
            void OnPrevious();

            void OnNext();
        }

        static readonly long tenSeconds = TimeUnit.Seconds.ToMillis(10);

        readonly OnActionClickedListener actionListener;

        PlaybackControlsRow.SkipPreviousAction skipPreviousAction;
        PlaybackControlsRow.SkipNextAction skipNextAction;
        PlaybackControlsRow.FastForwardAction fastForwardAction;
        PlaybackControlsRow.RewindAction rewindAction;

        PlaybackControlsRow.RepeatAction repeatAction;
        PlaybackControlsRow.ThumbsUpAction thumbsUpAction;
        PlaybackControlsRow.ThumbsDownAction thumbsDownAction;

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
            thumbsUpAction.Index = PlaybackControlsRow.ThumbsAction.IndexOutline;
            thumbsDownAction = new PlaybackControlsRow.ThumbsDownAction(context);
            thumbsDownAction.Index = PlaybackControlsRow.ThumbsAction.IndexOutline;
            repeatAction = new PlaybackControlsRow.RepeatAction(context);
        }

        protected override void OnCreatePrimaryActions(ArrayObjectAdapter primaryActionsAdapter)
        {
            base.OnCreatePrimaryActions(primaryActionsAdapter);

            primaryActionsAdapter.Add(skipPreviousAction);
            primaryActionsAdapter.Add(rewindAction);
            primaryActionsAdapter.Add(fastForwardAction);
            primaryActionsAdapter.Add(skipNextAction);
        }

        protected override void OnCreateSecondaryActions(ArrayObjectAdapter secondaryActionsAdapter)
        {
            base.OnCreateSecondaryActions(secondaryActionsAdapter);
            secondaryActionsAdapter.Add(thumbsDownAction);
            secondaryActionsAdapter.Add(thumbsUpAction);
            secondaryActionsAdapter.Add(repeatAction);
        }

        public override void OnActionClicked(Android.Support.V17.Leanback.Widget.Action action)
        {
            if (ShouldDispatchAction(action))
            {
                return;
            }
            base.OnActionClicked(action);
        }

        bool ShouldDispatchAction(Android.Support.V17.Leanback.Widget.Action action)
        {
            return action == rewindAction
                    || action == fastForwardAction
                    || action == thumbsDownAction
                    || action == thumbsUpAction
                    || action == repeatAction;
        }

        void NotifyActionChanged(PlaybackControlsRow.MultiAction action, ArrayObjectAdapter adapter)
        {
            if (adapter != null)
            {
                int index = adapter.IndexOf(action);
                if (index >= 0)
                {
                    adapter.NotifyArrayItemRangeChanged(index, 1);
                }
            }
        }

        public override void Next()
        {
            actionListener.OnNext();
        }

        public override void Previous()
        {
            actionListener.OnPrevious();
        }

        public void Rewind()
        {
            long newPosition = CurrentPosition - tenSeconds;
            newPosition = (newPosition < 0) ? 0 : newPosition;
            ((LeanbackPlayerAdapter)PlayerAdapter).SeekTo(newPosition);
        }

        public void FastForward()
        {
            if (Duration > -1)
            {
                long newPosition = CurrentPosition + tenSeconds;
                newPosition = (newPosition > Duration) ? Duration : newPosition;
                ((LeanbackPlayerAdapter)PlayerAdapter).SeekTo(newPosition);
            }
        }
    }
}
