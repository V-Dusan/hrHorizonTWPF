using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrHorizonT.UI.Event
{
    class AfterDetailClosedEvent : PubSubEvent<AfterDetailClosedEventArgs>
    {
    }

    public class AfterDetailClosedEventArgs
    {
        public int Id { get; set; }

        public string ViewModelName { get; set; }
    }
}
