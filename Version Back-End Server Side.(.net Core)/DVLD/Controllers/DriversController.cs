using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/Drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        [HttpGet("GetAllDrivers", Name = "GetAllDrivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DriversListDTO>> GetAllDrivers()
        {
            List<DriversListDTO> DriversList = clsDriver.GetAllLDrivers();

            if (DriversList.Count == 0)
                return NotFound("Not  Drivers Found !");

            return Ok(DriversList);
        }



        [HttpGet("GetDriverByID/{DriverID}", Name = "GetDriverByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriverDTO> GetDriverByID(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepted ID : " + DriverID);

            clsDriver Driver = clsDriver.FindByDriverID(DriverID);

            if (Driver == null)
                return NotFound("Driver With ID : " + DriverID + " Not Found !");

            return Ok(Driver.driverDTO);

        }



        [HttpGet("GetDriverByPersonID/{PersonID}", Name = "GetDriverByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriverDTO> GetDriverByPersonID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            clsDriver Driver = clsDriver.FindByPersonID(PersonID);

            if (Driver == null)
                return NotFound("Driver With Person ID : " + PersonID + " Not Found !");

            return Ok(Driver.driverDTO);

        }


        [HttpGet("DoesDriverIssuedInternationalLicense/{DriverID}", Name = "DoesDriverIssuedInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DoesDriverIssuedInternationalLicense(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepted ID : " + DriverID);

            if(clsDriver.FindByDriverID(DriverID) == null)
                return NotFound("Driver With ID " + DriverID + " Not Found -(");

            bool DoesDriverIssuedInternationalLicense = clsDriver.DoesDriverIssuedInternationalLicense(DriverID);

            if (!DoesDriverIssuedInternationalLicense)
                return NotFound("Driver With ID "+ DriverID + " Not Issued International License -(");

            return Ok("Driver With ID " + DriverID + " has been Issued International License -)");

        }
    }
}
