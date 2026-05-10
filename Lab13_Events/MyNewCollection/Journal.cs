using System.Text;

namespace MyNewCollectionLib
{
    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();


        public void CollectionEventHandler(object source, CollectionHandlerEventArgs args)
        {
            JournalEntry entry = new JournalEntry(
                args.CollectionName,
                args.ChangeType,
                args.ChangedItem.ToString() ?? "null"
            );

            entries.Add(entry);
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (JournalEntry entry in entries)
            {
                sb.AppendLine(entry.ToString());
            }

            return sb.ToString();
        }
    }
}