using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/Tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet("GetAllTests", Name = "GetAllTests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestDTO>> GetAllTests()
        {
            List<TestDTO> TestsList = clsTest.GetAllTests();

            if (TestsList.Count == 0)
                return NotFound("Not  Tests Found !");

            return Ok(TestsList);
        }


        [HttpGet("GetPassedTestCount/{LocalDrivingLicenseApplicationID}", Name = "GetPassedTestCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<byte> GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            if(LocalDrivingLicenseApplicationID<1)
                return BadRequest("not Accepte Id "+LocalDrivingLicenseApplicationID);

            if (clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID) ==null)
                return BadRequest("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            byte PassedTestCount = clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);

            return Ok(PassedTestCount);

        }




        [HttpGet("IsPassedAllTests/{LocalDrivingLicenseApplicationID}", Name = "IsPassedAllTests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID < 1)
                return BadRequest("not Accepte Id " + LocalDrivingLicenseApplicationID);

            if (clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                LocalDrivingLicenseApplicationID) == null)
                return NotFound("LocalDrivingLicenseApplication With Id " + LocalDrivingLicenseApplicationID + " Not Found");

            

            return Ok(clsTest.IsPassedAllTests(LocalDrivingLicenseApplicationID));

        }



        [HttpGet("GetTestByID/{TestID}", Name = "GetTestByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestDTO> GetTestByID(int TestID)
        {
            if (TestID < 1)
                return BadRequest("Not Accepted ID : " + TestID);

            clsTest Test = clsTest.Find(TestID);

            if (Test == null)
                return NotFound("Test With ID : " + TestID + " Not Found !");

            return Ok(Test.testDTO);

        }




        [HttpPost("AddTest", Name = "AddTest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<TestDTO> AddTest(TestDTO NewTestDTO)
        {
            if (NewTestDTO == null || NewTestDTO.TestAppointmentID<1 ||
                  NewTestDTO.CreatedByUserID < 1)

                return BadRequest("Invalid Test Data !");

            if (clsTestAppointment.Find(NewTestDTO.TestAppointmentID)==null)
                return NotFound("TestAppointment With ID " + NewTestDTO.TestAppointmentID + " Not Found !");



            if (!clsUser.IsUserExist(NewTestDTO.CreatedByUserID))
                return NotFound("User With id " + NewTestDTO.CreatedByUserID + " Not Found !");


           
            clsTest Test = new clsTest(
                new TestDTO(NewTestDTO.TestID,NewTestDTO.TestAppointmentID,NewTestDTO.Notes,
                NewTestDTO.CreatedByUserID,NewTestDTO.TestResult)
                );


            if (!Test.Save())
                return StatusCode(409, "Error Add Test ,! no row add");


            NewTestDTO.TestID = Test.TestID;
            

            return CreatedAtRoute("GetTestByID", new { TestID = NewTestDTO.TestID }, NewTestDTO);


        }

        [HttpPut("UpdateTest/{TestID}", Name = "UpdateTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public ActionResult<TestDTO> UpdateTest(int TestID, TestDTO UpdatedTestDTO)
        {
            if (UpdatedTestDTO == null||TestID<1 || UpdatedTestDTO.TestAppointmentID < 1 ||
                  UpdatedTestDTO.CreatedByUserID < 1)

                return BadRequest("Invalid Test Data !");



            clsTest Test = clsTest.Find(TestID);

            if (Test == null)
                return NotFound("Test With ID: " + TestID + " Not Found!");


            if (Test.TestAppointmentID != UpdatedTestDTO.TestAppointmentID && clsTestAppointment.Find(UpdatedTestDTO.TestAppointmentID) == null)
                return NotFound("TestAppointment With ID " + UpdatedTestDTO.TestAppointmentID + " Not Found !");



            if (Test.CreatedByUserID!=UpdatedTestDTO.CreatedByUserID&&  !clsUser.IsUserExist(UpdatedTestDTO.CreatedByUserID))
                return NotFound("User With id " + UpdatedTestDTO.CreatedByUserID + " Not Found !");



          
            Test.TestResult = UpdatedTestDTO.TestResult;
            Test.Notes = UpdatedTestDTO.Notes;
            Test.CreatedByUserID = UpdatedTestDTO.CreatedByUserID;
            Test.TestAppointmentID = UpdatedTestDTO.TestAppointmentID;

           
            if (!Test.Save())
                return StatusCode(409, "Error Updating Test");


            return Ok(Test.testDTO);

        }

    }
}
