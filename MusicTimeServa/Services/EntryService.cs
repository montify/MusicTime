using MusicTimeServa.Database;
using MusicTimeServa.Model;

namespace MusicTimeServa.Services
{
    public class EntryService : IEntryService
    {
        private EntryDataContext _context;

        public EntryService(EntryDataContext dbContext)
        {
            _context = dbContext;
        }

        public List<Entry> GetAllEntrys()
        {
            var entries = _context.Entries.ToList();
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            try
            {
                _context.Entries.Add(entry);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw new Exception("Cant Add user");
            }
        }

        public void DeleteEntryFromId(int entryId)
        {
            var entry = _context.Entries.Where(i => i.Id == entryId).FirstOrDefault();

            if (entry == null)
                throw new Exception("Entry not exist");

            try
            {
                _context.Entries.Remove(entry);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Cant remove entry");
            }
        }

        public void UpdateEntry(Entry modifiedEntry)
        {
            //TODO: Find a way to Update Specific fields, not just the whole Object,
            //      Exclude Id, Date,... maybe thats why a Object Mapper is useful? ;)
            var entry = _context.Entries.Where(i => i.Id == modifiedEntry.Id).FirstOrDefault();

            if (entry == null)
                throw new Exception("Entry not exist");

            entry.Description = modifiedEntry.Description;
            entry.IsFinished = modifiedEntry.IsFinished;
            entry.DurationInMinutes = modifiedEntry.DurationInMinutes;

            try
            {
                _context.Update(entry);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Cant update Entry");
            }
        }
    }
}