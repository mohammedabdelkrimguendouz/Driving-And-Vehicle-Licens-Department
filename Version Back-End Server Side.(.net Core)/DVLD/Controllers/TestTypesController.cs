using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DVLD.Controllers
{
    [Route("api/TestTypes")]
    [ApiController]
    public class TestTypesController : ControllerBase
    {
        [HttpGet("GetAllTestTypes", Name = "GetAllTestTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestTypeDTO>> GetAllTestTypes()
        {
            List<TestTypeDTO> TestTypesList = clsTestType.GetAllTestTypes();

            if (TestTypesList.Count == 0)
                return NotFound("Not  TestTypes Found !");

            return Ok(TestTypesList);
        }



        [HttpGet("GetTestTypeByID/{TestTypeID}", Name = "GetTestTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestTypeDTO> GetTestTypeByID(clsTestType.enTestType TestTypeID)
        {

            if (!clsTestType.IsTestTypeValide(TestTypeID))
            {
                return BadRequest("Invalid TestTypeID.");
            }

            clsTestType testType = clsTestType.Find(TestTypeID);

            if (testType == null)
                return NotFound("testType With ID : " + TestTypeID + " Not Found !");

            return Ok(testType.testTypeDTO);

        }



        [HttpPut("UpdateTestType/{TestTypeID}", Name = "UpdateTestType")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestTypeDTO> UpdateTestType(clsTestType.enTestType TestTypeID, TestTypeDTO UpdatedTestTypeDTO)
        {
            if (UpdatedTestTypeDTO == null || string.IsNullOrEmpty(UpdatedTestTypeDTO.Title) ||
                !clsValidation.IsNumber(Convert.ToString(UpdatedTestTypeDTO.Fees))|| !clsTestType.IsTestTypeValide(TestTypeID))

                return BadRequest("Invalid TestType Data !");


            clsTestType TestType = clsTestType.Find(TestTypeID);

            if (TestType == null)
                return NotFound("TestType With ID: " + TestTypeID + " Not Found!");


            

            TestType.Title = UpdatedTestTypeDTO.Title;
            TestType.Fees = UpdatedTestTypeDTO.Fees;
            TestType.Description = UpdatedTestTypeDTO.Description;
           

            if (!TestType.Save())
                return StatusCode(409, "Error Updating TestType");


            return Ok(TestType.testTypeDTO);

        }

       
    }
}
