using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/ApplicationTypes")]
    [ApiController]
    public class ApplicationTypesController : ControllerBase
    {
        [HttpGet("GetAllApplicationTypes", Name = "GetAllApplicationTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationTypeDTO>> GetAllApplicationTypes()
        {
            List<ApplicationTypeDTO> ApplicationTypesList = clsApplicationType.GetAllApplicationTypes();

            if (ApplicationTypesList.Count == 0)
                return NotFound("Not  ApplicationTypes Found !");

            return Ok(ApplicationTypesList);
        }



        [HttpGet("GetApplicationTypeByID/{ApplicationTypeID}", Name = "GetApplicationTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApplicationTypeDTO> GetApplicationTypeByID(int ApplicationTypeID)
        {
            if (ApplicationTypeID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationTypeID);

            clsApplicationType ApplicationType = clsApplicationType.Find(ApplicationTypeID);

            if (ApplicationType == null)
                return NotFound("ApplicationType With ID : " + ApplicationTypeID + " Not Found !");

            return Ok(ApplicationType.applicationTypeDTO);

        }




        [HttpPut("UpdateApplicationType/{ApplicationTypeID}", Name = "UpdateApplicationType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationTypeDTO> UpdateApplicationType(int ApplicationTypeID, ApplicationTypeDTO UpdatedApplicationTypeDTO)
        {
            if (UpdatedApplicationTypeDTO == null || string.IsNullOrEmpty(UpdatedApplicationTypeDTO.ApplicationTypeTitle) ||
                  ApplicationTypeID < 1 || !clsValidation.IsNumber(Convert.ToString(UpdatedApplicationTypeDTO.ApplicationFees)))

                return BadRequest("Invalid ApplicationType Data !");


            clsApplicationType ApplicationType = clsApplicationType.Find(ApplicationTypeID);

            if (ApplicationType == null)
                return NotFound("ApplicationType With ID: " + ApplicationTypeID + " Not Found!");




            ApplicationType.ApplicationTypeTitle = UpdatedApplicationTypeDTO.ApplicationTypeTitle;
            ApplicationType.ApplicationFees = UpdatedApplicationTypeDTO.ApplicationFees;
            

            if (!ApplicationType.Save())
                return StatusCode(409, "Error Updating ApplicationType");


            return Ok(ApplicationType.applicationTypeDTO);

        }
    }
}
