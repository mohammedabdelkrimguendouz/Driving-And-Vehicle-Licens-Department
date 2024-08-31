using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace DVLD.Controllers
{
    [Route("api/Licenses")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        [HttpGet("GetAllLicenses", Name = "GetAllLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LicenseDTO>> GetAllLicenses()
        {
            List<LicenseDTO> LicensesList = clsLicense.GetAllLicenses();

            if (LicensesList.Count == 0)
                return NotFound("Not  Licenses Found !");

            return Ok(LicensesList);
        }


        [HttpGet("GetDriverLocalLicenses/{DriverID}", Name = "GetDriverLocalLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<DriverLocalLicensesDTO>> GetDriverLocalLicenses(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepte Id "+DriverID);

            if(clsDriver.FindByDriverID(DriverID) == null)
                return NotFound("Driver With ID "+DriverID+" Not Found !");

            List<DriverLocalLicensesDTO> DriverLocalLicenses = clsLicense.GetDriverLocalLicenses(DriverID);

            if (DriverLocalLicenses.Count == 0)
                return NotFound("Not Driver Local Licenses For Driver With ID "+DriverID);

            return Ok(DriverLocalLicenses);
        }



        [HttpGet("GetInternationalLicenses/{DriverID}", Name = "GetInternationalLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<InternationalLicensesListDTO>> GetInternationalLicenses(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepte Id " + DriverID);

            if (clsDriver.FindByDriverID(DriverID) == null)
                return NotFound("Driver With ID " + DriverID + " Not Found !");

            List<InternationalLicensesListDTO> InternationalLicensesList = clsLicense.GetInternationalLicenses(DriverID);

            if (InternationalLicensesList.Count == 0)
                return NotFound("Not International Licenses For Driver With ID " + DriverID);

            return Ok(InternationalLicensesList);
        }



        [HttpGet("GetLicenseByID/{LicenseID}", Name = "GetLicenseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LicenseDTO> GetLicenseByID(int LicenseID)
        {
            if (LicenseID < 1)
                return BadRequest("Not Accepted ID : " + LicenseID);

            clsLicense License = clsLicense.FindByLicenseID(LicenseID);

            if (License == null)
                return NotFound("License With ID : " + LicenseID + " Not Found !");

            return Ok(License.licenseDTO);

        }


        [HttpGet("GetActiveLicenseIDByPersonID/{PersonID},{LicenseClassID}", Name = "GetActiveLicenseIDByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetActiveLicenseIDByPersonID(int PersonID,int LicenseClassID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted Person with ID : " + PersonID);

            if (LicenseClassID < 1)
                return BadRequest("Not Accepted LicenseClass with ID : " + LicenseClassID);


            if (!clsPerson.IsPersonExist(PersonID))
                NotFound("Person With ID " + PersonID + " Not Found");

            if (clsLicenseClass.Find(LicenseClassID)==null)
                NotFound("LicenseClass With ID " + LicenseClassID + " Not Found");


            int ActiveLicenseIDByPersonID = clsLicense.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

            return Ok((ActiveLicenseIDByPersonID==-1)?null: ActiveLicenseIDByPersonID);

        }



        [HttpGet("IsLicenseExistByPersonID/{PersonID},{LicenseClassID}", Name = "IsLicenseExistByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted Person with ID : " + PersonID);

            if (LicenseClassID < 1)
                return BadRequest("Not Accepted LicenseClass with ID : " + LicenseClassID);


            if (!clsPerson.IsPersonExist(PersonID))
                NotFound("Person With ID " + PersonID + " Not Found");

            if (clsLicenseClass.Find(LicenseClassID) == null)
                NotFound("LicenseClass With ID " + LicenseClassID + " Not Found");


            bool IsLicenseExistByPersonID = clsLicense.IsLicenseExistByPersonID(PersonID, LicenseClassID);

            

            if (!IsLicenseExistByPersonID)
                return NotFound(" License with LicenseClassID "+LicenseClassID+"Not Found For Person with id "+PersonID);

            return Ok(" License with LicenseClassID " + LicenseClassID + "Exist For Person with id " + PersonID +"-)");

        }

        [HttpGet("IsLicenseExpired/{LicenseID}", Name = "IsLicenseExpired")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsLicenseExpired(int LicenseID)
        {
            if (LicenseID < 1)
                return BadRequest("Not Accepted ID : " + LicenseID);

           clsLicense license=clsLicense.FindByLicenseID(LicenseID);

            if (license==null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");



            return Ok(license.IsLicenseExpired());

        }



        [HttpGet("DeactivateLicense/{LicenseID}", Name = "DeactivateLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeactivateLicense(int LicenseID)
        {
            if (LicenseID < 1)
                return BadRequest("Not Accepted ID : " + LicenseID);

            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");

            if(!license.IsActive)
                return BadRequest("License With id "+LicenseID+"Is not active");

            if (!license.DeactivateCurrentLicense())
                return StatusCode(409, "Error Deactivate License ");

            return Ok("License With ID "+LicenseID+ " has been Deactivate Succefully");

        }




        [HttpGet("RenewLicense/{LicenseID},{CreatedByUserID},{Notes}", Name = "RenewLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<LicenseDTO> RenewLicense(int LicenseID,int CreatedByUserID,string Notes)
        {
            if (LicenseID < 1 || CreatedByUserID<1)
                return BadRequest("Invalide Info");

            if(!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");


            if (license.IsDetained)
                return BadRequest("License With Id " + LicenseID + " is Detained");

            if (!license.IsLicenseExpired())
                return BadRequest("License With Id "+LicenseID+ " is not Expired");

            clsLicense NewLicense = license.RenewLicense(CreatedByUserID, Notes);

            if (NewLicense == null)
                return StatusCode(409, "Error Renew License ");

            return Ok(NewLicense.licenseDTO);

        }





        [HttpGet("DetainLicense/{LicenseID},{CreatedByUserID},{FineFees},{ViolationID}", Name = "DetainLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<DetainedLicenseDTO> DetainLicense(int LicenseID, int CreatedByUserID, float FineFees,int ViolationID)
        {
            if (LicenseID < 1|| CreatedByUserID < 1 || !clsValidation.IsNumber(Convert.ToString(FineFees))||ViolationID<1)
                return BadRequest("Invalide Info");


            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");

            if(!clsViolation.IsViolationExist(ViolationID))
                return NotFound("Violation With  ID : " + ViolationID + " Not Found -(");



            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");


            if (license.IsDetained)
                return BadRequest("License With Id " + LicenseID + " is Detained");



            clsDetainedLicense detainedLicense = license.DetainLicense(CreatedByUserID, FineFees, ViolationID);

            if (detainedLicense == null)
                return StatusCode(409, "Error detained License ");

            return Ok(detainedLicense.detainedLicenseDTO);

        }



        [HttpGet("ReplacementForLost/{LicenseID},{CreatedByUserID}", Name = "ReplacementForLost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<LicenseDTO> ReplacementForLost(int LicenseID, int CreatedByUserID)
        {
            if (LicenseID < 1 || CreatedByUserID < 1)
                return BadRequest("Invalide Info");

           

            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");


            if (license.IsDetained)
                return BadRequest("License With Id " + LicenseID + " is Detained");


            clsLicense NewLicense = license.ReplaceLicense(CreatedByUserID, clsLicense.enIssueReason.LostReplacement);

            if (NewLicense == null)
                return StatusCode(409, "Error Replace License ");

            return Ok(NewLicense.licenseDTO);

        }

        [HttpGet("ReplacementForDamaged/{LicenseID},{CreatedByUserID}", Name = "ReplacementForDamaged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<LicenseDTO> ReplacementForDamaged(int LicenseID, int CreatedByUserID)
        {
            if (LicenseID < 1 || CreatedByUserID < 1)
                return BadRequest("Invalide Info");

           
            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");


            if (license.IsDetained)
                return BadRequest("License With Id " + LicenseID + " is Detained");


            clsLicense NewLicense = license.ReplaceLicense(CreatedByUserID, clsLicense.enIssueReason.DamagedReplacement);

            if (NewLicense == null)
                return StatusCode(409, "Error Replace License ");

            return Ok(NewLicense.licenseDTO);

        }


        [HttpGet("ReleaseDetainedLicense/{LicenseID},{ReleasedByUserID}", Name = "ReleaseDetainedLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult ReleaseDetainedLicense(int LicenseID, int ReleasedByUserID)
        {
            if (LicenseID < 1 || ReleasedByUserID < 1)
                return BadRequest("Invalide Info");

            if (!clsUser.IsUserExist(ReleasedByUserID))
                return NotFound("User With  ID : " + ReleasedByUserID + " Not Found -(");


            clsLicense license = clsLicense.FindByLicenseID(LicenseID);

            if (license == null)
                return NotFound("License With  ID : " + LicenseID + " Not Found -(");


            if (!license.IsDetained)
                return BadRequest("License With Id " + LicenseID + " is not Detained");

            

            bool ReleaseDetainedLicense = license.ReleaseDetainedLicense(ReleasedByUserID);

            if (!ReleaseDetainedLicense)
                return StatusCode(409, "Error Release Detained License ");

            return Ok("License With ID "+LicenseID+ " has been Released Successfully");

        }



       
    }
}
