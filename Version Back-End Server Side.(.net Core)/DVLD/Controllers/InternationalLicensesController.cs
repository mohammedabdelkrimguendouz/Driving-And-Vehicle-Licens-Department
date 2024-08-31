using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/InternationalLicenses")]
    [ApiController]
    public class InternationalLicensesController : ControllerBase
    {
        [HttpGet("GetAllInternationalLicenseApplications", Name = "GetAllInternationalLicenseApplications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<InternationalLicensesListDTO>> GetAllInternationalLicenseApplications()
        {
            List<InternationalLicensesListDTO> InternationalLicensesList = clsInternationalLicense.GetAllInternationalLicenses();

            if (InternationalLicensesList.Count == 0)
                return NotFound("Not  International Licenses Found !");

            return Ok(InternationalLicensesList);
        }


        [HttpGet("GetActiveInternationalLicenseIDByDriverID/{DriverID}", Name = "GetActiveInternationalLicenseIDByDriverID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepted Driver with ID : " + DriverID);


            if (clsDriver.FindByDriverID(DriverID) == null)
                return NotFound("Driver Eith Id " + DriverID + "Not Found");


           

            int ActiveInternationalLicenseID =clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(DriverID) ;

            return Ok((ActiveInternationalLicenseID == -1) ? null : ActiveInternationalLicenseID);

        }


        [HttpGet("GetDriverInternationalLicenses/{DriverID}", Name = "GetDriverInternationalLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<InternationalLicensesListDTO>> GetDriverInternationalLicenses(int DriverID)
        {
            if (DriverID < 1)
                return BadRequest("Not Accepted Driver with ID : " + DriverID);


            if (clsDriver.FindByDriverID(DriverID) == null)
                return NotFound("Driver Eith Id " + DriverID + "Not Found");

            List<InternationalLicensesListDTO> DriverInternationalLicensesList = clsInternationalLicense.GetDriverInternationalLicenses(DriverID);

            if (DriverInternationalLicensesList.Count == 0)
                return NotFound("Not  Driver International Licenses Found !");

            return Ok(DriverInternationalLicensesList);
        }



        [HttpGet("DeactivateInternationalLicense/{InternationalLicenseID}", Name = "DeactivateInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeactivateInternationalLicense(int InternationalLicenseID)
        {
            if (InternationalLicenseID < 1)
                return BadRequest("Not Accepted ID : " + InternationalLicenseID);

            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (internationalLicense == null)
                return NotFound("international License With  ID : " + InternationalLicenseID + " Not Found -(");

            if (!internationalLicense.IsActive)
                return BadRequest("international License With id " + InternationalLicenseID + "Is not active");

            if (!internationalLicense.DeactivateCurrentInternationalLicense())
                return StatusCode(409, "Error Deactivate international License ");

            return Ok("international License With ID " + InternationalLicenseID + " has been Deactivate Succefully");

        }



        [HttpGet("IsInternationalLicenseExpired/{InternationalLicenseID}", Name = "IsInternationalLicenseExpired")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsInternationalLicenseExpired(int InternationalLicenseID)
        {
            if (InternationalLicenseID < 1)
                return BadRequest("Not Accepted ID : " + InternationalLicenseID);

            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (internationalLicense == null)
                return NotFound("License With  ID : " + InternationalLicenseID + " Not Found -(");



            return Ok(internationalLicense.IsInternationalLicenseExpired());

        }


        [HttpGet("GetInternationalLicenseByID/{InternationalLicenseID}", Name = "GetInternationalLicenseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<InternationalLicenseDTO> GetInternationalLicenseByID(int InternationalLicenseID)
        {
            if (InternationalLicenseID < 1)
                return BadRequest("Not Accepted ID : " + InternationalLicenseID);

            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find(
                  InternationalLicenseID);

            if (InternationalLicense == null)
                return NotFound("International License With Id " + InternationalLicenseID + " Not Found");

            return Ok(InternationalLicense.internationalLicenseDTO);

        }






        [HttpPost("AddInternationalLicense,{ApplicantPersonID},{CreatedByUserID}", Name = "AddInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InternationalLicenseDTO> AddInternationalLicense(int CreatedByUserID,
         InternationalLicenseDTO NewInternationalLicenseDTO)
        {
            if (NewInternationalLicenseDTO == null || NewInternationalLicenseDTO.IssuedUsingLocalLicenseID < 1 ||
                NewInternationalLicenseDTO.DriverID < 1 || CreatedByUserID < 1)

                return BadRequest("Invalid  Data !");

            

            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With ID " + CreatedByUserID + " Not Found");


            clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                    NewInternationalLicenseDTO.IssuedUsingLocalLicenseID);

            if (localDrivingLicenseApplication == null)
                return NotFound("Issued Using Local License with id " + localDrivingLicenseApplication + " Not Found");



            if (localDrivingLicenseApplication.LicenseClassID != (int)clsLocalDrivingLicenseApplication.enLicenseClass.Ordinarydrivinglicense)
                return NotFound("must be License From Class "+ clsLocalDrivingLicenseApplication.enLicenseClass.Ordinarydrivinglicense.ToString());


            clsDriver driver = clsDriver.FindByDriverID(NewInternationalLicenseDTO.DriverID);

            if (driver == null)
                return NotFound("Driver With ID " + NewInternationalLicenseDTO.DriverID + " Not Found");

            if(driver.IsIssuedInternationalLicense)
                return NotFound("Driver With ID " + NewInternationalLicenseDTO.DriverID + " alread Issued International License");


            clsApplicationType applicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense);
            
            clsInternationalLicense InternationalLicense = new clsInternationalLicense(
                new InternationalLicenseDTO(-1, NewInternationalLicenseDTO.DriverID,
                NewInternationalLicenseDTO.IssuedUsingLocalLicenseID,DateTime.Now,
                DateTime.Now.AddYears(clsSetting.GetDefaultValidityLengthForAnInternationalLicense()),true,
                (byte)clsInternationalLicense.enIssueReason.FirstTime,CreatedByUserID,-1), new ApplicationDTO(-1,driver.PersonID, DateTime.Now, (int)clsApplication.enApplicationType.NewInternationalLicense,
                DateTime.Now, (int)clsApplication.enApplicationStatus.Completed, applicationType.ApplicationFees, CreatedByUserID)
                );


            if (!InternationalLicense.Save())
                return StatusCode(500, "Error Add International License ,! no row add");


            NewInternationalLicenseDTO.InternationalLicenseID = InternationalLicense.InternationalLicenseID;


            return CreatedAtRoute("GetInternationalLicenseByID", new { InternationalLicenseID = NewInternationalLicenseDTO.InternationalLicenseID }, NewInternationalLicenseDTO);


        }

        [HttpGet("RenewInternationalLicense/{InternationalLicenseID},{CreatedByUserID}", Name = "InternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<InternationalLicenseDTO> RenewInternationalLicense(int InternationalLicenseID, int CreatedByUserID)
        {
            if (InternationalLicenseID < 1 || CreatedByUserID < 1)
                return BadRequest("Invalide Info");

            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsInternationalLicense internationalLicense =clsInternationalLicense.Find(InternationalLicenseID);

            if (internationalLicense == null)
                return NotFound("internationalLicense With  ID : " + InternationalLicenseID + " Not Found -(");


            

            if (!internationalLicense.IsInternationalLicenseExpired())
                return BadRequest("internationalLicense With Id " + InternationalLicenseID + " is not Expired");

            clsInternationalLicense NewinternationalLicense = internationalLicense.RenewInternationalLicense(CreatedByUserID);

            if (NewinternationalLicense == null)
                return StatusCode(409, "Error Renew internationalLicense ");

            return Ok(NewinternationalLicense.internationalLicenseDTO);

        }




        [HttpGet("ReplacementForLostinternationalLicense/{internationalLicenseID},{CreatedByUserID}", Name = "ReplacementForLostinternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<InternationalLicenseDTO> ReplacementForLost(int internationalLicenseID, int CreatedByUserID)
        {
            if (internationalLicenseID < 1 || CreatedByUserID < 1)
                return BadRequest("Invalide Info");



            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(internationalLicenseID);

            if (internationalLicense == null)
                return NotFound("License With  ID : " + internationalLicenseID + " Not Found -(");


           

            clsInternationalLicense NewInternationalLicense = internationalLicense.ReplaceInternationalLicense(CreatedByUserID, clsInternationalLicense.enIssueReason.LostReplacement);

            if (NewInternationalLicense == null)
                return StatusCode(409, "Error Replace NewInternationalLicense ");

            return Ok(NewInternationalLicense.internationalLicenseDTO);

        }



        [HttpGet("ReplacementForDamagedInternationalLicense/{internationalLicenseID},{CreatedByUserID}", Name = "ReplacementForDamagedInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<InternationalLicenseDTO> ReplacementForDamaged(int internationalLicenseID, int CreatedByUserID)
        {
            if (internationalLicenseID < 1 || CreatedByUserID < 1)
                return BadRequest("Invalide Info");



            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");


            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(internationalLicenseID);

            if (internationalLicense == null)
                return NotFound("License With  ID : " + internationalLicenseID + " Not Found -(");




            clsInternationalLicense NewInternationalLicense = internationalLicense.ReplaceInternationalLicense(CreatedByUserID, clsInternationalLicense.enIssueReason.DamagedReplacement);

            if (NewInternationalLicense == null)
                return StatusCode(409, "Error Replace NewInternationalLicense ");

            return Ok(NewInternationalLicense.internationalLicenseDTO);

        }

    }
}
