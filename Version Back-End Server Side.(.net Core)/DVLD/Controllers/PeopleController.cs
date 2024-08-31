using DVLD.Global.Classes;
using DVLD.Global_Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("GetAllPeople", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PeopleListDTO>> GetAllPeople()
        {
            List<PeopleListDTO> PeopleList = clsPerson.GetAllPeople();

            if (PeopleList.Count == 0)
                return NotFound("Not  People Found !");

            return Ok(PeopleList);
        }

        [HttpGet("IsPersonExistByID/{PersonID}", Name = "IsPersonExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsPersonExistByID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            bool IsPersonExist = clsPerson.IsPersonExist(PersonID);

            if (!IsPersonExist)
                return NotFound("Person With  ID : " + PersonID + " Not Found -(");

            return Ok("Person With  ID : " + PersonID + " Exist -)");

        }


        [HttpGet("IsPersonExistByNationalNo/{PersonID}", Name = "IsPersonExistByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsPersonExistByNationalNo(string NationalNo)
        {
            if (string.IsNullOrEmpty(NationalNo))
                return BadRequest("Not Accepted National No : " + NationalNo);

            bool IsPersonExist = clsPerson.ISPersonExist(NationalNo);

            if (!IsPersonExist)
                return NotFound("Person With  National No : " + NationalNo + " Not Found -(");

            return Ok("Person With  National No : " + NationalNo + " Exist -)");

        }



        [HttpDelete("DeletePerson/{PersonID}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeletePerson(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            if (!clsPerson.IsPersonExist(PersonID))
                return NotFound("Person With  ID : " + PersonID + " Not Found -(");


            if (!clsPerson.DeletePerson(PersonID))
                return StatusCode(409, "Error Delete Person , ! .no row deleted");


            return Ok("Person with id : " + PersonID + " has been deleted");

        }


        [HttpGet("GetPersonByID/{PersonID}", Name = "GetPersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> GetPersonByID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            clsPerson Person = clsPerson.Find(PersonID);

            if (Person == null)
                return NotFound("Person With ID : " + PersonID + " Not Found !");

            return Ok(Person.personDTO);

        }


        [HttpGet("GetPersonByNationNo/{NationalNo}", Name = "GetPersonByNationNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> GetPersonByNationNo(string NationalNo)
        {
            if (string.IsNullOrEmpty(NationalNo))
                return BadRequest("Not Accepted National No : " + NationalNo);

            clsPerson Person = clsPerson.Find(NationalNo);

            if (Person == null)
                return NotFound("Person With National No : " + NationalNo + " Not Found !");

            return Ok(Person.personDTO);

        }




        [HttpPost("AddPerson", Name = "AddPerson")]
        //[Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<PersonDTO> AddPerson(PersonDTO NewPersonDTO ,clsPerson.enGender Gender)
        {
            

            if (NewPersonDTO == null || string.IsNullOrEmpty(NewPersonDTO.NationalNo) ||
                 string.IsNullOrEmpty(NewPersonDTO.FirstName) || string.IsNullOrEmpty(NewPersonDTO.LastName)
                 || NewPersonDTO.NationalityCountryID<1 || string.IsNullOrEmpty(NewPersonDTO.Phone)||
                 !clsValidation.IsNumber(NewPersonDTO.Phone))

                return BadRequest("Invalid Person Data !");



            if (!string.IsNullOrEmpty(NewPersonDTO.Email) && !clsValidation.ValidateEmail(NewPersonDTO.Email))
                return BadRequest("Email Not Valide");


            if (clsCountry.Find(NewPersonDTO.NationalityCountryID) == null)
                return NotFound("Country With id " + NewPersonDTO.NationalityCountryID + " Not Found");



            if (clsPerson.ISPersonExist(Convert.ToString(NewPersonDTO.NationalNo)))
                return BadRequest("National Number "+ NewPersonDTO.NationalNo+" already exist");


           




            clsPerson Person = new clsPerson(
                new PersonDTO(-1,NewPersonDTO.NationalNo,(byte)Gender,NewPersonDTO.FirstName,NewPersonDTO.SecondName,
                NewPersonDTO.ThirdName,NewPersonDTO.LastName,NewPersonDTO.Email,NewPersonDTO.Phone,NewPersonDTO.Address,
                NewPersonDTO.DateOfBirth,"",NewPersonDTO.NationalityCountryID)
                );


            if (!Person.Save())
                return StatusCode(409, "Error Add Person ,! no row add");


            NewPersonDTO.PersonID = Person.PersonID;
            

            return CreatedAtRoute("GetPersonByID", new { PersonID = NewPersonDTO.PersonID }, NewPersonDTO);


        }


        [HttpPost("UploadPersonImage/{PersonID}",Name = "UploadPersonImage")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadPersonImage(int PersonID, IFormFile ImageFile)
        {
            clsPerson person=clsPerson.Find(PersonID);
            if(person==null)
                return NotFound("Person With id "+PersonID+" Not Found");

            if (ImageFile == null || ImageFile.Length == 0)
                return BadRequest("No File Uploaded !");

            
            if (!ImageFile.ContentType.StartsWith("image/"))
                 return BadRequest("The uploaded file is not an image.");

           
             
            string? FilePath = await clsUtil.CopyImageToProjectImagesFolder(ImageFile);

            if (FilePath == null)
                return StatusCode(500, "Error Save Image");

            person.ImagePath = FilePath;
            if(!person.Save())
                return StatusCode(500, "Error Save Image");

            return Ok(new { FilePath });
        }

        [HttpPost("GetPersonImage/{PersonID}", Name = "GetPersonImage")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult GetPersonImage(int PersonID)
        {
            clsPerson person = clsPerson.Find(PersonID);
            if (person == null)
                return NotFound("Person With id " + PersonID + " Not Found");

            string FilePath = person.ImagePath;

            if (!System.IO.File.Exists(FilePath))
                return NotFound("Image Not Found !");

            var Image = System.IO.File.OpenRead(FilePath);
            var MimeType = GetMimeType(FilePath);




            return File(Image, MimeType);
        }

        private string GetMimeType(string FilePath)
        {
            var Extension = Path.GetExtension(FilePath).ToLowerInvariant();

            return Extension switch
            {
                ".jpg" => "image/jpg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }

    }
}
