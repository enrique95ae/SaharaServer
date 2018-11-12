using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class ResponseEvent : BaseEvent
    {
        /*
        [ProtoMember(1)]
        public int _ErrorCode { get; set; }
        
        [ProtoMember(2)]
        public string _ErrorMessage { get; set; }
        */

        [ProtoMember(1)]
        public bool EventProcessSuccess { get; set; }

        public ResponseEvent()
        {
            Type = EventType.Response;
        }

        public ResponseEvent(bool processSuccess)
        {
            Type = EventType.Response;
            EventProcessSuccess = processSuccess;
        }

        /*
        public ResponseEvent(int errorCode, string errorMessage, bool eventSuccess)
        {
            Type                = EventType.Response;
            _ErrorCode           = errorCode;
            _ErrorMessage        = errorMessage;
            _EventProcessSuccess = eventSuccess;
        }
        */
    }
}