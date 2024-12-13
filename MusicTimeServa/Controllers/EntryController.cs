using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IEntryService m_entrieService;
        private readonly IMapper m_Mapper;

        public EntryController(IEntryService entryService, IMapper mapper)
        {
            m_entrieService = entryService;
            m_Mapper = mapper;
        }

        [HttpGet()] 
        public ApiResponse GetEntry()
        {
            var entries = m_entrieService.GetAllEntrys();
           
            if (entries == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No Entry in Database",
                    Payload = null
                };


            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Success",
                Payload = entries
            };
        }

        [HttpPost()]
        public ApiResponse AddEntry(AddEntryRequestDTO entryDto)
        {
            var entry = m_Mapper.Map<Entry>(entryDto);

            //TODO Detect if any field is empy not just the object itself is null
            if (entry.CheckIfValid() is false)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Entry cant be null!",
                    Payload = null
                };
            }

            try
            {
                m_entrieService.AddEntry(entry);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Success",
                    Payload = entry
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Payload = null
                };
            }
        }

        [HttpDelete()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse DeleteEntry(int id)
        {
            if (id <= 0)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Id must be > 0",
                    Payload = null
                };

            try
            {
                m_entrieService.DeleteEntryFromId(id);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Entry Deleted",
                    Payload = null
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Payload = null
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
                    Payload = null
                };
            try
            {
                m_entrieService.UpdateEntry(entry);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Entry Updated!",
                    Payload = entry
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Payload = null
                };
            }
        }
    }
}