using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/DetainedLicenses")]
    [ApiController]
    public class DetainedLicensesController : ControllerBase
    {

        [HttpGet("GetAllDetainedLicenses", Name = "GetAllDetainedLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DetainedLicensesListDTO>> GetAllDetainedLicenses()
        {
            List<DetainedLicensesListDTO> DetainedLicensesList = clsDetainedLicense.GetAllDetainedLicenses();

            if (DetainedLicensesList.Count == 0)
                return NotFound("Not  Detained Licenses Found !");

            return Ok(DetainedLicensesList);
        }


        [HttpGet("IsLicenseDetained/{LicenseID}", Name = "IsLicenseDetained")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsLicenseDetained(int LicenseID)
        {
            if (LicenseID < 1)
                return BadRequest("Not Accepted ID : " + LicenseID);

            if(clsLicense.FindByLicenseID(LicenseID)==null)
                return NotFound("License with ID : " + LicenseID+" Not Found");

            return Ok(clsDetainedLicense.IsLicenseDetained(LicenseID));

        }

        [HttpGet("GetDetainedLicenseByID/{DetainedID}", Name = "GetDetainedLicenseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DetainedLicenseDTO> GetDetainedLicenseByID(int DetainedID)
        {
            if (DetainedID < 1)
                return BadRequest("Not Accepted ID : " + DetainedID);

            clsDetainedLicense DetainedLicense = clsDetainedLicense.FindByDetainedID(DetainedID);

            if (DetainedLicense == null)
                return NotFound("Detained License With ID : " + DetainedID + " Not Found !");

            return Ok(DetainedLicense.detainedLicenseDTO);

        }





    }
}
