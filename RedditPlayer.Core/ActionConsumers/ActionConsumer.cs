using System;
using RedditPlayer.Models;
using RedditPlayer.Actions;
namespace RedditPlayer.ActionConsumers
{
    public interface IActionConsumer
    {
        bool TestAction(IAction action);
        void Reduce(StateStore store, ApplicationState state, IAction action);
    }

    public abstract class ActionConsumer<TAction> : IActionConsumer where TAction : IAction
    {
        public bool TestAction (IAction action)
        {
            return action is TAction;
        }

        public abstract void Reduce(StateStore store, ApplicationState state, TAction action);

        public void Reduce(StateStore store, ApplicationState state, IAction action)
        {
            Reduce(store, state, (TAction)action);
        }
    }
}

