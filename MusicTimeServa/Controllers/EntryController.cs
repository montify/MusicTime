using Microsoft.AspNetCore.Mvc;
using MusicTimeServa.Model;
using MusicTimeServa.Services;

namespace MusicTimeServa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController : ControllerBase
    {
        public IEntrieService m_entrieService { get; set; }

        public EntryController(IEntrieService entrie)
        {
            m_entrieService = entrie;
        }

        [HttpGet()]
        public ActionResult<List<Entry>> GetEntry()
        {
            var entries = m_entrieService.GetAllEntrys();

            if(entries == null)
                return StatusCode(StatusCodes.Status204NoContent, "Cant fetch entrys");

            
            return StatusCode(StatusCodes.Status200OK, entries); 
        }
        
        [HttpPost()]
        public ActionResult AddEntry(Entry entry)
        {
            //TODO Detect if any field is empy not just the object itself is null
            if (entry.CheckIfValid() is false)
                return StatusCode(StatusCodes.Status400BadRequest, "Entry cant be null");

            try
            {
                m_entrieService.AddEntry(entry);
                return StatusCode(StatusCodes.Status201Created, $"Entry: {entry.Description} added!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete()]
        public ActionResult DeleteEntry(int id)
        {
            if(id <= 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Id must be > 0");

            try
            {
                m_entrieService.DeleteEntryFromId(id);
                return StatusCode(StatusCodes.Status200OK, "Entry Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateEntry(Entry entry)
        {
            if(entry == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Entry cant be null");

            try
            {
                m_entrieService.UpdateUser(entry);
                return StatusCode(StatusCodes.Status200OK, "Entry Updated");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}
