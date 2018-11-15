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
    public class ItemData : BaseEvent
    {
        [ProtoMember(1)]
        public int? ID { get; set; }

        [ProtoMember(2)]
        public string ItemName { get; set; }

        [ProtoMember(3)]
        public int? Quantity { get; set; }

        public ItemData()
        {
            Type = EventType.GetItemData;
            ID = null;
            ItemName = null;
            Quantity = null;
        }

        public ItemData(int id, string itemName, int quantity)
        {
            Type = EventType.GetItemData;
            ID = id;
            ItemName = itemName;
            Quantity = quantity;
        }
    }
}
