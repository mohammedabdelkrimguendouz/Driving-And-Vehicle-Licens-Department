using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/Violations")]
    [ApiController]
    public class ViolationsController : ControllerBase
    {
        [HttpGet("GetAllViolations", Name = "GetAllViolations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ViolationDTO>> GetAllViolations()
        {
            List<ViolationDTO> ViolationsList = clsViolation.GetAllViolations();

            if (ViolationsList.Count == 0)
                return NotFound("Not  Violations Found !");

            return Ok(ViolationsList);
        }



        [HttpGet("GetViolationByID/{ViolationID}", Name = "GetViolationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ViolationDTO> GetViolationByID(int ViolationID)
        {
            if (ViolationID < 1)
                return BadRequest("Not Accepted ID : " + ViolationID);

            clsViolation Violation = clsViolation.Find(ViolationID);

            if (Violation == null)
                return NotFound("Violation With ID : " + ViolationID + " Not Found !");

            return Ok(Violation.violationDTO);

        }



        [HttpGet("GetViolationByTitle/{ViolationTitle}", Name = "GetViolationByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ViolationDTO> GetViolationByTitle(string ViolationTitle)
        {


            clsViolation Violation = clsViolation.Find(ViolationTitle);

            if (Violation == null)
                return NotFound("Violation With  ViolationName : " + ViolationTitle + " Not Found !");

            return Ok(Violation.violationDTO);

        }



        [HttpPut("UpdateViolation/{ViolationID}", Name = "UpdateViolation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ViolationDTO> UpdateViolation(int ViolationID, ViolationDTO UpdatedViolationDTO)
        {
            if (UpdatedViolationDTO == null || string.IsNullOrEmpty(UpdatedViolationDTO.ViolationTitle) ||
                 !clsValidation.IsNumber(Convert.ToString(UpdatedViolationDTO.FineFees)) || ViolationID < 1)

                return BadRequest("Invalid Violation Data !");


            clsViolation Violation = clsViolation.Find(ViolationID);

            if (Violation == null)
                return NotFound("Violation With ID: " + ViolationID + " Not Found!");

            if (Violation.ViolationTitle!=UpdatedViolationDTO.ViolationTitle &&
                clsViolation.IsViolationExist(UpdatedViolationDTO.ViolationTitle))

                return BadRequest("Violation Title: " + UpdatedViolationDTO.ViolationTitle + " already exist!");



            Violation.ViolationDescription=UpdatedViolationDTO.ViolationDescription;
            Violation.ViolationTitle=UpdatedViolationDTO.ViolationTitle;
            Violation.FineFees=UpdatedViolationDTO.FineFees;    
            

            if (!Violation.Save())
                return StatusCode(409, "Error Updating Violation");


            return Ok(Violation.violationDTO);

        }


        [HttpGet("IsViolationExistByID/{ViolationID}", Name = "IsViolationExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsViolationExistByID(int ViolationID)
        {
            if (ViolationID < 1)
                return BadRequest("Not Accepted ID : " + ViolationID);

            bool IsViolationExist = clsViolation.IsViolationExist(ViolationID);

            if (!IsViolationExist)
                return NotFound("Violation With  ID : " + ViolationID + " Not Found -(");

            return Ok("Violation With  ID : " + ViolationID + " Exist -)");

        }


        [HttpGet("IsViolationExistByViolationName/{ViolationName}", Name = "IsViolationExistByViolationName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult IsViolationExistByTitle(string ViolationTitle)
        {


            bool IsViolationExist = clsViolation.IsViolationExist(ViolationTitle);

            if (!IsViolationExist)
                return NotFound("Violation With  ViolationTitle : " + ViolationTitle + " Not Found -(");

            return Ok("Violation With  ViolationTitle : " + ViolationTitle + " Exist -)");

        }
    }
}
