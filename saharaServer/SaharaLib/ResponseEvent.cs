 using ProtoBuf;

/*
 * This file contains:
 *      -Object type definition for the varaible that will be creating during server-client responses exchange.
 * 
 */

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
            EventProcessSuccess = processSuccess;
           //EventProcessSuccess = true;
        }

    }
}