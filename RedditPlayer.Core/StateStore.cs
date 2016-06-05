using System;
using System.Collections.Generic;
using RedditPlayer.ActionConsumers;
using RedditPlayer.Actions;
using RedditPlayer.Models;
using System.Linq;
namespace RedditPlayer
{
    public class StateStore
    {
        ApplicationState state;
        List<IActionConsumer> consumers;

        public StateStore(ApplicationState state)
        {
            this.state = state;
            consumers = new List<IActionConsumer>();
        }

        public void RegisterConsumer (IActionConsumer consumer)
        {
            consumers.Add(consumer);
        }

        public void Dispatch (IAction action)
        {
            foreach (var consumer in consumers) 
            {
                if (consumer.TestAction(action)) {
                    consumer.Reduce(this, state, action);
                }
            }
        }
    }
}

