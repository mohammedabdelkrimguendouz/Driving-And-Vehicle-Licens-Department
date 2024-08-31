using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/LocalDrivingLicenseApplications")]
    [ApiController]
    public class LocalDrivingLicenseApplicationsController : ControllerBase
    {
        [HttpGet("GetAllLocalDrivingLicenseApplications", Name = "GetAllLocalDrivingLicenseApplications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LocalDrivingLicenseApplicationsListDTO>> GetAllLocalDrivingLicenseApplications()
        {
            List<LocalDrivingLicenseApplicationsListDTO> LocalDrivingLicenseApplicationsList = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            if (LocalDrivingLicenseApplicationsList.Count == 0)
                return NotFound("Not  Local Driving License Applications Found !");

            return Ok(LocalDrivingLicenseApplicationsList);
        }

        [HttpGet("GetPassedTestCount/{LocalDrivingLicenseApplicationID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<byte> GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return BadRequest("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            byte PassedTestCount = localDrivingLicenseApplication.GetPassedTestCount();

            return Ok(PassedTestCount);

        }



        [HttpGet("IsPassedAllTests/{LocalDrivingLicenseApplicationID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.IsPassedAllTests());

        }


        [HttpGet("DoesPassedTestType/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "DoesPassedTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> DoesPassedTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return BadRequest("TestType with ID " + (int)TestTypeID + "Not Found");

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.DoesPassedTestType(TestTypeID));

        }

        [HttpGet("DeosAttendTestType/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "DeosAttendTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> DeosAttendTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return BadRequest("TestType with ID " + (int)TestTypeID + "Not Found");

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.DeosAttendTestType(TestTypeID));

        }


        [HttpGet("GetTotalTrialsPerTest/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "GetTotalTrialsPerTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return BadRequest("TestType with ID " + (int)TestTypeID + "Not Found");

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.GetTotalTrialsPerTest(TestTypeID));

        }




        [HttpGet("IsThereAnActiveScheduleTest/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "IsThereAnActiveScheduleTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsThereAnActiveScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return BadRequest("TestType with ID " + (int)TestTypeID + "Not Found");

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.IsThereAnActiveScheduleTest(TestTypeID));

        }


        [HttpGet("GetActiveLicenseID/{LocalDrivingLicenseApplicationID}",Name = "GetActiveLicenseID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetActiveLicenseID(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("Not Accepted  ID : " + LocalDrivingLicenseApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");


            int ActiveLicenseID = localDrivingLicenseApplication.GetActiveLicenseID();

            return Ok((ActiveLicenseID == -1) ? null : ActiveLicenseID);

        }



        [HttpGet("IsLicenseIssued/{LocalDrivingLicenseApplicationID}", Name = "IsLicenseIssued")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsLicenseIssued(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.IsLicenseIssued());

        }



        [HttpGet("IssuedLicenseForTheFirstTime/{LocalDrivingLicenseApplicationID},{Notes},{CreatedByUserID}", Name = "IssuedLicenseForTheFirstTime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<LicenseDTO> IssuedLicenseForTheFirstTime(int LocalDrivingLicenseApplicationID,string Notes, int CreatedByUserID)
        {
            if (LocalDrivingLicenseApplicationID < 1 || CreatedByUserID<1)
                return BadRequest("Invalide Info ");


            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");


            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");

            if (localDrivingLicenseApplication.IsLicenseIssued())
                return BadRequest("License Already Issued For the first time");

            if(!localDrivingLicenseApplication.IsPassedAllTests())
                return BadRequest("Can not Issued License Because Deos Not Passed All Tests !");

            clsLicense license=localDrivingLicenseApplication.IssuedLicenseForTheFirstTime(Notes, CreatedByUserID);
            if(license == null)
                return StatusCode(409,"Error Issued License For The First Time");

            return Ok(license.licenseDTO);

        }

        [HttpDelete("DeleteLocalDrivingLicenseApplication/{LocalDrivingLicenseApplicationID}", Name = "DeleteLocalDrivingLicenseApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("Not Accepted ID : " + LocalDrivingLicenseApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                 LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");


            if(!localDrivingLicenseApplication.Delete())
                return StatusCode(409, "Error delete local Driving License Application !,no row deleted");


            return Ok("Local Driving License Application with id : " + LocalDrivingLicenseApplicationID + " has been deleted");

        }



        [HttpGet("GetLocalDrivingLicenseApplicationByID/{LocalDrivingLicenseApplicationID}", Name = "GetLocalDrivingLicenseApplicationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocalDrivingLicenseApplicationDTO> GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("Not Accepted ID : " + LocalDrivingLicenseApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                  LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.localDrivingLicenseApplicationDTO);

        }


        [HttpGet("GetLocalDrivingLicenseApplicationByApplicationID/{ApplicationID}", Name = "GetLocalDrivingLicenseApplicationByApplicationID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocalDrivingLicenseApplicationDTO> GetLocalDrivingLicenseApplicationByApplicationID(int ApplicationID)
        {
            if (ApplicationID < 1)
                return BadRequest("Not Accepted ID : " + ApplicationID);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(
                  ApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("Application With Id " + ApplicationID + " Not Found");

            return Ok(localDrivingLicenseApplication.localDrivingLicenseApplicationDTO);

        }


        [HttpPost("AddLocalDrivingLicenseApplication,{ApplicantPersonID},{CreatedByUserID}", Name = "AddLocalDrivingLicenseApplication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LocalDrivingLicenseApplicationDTO> AddLocalDrivingLicenseApplication(int ApplicantPersonID,int CreatedByUserID,
           LocalDrivingLicenseApplicationDTO NewLocalDrivingLicenseApplicationDTO)
        {
            if (NewLocalDrivingLicenseApplicationDTO==null||NewLocalDrivingLicenseApplicationDTO.LicenseClassID<1 ||
                ApplicantPersonID<1 || CreatedByUserID<1)

                return BadRequest("Invalid  Data !");

            clsLicenseClass licenseClass = clsLicenseClass.Find(NewLocalDrivingLicenseApplicationDTO.LicenseClassID);

            if (licenseClass == null)
                return NotFound("LicenseClass With ID " + NewLocalDrivingLicenseApplicationDTO.LicenseClassID + " Not Found");


            if (!clsPerson.IsPersonExist(ApplicantPersonID))
                return NotFound("Person With ID " + ApplicantPersonID + " Not Found");

            if (!clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");

            clsApplicationType applicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense);

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication(
                new LocalDrivingLicenseApplicationDTO(-1,NewLocalDrivingLicenseApplicationDTO.LicenseClassID,
                -1), new ApplicationDTO(-1, ApplicantPersonID, DateTime.Now, (int)clsApplication.enApplicationType.NewDrivingLicense,
                DateTime.Now, (int)clsApplication.enApplicationStatus.New, applicationType.ApplicationFees, CreatedByUserID)
                );


            if (!localDrivingLicenseApplication.Save())
                return StatusCode(500, "Error Add Local Driving License Application ,! no row add");


           NewLocalDrivingLicenseApplicationDTO.LocalDrivingLicenseApplicationID=localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
           

            return CreatedAtRoute("GetLocalDrivingLicenseApplicationByID", new { LocalDrivingLicenseApplicationID = NewLocalDrivingLicenseApplicationDTO.LocalDrivingLicenseApplicationID }, NewLocalDrivingLicenseApplicationDTO);


        }

        [HttpPut("UpdateLocalDrivingLicenseApplication/{LocalDrivingLicenseApplicationID}", Name = "UpdateLocalDrivingLicenseApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LocalDrivingLicenseApplicationDTO> UpdateLocalDrivingLicenseApplicationID(int ApplicantPersonID, int CreatedByUserID,
            int LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplicationDTO UpdateLocalDrivingLicenseApplicationDTO)
        {
            if (UpdateLocalDrivingLicenseApplicationDTO == null||LocalDrivingLicenseApplicationID<1 || UpdateLocalDrivingLicenseApplicationDTO.LicenseClassID < 1 ||
               ApplicantPersonID < 1 || CreatedByUserID < 1 || UpdateLocalDrivingLicenseApplicationDTO.ApplicationID<1)

                return BadRequest("Invalid  Data !");


            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
                return NotFound("local Driving License Application With ID: " + LocalDrivingLicenseApplicationID + " Not Found!");


            if (localDrivingLicenseApplication.ApplicantPersonID!=ApplicantPersonID&&  !clsPerson.IsPersonExist(ApplicantPersonID))
                return NotFound("Person With ID " + ApplicantPersonID + " Not Found");

            if (localDrivingLicenseApplication.CreatedByUserID!=CreatedByUserID&& !clsUser.IsUserExist(CreatedByUserID))
                return NotFound("User With  ID : " + CreatedByUserID + " Not Found -(");

            if(localDrivingLicenseApplication.ApplicationID!=UpdateLocalDrivingLicenseApplicationDTO.ApplicationID&& !clsApplication.IsApplicationExist(UpdateLocalDrivingLicenseApplicationDTO.ApplicationID))
                return NotFound("Application With ID"+UpdateLocalDrivingLicenseApplicationDTO.ApplicationID+" Not Found");



            clsLicenseClass licenseClass = clsLicenseClass.Find(UpdateLocalDrivingLicenseApplicationDTO.LicenseClassID);

            if(licenseClass == null)
                    return NotFound("LicenseClass With ID " + UpdateLocalDrivingLicenseApplicationDTO.LicenseClassID + " Not Found");
            
                

            

            localDrivingLicenseApplication.ApplicationID=UpdateLocalDrivingLicenseApplicationDTO.ApplicationID;
            localDrivingLicenseApplication.LastStatusDate=DateTime.Now;
            localDrivingLicenseApplication.ApplicationDate=DateTime.Now;
            localDrivingLicenseApplication.LastStatusDate=DateTime.Now;
            localDrivingLicenseApplication.ApplicationID = UpdateLocalDrivingLicenseApplicationDTO.ApplicationID;
            localDrivingLicenseApplication.CreatedByUserID = CreatedByUserID;
            localDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            localDrivingLicenseApplication.ApplicantPersonID = ApplicantPersonID;
            localDrivingLicenseApplication.ApplicationTypeID =(int) clsApplication.enApplicationType.NewDrivingLicense;
            localDrivingLicenseApplication.PaidFees=clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees;
            localDrivingLicenseApplication.LicenseClassID=licenseClass.LicenseClassID;
            
          

            if (!localDrivingLicenseApplication.Save())
                return StatusCode(409, "Error Updating localDrivingLicenseApplication");


            return Ok(localDrivingLicenseApplication.localDrivingLicenseApplicationDTO);

        }
    }
}
