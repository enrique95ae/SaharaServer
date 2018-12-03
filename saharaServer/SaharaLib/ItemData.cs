using System;
using ProtoBuf;


/*
 * This file contains:
 *  -Item data model.
 *  -This will hold the item data coming from the client or ready to be sent to the client.
 * 
 * 
 * 
 * 
 */

namespace SaharaLib
{
    [ProtoContract]
    public class GetItemDataEvent : BaseEvent
    {
        [ProtoMember(1)]
        public string ItemTitle { get; set; }

        [ProtoMember(2)]
        public string ItemDescription { get; set; }

        [ProtoMember(3)]
        public double ItemPrice { get; set; }

        public GetItemDataEvent()
        {
            Type = EventType.GetItemData;
            ItemTitle = null;
            ItemDescription = null;
            ItemPrice = 0.0;
        }

        public GetItemDataEvent(string title, string description, double price)
        {
            Type = EventType.GetItemData;
            ItemTitle = title;
            ItemDescription = description;
            ItemPrice = price;
        }
    }
}
