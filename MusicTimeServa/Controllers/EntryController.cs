using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicTimeServa.Model;
using MusicTimeServa.Services;

namespace MusicTimeServa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController : ControllerBase
    {
        public IEntryService m_entrieService { get; set; }

        public EntryController(IEntryService entrie)
        {
            m_entrieService = entrie;
        }

        [HttpGet()]
      //  [Authorize]
        public ApiResponse GetEntry()
        {
            var entries = m_entrieService.GetAllEntrys();
           
            if (entries == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No Entry in Database",
                    Result = null
                };


            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Success",
                Result = entries
            };
        }

        [HttpPost()]
        public ApiResponse AddEntry(Entry entry)
        {
            //TODO Detect if any field is empy not just the object itself is null
            if (entry.CheckIfValid() is false)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Entry cant be null!",
                    Result = null
                };
            }

            try
            {
                m_entrieService.AddEntry(entry);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = entry
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Result = null
                };
            }
        }

        [HttpDelete()]
        public ApiResponse DeleteEntry(int id)
        {
            if (id <= 0)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Id must be > 0",
                    Result = null
                };

            try
            {
                m_entrieService.DeleteEntryFromId(id);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Entry Deleted",
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Result = null
                };
            }
        }

        [HttpPut]
        public ApiResponse UpdateEntry(Entry entry)
        {
            if (entry == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Entry cant be null",
                    Result = null
                };
            try
            {
                m_entrieService.UpdateUser(entry);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Entry Updated!",
                    Result = entry
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Result = null
                };
            }
        }
    }
}