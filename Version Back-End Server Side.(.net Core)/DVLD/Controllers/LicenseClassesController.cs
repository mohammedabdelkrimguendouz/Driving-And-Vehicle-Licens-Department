using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/LicenseClasses")]
    [ApiController]
    public class LicenseClassesController : ControllerBase
    {
        [HttpGet("GetAllLicenseClasses", Name = "GetAllLicenseClasses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LicenseClassDTO>> GetAllLicenseClasses()
        {
            List<LicenseClassDTO> LicenseClassesList = clsLicenseClass.GetAllLicenseClasses();

            if (LicenseClassesList.Count == 0)
                return NotFound("Not  License Classes Found !");

            return Ok(LicenseClassesList);
        }



        [HttpGet("GetLicenseClasseByID/{LicenseClasseID}", Name = "GetLicenseClasseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LicenseClassDTO> GetLicenseClasseByID(int LicenseClasseID)
        {
            if (LicenseClasseID < 1)
                return BadRequest("Not Accepted ID : " + LicenseClasseID);

            clsLicenseClass LicenseClasse = clsLicenseClass.Find(LicenseClasseID);

            if (LicenseClasse == null)
                return NotFound("LicenseClasse With ID : " + LicenseClasseID + " Not Found !");

            return Ok(LicenseClasse.licenseClassDTO);

        }




        [HttpGet("GetLicenseClasseByName/{LicenseClasseName}", Name = "GetLicenseClasseByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseClassDTO> GetLicenseClasseByName(string LicenseClasseName)
        {

            clsLicenseClass LicenseClasse = clsLicenseClass.Find(LicenseClasseName);

            if (LicenseClasse == null)
                return NotFound("LicenseClasse With  LicenseClasseName : " + LicenseClasseName + " Not Found !");

            return Ok(LicenseClasse.licenseClassDTO);

        }



        [HttpPut("UpdateLicenseClasse/{LicenseClasseID}", Name = "UpdateLicenseClasse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseClassDTO> UpdateLicenseClasse(int LicenseClasseID, LicenseClassDTO UpdatedLicenseClasseDTO)
        {
            if (UpdatedLicenseClasseDTO == null || string.IsNullOrEmpty(UpdatedLicenseClasseDTO.ClassName) ||
                 !clsValidation.IsNumber(Convert.ToString(UpdatedLicenseClasseDTO.ClassFees)) || LicenseClasseID < 1 || UpdatedLicenseClasseDTO.DefaultValidityLength<=0 ||
                 UpdatedLicenseClasseDTO.MinimumAllowedAge<18)

                return BadRequest("Invalid License Classe Data !");


            clsLicenseClass LicenseClasse = clsLicenseClass.Find(LicenseClasseID);

            if (LicenseClasse == null)
                return NotFound("LicenseClasse With ID: " + LicenseClasseID + " Not Found!");


            LicenseClasse.DefaultValidityLength=UpdatedLicenseClasseDTO.DefaultValidityLength;
            LicenseClasse.MinimumAllowedAge=UpdatedLicenseClasseDTO.MinimumAllowedAge;
            LicenseClasse.ClassDescription=UpdatedLicenseClasseDTO.ClassDescription;
            LicenseClasse.ClassName=UpdatedLicenseClasseDTO.ClassName;
            LicenseClasse.ClassFees=UpdatedLicenseClasseDTO.ClassFees;
            

            if (!LicenseClasse.Save())
                return StatusCode(409, "Error Updating LicenseClasse");


            return Ok(LicenseClasse.licenseClassDTO);

        }
    }
}
