using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace hrHorizonT.UI.Event
{
    public class AfterCollectionSavedEvent : PubSubEvent<AfterCollectionSavedEventArgs>
    {
    }

    public class AfterCollectionSavedEventArgs
    {
        public string ViewModelName { get; set; }
    }
}
