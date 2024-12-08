using MusicTimeServa.Model;

namespace MusicTimeServa.Services
{
    public class EntryService : IEntryService
    {
        private DataContext _context;

        public EntryService(DataContext dbContext)
        {
            _context = dbContext;
        }
        public List<Entry> GetAllEntrys()
        {
            var entries = _context.Set<Entry>().ToList();
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            try
            {
                _context.Add(entry);
                _context.SaveChanges();
              
            }
            catch (Exception)
            {
                throw new Exception("Cant delete user");
            }
        }

        public void DeleteEntryFromId(int entryId)
        {
            var user = _context.Set<Entry>().Where(i => i.Id == entryId).FirstOrDefault();

            if (user == null)
                throw new Exception("User not exist");

            _context.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(Entry modifiedEntry)
        {
            //TODO: Find a way to Update Specific fields, not just the whole Object,
            //      Exclude Id, Date,... maybe thats why a Object Mapper is useful? ;)
            var entry = _context.Set<Entry>().Where(i => i.Id == modifiedEntry.Id).FirstOrDefault();

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
