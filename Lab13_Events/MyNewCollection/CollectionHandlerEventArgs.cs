using System;

namespace MyNewCollectionLib
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public object ChangedItem { get; set; }


        public CollectionHandlerEventArgs(string collectionName, string changeType, object changedItem)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedItem = changedItem;
        }
    }
}
