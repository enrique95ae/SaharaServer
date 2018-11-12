using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class ResponseEvent : BaseEvent
    {

        [ProtoMember(1)]
        public bool EventProcessSuccess { get; set; }

        public ResponseEvent()
        {
            Type = EventType.Response;
        }

        public ResponseEvent(bool processSuccess)
        {
            Type = EventType.Response;
            //EventProcessSuccess = processSuccess;
            EventProcessSuccess = true;
        }

    }
}