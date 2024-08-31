using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/TestAppointments")]
    [ApiController]
    public class TestAppointmentsController : ControllerBase
    {
        [HttpGet("GetAllTestAppointments", Name = "GetAllTestAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestAppointmentsListDTO>> GetAllTestAppointments()
        {
            List<TestAppointmentsListDTO> TestAppointmentsList = clsTestAppointment.GetAllTestAppointments();

            if (TestAppointmentsList.Count == 0)
                return NotFound("Not  TestAppointments Found !");

            return Ok(TestAppointmentsList);
        }



        [HttpGet("GetApplicationTestAppointmentsPerTestType/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "GetApplicationTestAppointmentsPerTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationTestAppointmentsDTO>> GetApplicationTestAppointmentsPerTestType(
            int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return NotFound("TestType With ID "+ TestTypeID+" Not Found.");

            if(clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID)==null)
                return NotFound("LocalDrivingLicenseApplication with ID "+LocalDrivingLicenseApplicationID+" Not Found.");



            List<ApplicationTestAppointmentsDTO> ApplicationTestAppointmentsList = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(
                LocalDrivingLicenseApplicationID, TestTypeID);

            if (ApplicationTestAppointmentsList.Count == 0)
                return NotFound("Not  Application Test Appointments Found !");

            return Ok(ApplicationTestAppointmentsList);
        }



      


        [HttpGet("GetTestAppointmentByID/{TestAppointmentID}", Name = "GetTestAppointmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestAppointmentDTO> GetTestAppointmentByID(int TestAppointmentID)
        {
            if (TestAppointmentID < 1)
                return BadRequest("Not Accepted ID : " + TestAppointmentID);

            clsTestAppointment TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (TestAppointment == null)
                return NotFound("TestAppointment With ID : " + TestAppointmentID + " Not Found !");

            return Ok(TestAppointment.testAppointmentDTO);

        }



        [HttpGet("GetLastTestAppointment/{LocalDrivingLicenseApplicationID},{TestTypeID}", Name = "GetLastTestAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestAppointmentDTO> GetLastTestAppointment(
             int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            if (!clsTestType.IsTestTypeValide(TestTypeID))
                return NotFound("TestType With ID " + TestTypeID + " Not Found.");

            if (clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID) == null)
                return NotFound("LocalDrivingLicenseApplication with ID " + LocalDrivingLicenseApplicationID + " Not Found.");

            clsTestAppointment TestAppointment = clsTestAppointment.GetLastTestAppointment(
                LocalDrivingLicenseApplicationID, TestTypeID);

            if (TestAppointment == null)
                return NotFound("Last Test Appointment With LocalDrivingLicenseApplicationID : " + LocalDrivingLicenseApplicationID +
                    "And TestTypeID = "+TestTypeID+" Not Found !");

            return Ok(TestAppointment.testAppointmentDTO);

        }



        [HttpPost("AddTestAppointment", Name = "AddTestAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<TestAppointmentDTO> AddTestAppointment(TestAppointmentDTO NewTestAppointmentDTO)
        {
            if (NewTestAppointmentDTO == null || NewTestAppointmentDTO.LocalDrivingLicenseApplicationID<1 ||
                 NewTestAppointmentDTO.CreatedByUserID<1 || !clsTestType.IsTestTypeValide((clsTestType.enTestType)NewTestAppointmentDTO.TestTypeID)
                || !clsValidation.IsNumber(Convert.ToString(NewTestAppointmentDTO.PaidFees)))

                return BadRequest("Invalid TestAppointment Data !");

            if (clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                NewTestAppointmentDTO.LocalDrivingLicenseApplicationID)==null)
                return NotFound("LocalDrivingLicenseApplication With ID " + NewTestAppointmentDTO.LocalDrivingLicenseApplicationID + " Not Found !");



            if (!clsUser.IsUserExist(NewTestAppointmentDTO.CreatedByUserID))
                return NotFound("User With id " + NewTestAppointmentDTO.CreatedByUserID + " Not Found !");


            if (!clsApplication.IsApplicationExist(NewTestAppointmentDTO.RetakeTestApplicationID ?? 0))
                NewTestAppointmentDTO.RetakeTestApplicationID = null;

            clsTestAppointment TestAppointment = new clsTestAppointment(
                new TestAppointmentDTO(NewTestAppointmentDTO.TestAppointmentID, NewTestAppointmentDTO.TestTypeID,
                NewTestAppointmentDTO.LocalDrivingLicenseApplicationID, NewTestAppointmentDTO.AppointmentDate,
                NewTestAppointmentDTO.PaidFees, NewTestAppointmentDTO.CreatedByUserID, NewTestAppointmentDTO.IsLocked,
                NewTestAppointmentDTO.RetakeTestApplicationID)
                );



            if (!TestAppointment.Save())
                return StatusCode(409, "Error Add TestAppointment ,! no row add");

            NewTestAppointmentDTO.TestAppointmentID = TestAppointment.TestAppointmentID;

            return CreatedAtRoute("GetTestAppointmentByID", new { TestAppointmentID = NewTestAppointmentDTO.TestAppointmentID }, NewTestAppointmentDTO);
        }




        [HttpPut("UpdateTestAppointment/{TestAppointmentID}", Name = "UpdateTestAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestAppointmentDTO> UpdateTestAppointment(int TestAppointmentID, TestAppointmentDTO UpdatedTestAppointmentDTO)
        {
            if (UpdatedTestAppointmentDTO == null || UpdatedTestAppointmentDTO.LocalDrivingLicenseApplicationID < 1 ||
                 UpdatedTestAppointmentDTO.CreatedByUserID < 1 || !clsTestType.IsTestTypeValide((clsTestType.enTestType)UpdatedTestAppointmentDTO.TestTypeID)
                || !clsValidation.IsNumber(Convert.ToString(UpdatedTestAppointmentDTO.PaidFees)) || TestAppointmentID <1)

                return BadRequest("Invalid TestAppointment Data !");

           


            clsTestAppointment TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (TestAppointment == null)
                return NotFound("TestAppointment With ID: " + TestAppointmentID + " Not Found!");


            if (!clsApplication.IsApplicationExist(UpdatedTestAppointmentDTO.RetakeTestApplicationID ?? 0))
                UpdatedTestAppointmentDTO.RetakeTestApplicationID = null;


            TestAppointment.AppointmentDate=UpdatedTestAppointmentDTO.AppointmentDate;
           TestAppointment.RetakeTestApplicationID=UpdatedTestAppointmentDTO.RetakeTestApplicationID;
            TestAppointment.LocalDrivingLicenseApplicationID = UpdatedTestAppointmentDTO.LocalDrivingLicenseApplicationID;
           TestAppointment.CreatedByUserID=UpdatedTestAppointmentDTO.CreatedByUserID;
            TestAppointment.IsLocked = UpdatedTestAppointmentDTO.IsLocked;
            TestAppointment.PaidFees = UpdatedTestAppointmentDTO.PaidFees;
            

            if (!TestAppointment.Save())
                return StatusCode(409, "Error Updating TestAppointment");


            return Ok(TestAppointment.testAppointmentDTO);

        }


    }
}
