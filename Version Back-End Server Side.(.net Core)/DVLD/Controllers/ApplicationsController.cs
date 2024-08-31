using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/clsApplications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        [HttpGet("GetAllApplications", Name = "GetAllApplications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationDTO>> GetAllApplications()
        {
            List<ApplicationDTO> ApplicationsList = clsApplication.GetAllApplications();

            if (ApplicationsList.Count == 0)
                return NotFound("Not  Applications Found !");

            return Ok(ApplicationsList);
        }



        [HttpGet("DeosPersonHaveActiveApplication/{ApplicantPersonID},{ApplicationTypeID}", Name = "DeosPersonHaveActiveApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeosPersonHaveActiveApplication(int ApplicantPersonID,clsApplication.enApplicationType ApplicationTypeID)
        {
            if (ApplicantPersonID < 1)
                return BadRequest("Not Accepted Applicant Person With ID : " + ApplicantPersonID);

            if (!clsApplication.IsApplicationTypeValide(ApplicationTypeID))
                return BadRequest("Not Accepted Application Type with ID : " + ApplicationTypeID);

            if (!clsPerson.IsPersonExist(ApplicantPersonID))
                return NotFound("Person With ID " + ApplicantPersonID + " Not Found -(");

            

            bool DeosPersonHaveActiveApplication = clsApplication.DeosPersonHaveActiveApplication(ApplicantPersonID,
                ApplicationTypeID); 

            if (!DeosPersonHaveActiveApplication)
                return NotFound("Person With ID " + ApplicantPersonID + " Doet not have active Application " +
                    "For "+ApplicationTypeID+ " -(");

            return Ok("Person With ID " + ApplicantPersonID + "   have active Application " +
                    "For Application Type with ID " + ApplicationTypeID + " -(");

        }




        [HttpGet("GetActiveApplicationIDForLicenseClass/{ApplicantPersonID},{ApplicationTypeID},{LicenseClassID}", Name = "GetActiveApplicationIDForLicenseClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> GetActiveApplicationIDForLicenseClass(
            int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            if (ApplicantPersonID < 1)
                return BadRequest("Not Accepted Applicant Person With ID : " + ApplicantPersonID);

            if (LicenseClassID < 1)
                return BadRequest("Not Accepted LicenseClass With ID : " + LicenseClassID);

            if (!clsApplication.IsApplicationTypeValide(ApplicationTypeID))
                return BadRequest("Not Accepted Application Type with ID : " + ApplicationTypeID);

            if (!clsPerson.IsPersonExist(ApplicantPersonID))
                return NotFound("Person With ID " + ApplicantPersonID + " Not Found -(");

            if (clsLicenseClass.Find(LicenseClassID)==null)
                return NotFound("LicenseClass With ID " + LicenseClassID + " Not Found -(");



            int ActiveApplicationIDForLicenseClass = clsApplication.GetActiveApplicationIDForLicenseClass(
                ApplicantPersonID,ApplicationTypeID,LicenseClassID);

            return Ok(ActiveApplicationIDForLicenseClass);

        }



        [HttpGet("GetActiveApplicationID/{ApplicantPersonID},{ApplicationTypeID}", Name = "GetActiveApplicationID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> GetActiveApplicationID(
           int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            if (ApplicantPersonID < 1)
                return BadRequest("Not Accepted Applicant Person With ID : " + ApplicantPersonID);

            

            if (!clsApplication.IsApplicationTypeValide(ApplicationTypeID))
                return BadRequest("Not Accepted Application Type with ID : " + ApplicationTypeID);

            if (!clsPerson.IsPersonExist(ApplicantPersonID))
                return NotFound("Person With ID " + ApplicantPersonID + " Not Found -(");

          


            int GetActiveApplicationID = clsApplication.GetActiveApplicationID(
                ApplicantPersonID, ApplicationTypeID);

            return Ok(GetActiveApplicationID);

        }



        [HttpGet("IsApplicationExistByID/{ApplicationID}", Name = "IsApplicationExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsApplicationExistByApplicationID(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            bool IsApplicationExist = clsApplication.IsApplicationExist(ApplicationID);

            if (!IsApplicationExist)
                return NotFound("Application With  ID : " + ApplicationID + " Not Found -(");

            return Ok("Application With  ID : " + ApplicationID + " Exist -)");

        }



        [HttpGet("GetApplicationByID/{ApplicationID}", Name = "GetApplicationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApplicationDTO> GetApplicationByID(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

            if (Application == null)
                return NotFound("Application With ID : " + ApplicationID + " Not Found !");

            return Ok(Application.applicationDTO);

        }


        [HttpDelete("DeleteApplication/{ApplicationID}", Name = "DeleteApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeleteApplication(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            clsApplication application= clsApplication.FindBaseApplication(ApplicationID);

            if (application==null)
                return NotFound("Application With  ID : " + ApplicationID + " Not Found -(");


            if (!application.Delete())
                return StatusCode(409, "Error Delete Application , ! .no row deleted");


            return Ok("Application with id : " + ApplicationID + " has been deleted");

        }



        [HttpPut("SetCompleteApplication/{ApplicationID}", Name = "SetCompleteApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult SetCompleteApplication(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            clsApplication application = clsApplication.FindBaseApplication(ApplicationID);

            if (application == null)
                return NotFound("Application With  ID : " + ApplicationID + " Not Found -(");


            if (!application.SetComplete())
                return StatusCode(409, "Error Completed Application !");


            return Ok("Application with id : " + ApplicationID + "  Completed Succefully");

        }



        [HttpPut("CancelApplication/{ApplicationID}", Name = "CancelApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult CancelApplication(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            clsApplication application = clsApplication.FindBaseApplication(ApplicationID);

            if (application == null)
                return NotFound("Application With  ID : " + ApplicationID + " Not Found -(");


            if (!application.Cancel())
                return StatusCode(409, "Error Canceled Application !");


            return Ok("Application with id : " + ApplicationID + "  Canceled Succefully");

        }

        [HttpPost("AddApplication", Name = "AddApplication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<ApplicationDTO> AddApplication(ApplicationDTO NewApplicationDTO)
        {
            if (NewApplicationDTO == null || !clsApplication.IsApplicationStatusValide((clsApplication.enApplicationStatus)NewApplicationDTO.ApplicationStatus) ||
                 NewApplicationDTO.ApplicantPersonID<1 || !clsApplication.IsApplicationTypeValide((clsApplication.enApplicationType)NewApplicationDTO.ApplicationTypeID)
                 || !clsValidation.IsNumber(Convert.ToString(NewApplicationDTO.PaidFees)) || NewApplicationDTO.CreatedByUserID<1)

                return BadRequest("Invalid Application Data !");

            if (!clsPerson.IsPersonExist(NewApplicationDTO.ApplicantPersonID))
                return NotFound("Person With ID " + NewApplicationDTO.ApplicantPersonID + " Not Found !");

            if (!clsUser.IsUserExist(NewApplicationDTO.CreatedByUserID))
                return NotFound("User With ID " + NewApplicationDTO.CreatedByUserID + " Not Found !");

            


            
            clsApplication Application = new clsApplication(
                new ApplicationDTO(NewApplicationDTO.ApplicationID, NewApplicationDTO.ApplicantPersonID,
                NewApplicationDTO.ApplicationDate,NewApplicationDTO.ApplicationTypeID,NewApplicationDTO.LastStatusDate,
                NewApplicationDTO.ApplicationStatus,NewApplicationDTO.PaidFees,NewApplicationDTO.CreatedByUserID)
                
                );


            if (!Application.Save())
                return StatusCode(409, "Error Add Application ,! no row add");


            NewApplicationDTO.ApplicationID = Application.ApplicationID;
            

            return CreatedAtRoute("GetApplicationByID", new { ApplicationID = NewApplicationDTO.ApplicationID }, NewApplicationDTO);


        }

        [HttpPut("UpdateApplication/{ApplicationID}", Name = "UpdateApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationDTO> UpdateApplication(int ApplicationID, ApplicationDTO UpdatedApplicationDTO)
        {
            if (UpdatedApplicationDTO == null || !clsApplication.IsApplicationStatusValide((clsApplication.enApplicationStatus)UpdatedApplicationDTO.ApplicationStatus) ||
                  UpdatedApplicationDTO.ApplicantPersonID < 1 || !clsApplication.IsApplicationTypeValide((clsApplication.enApplicationType)UpdatedApplicationDTO.ApplicationTypeID)
                  || !clsValidation.IsNumber(Convert.ToString(UpdatedApplicationDTO.PaidFees)) || UpdatedApplicationDTO.CreatedByUserID < 1 ||
                  ApplicationID<1)

                return BadRequest("Invalid Application Data !");

           


            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

            if (Application == null)
                return NotFound("Application With ID: " + ApplicationID + " Not Found!");


            if (Application.ApplicantPersonID != UpdatedApplicationDTO.ApplicantPersonID && !clsPerson.IsPersonExist(UpdatedApplicationDTO.ApplicantPersonID))
                return NotFound("Person With ID : " + UpdatedApplicationDTO.ApplicantPersonID + " Not Found !");


            if (Application.CreatedByUserID != UpdatedApplicationDTO.CreatedByUserID && clsUser.IsUserExist(UpdatedApplicationDTO.CreatedByUserID))
                return NotFound("User With id " + UpdatedApplicationDTO.CreatedByUserID + "  Not Found !");



            Application.ApplicationStatus =(clsApplication.enApplicationStatus) UpdatedApplicationDTO.ApplicationStatus;
            Application.ApplicantPersonID=UpdatedApplicationDTO.ApplicantPersonID;
            Application.ApplicationDate=UpdatedApplicationDTO.ApplicationDate;
            Application.ApplicationTypeID = UpdatedApplicationDTO.ApplicationTypeID;
            Application.LastStatusDate=UpdatedApplicationDTO.LastStatusDate;
            Application.PaidFees=UpdatedApplicationDTO.PaidFees;
            Application.CreatedByUserID=UpdatedApplicationDTO.CreatedByUserID;

            if (!Application.Save())
                return StatusCode(409, "Error Updating Application");


            return Ok(Application.applicationDTO);

        }



    }
}
