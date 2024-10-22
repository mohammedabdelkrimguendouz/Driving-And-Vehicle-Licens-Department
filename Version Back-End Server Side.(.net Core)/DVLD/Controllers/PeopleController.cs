using DVLD.Global.Classes;
using DVLD.Global_Classes;
using DVLD_Buisness;
using DVLD_Buisness.Global_Classes;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetPersonByID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            clsPerson Person = clsPerson.Find(PersonID);

            if (Person == null)
                return NotFound("Person With ID : " + PersonID + " Not Found !");

            return Ok(new
            { 
                personInfo = Person.personDTO,
                countryInfo = Person.CountryInfo.countryDTO
            });
        }


        [HttpGet("GetPersonByNationNo/{NationalNo}", Name = "GetPersonByNationNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPersonByNationNo(string NationalNo)
        {
            if (string.IsNullOrEmpty(NationalNo))
                return BadRequest("Not Accepted National No : " + NationalNo);

            clsPerson Person = clsPerson.Find(NationalNo);

            if (Person == null)
                return NotFound("Person With National No : " + NationalNo + " Not Found !");

            return Ok(new
            {
                personInfo = Person.personDTO,
                countryInfo = Person.CountryInfo.countryDTO
            });

        }




        [HttpPost("AddPerson", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult>  AddPerson([FromForm] PersonDTO NewPersonDTO, IFormFile ImageFile)
        {
            

            if (NewPersonDTO==null || string.IsNullOrEmpty(NewPersonDTO.NationalNo) ||
                 string.IsNullOrEmpty(NewPersonDTO.FirstName) || string.IsNullOrEmpty(NewPersonDTO.LastName)
                 || NewPersonDTO.NationalityCountryID<1 || string.IsNullOrEmpty(NewPersonDTO.Phone)||
                 !clsValidation.IsNumber(NewPersonDTO.Phone) || !Enum.IsDefined(typeof(clsPerson.enGender),(Int32) NewPersonDTO.Gender))

                return BadRequest("Invalid Person Data !");


            if (ImageFile == null || ImageFile.Length == 0)
                return BadRequest("No Image File Uploaded !");

            if (!ImageFile.ContentType.StartsWith("image/"))
                return BadRequest("The uploaded file is not an image.");


            if (!string.IsNullOrEmpty(NewPersonDTO.Email) && !clsValidation.ValidateEmail(NewPersonDTO.Email))
                return BadRequest("Email Not Valide");


            if (clsCountry.Find(NewPersonDTO.NationalityCountryID) == null)
                return NotFound("Country With id " + NewPersonDTO.NationalityCountryID + " Not Found");



            if (clsPerson.ISPersonExist(Convert.ToString(NewPersonDTO.NationalNo)))
                return BadRequest("National Number "+ NewPersonDTO.NationalNo+" already exist");


            string? FilePath = await clsUtil.CopyImageToProjectImagesFolder(ImageFile);

            if (FilePath == null)
                return StatusCode(500, "Error Save Image");

           
            clsPerson Person = new clsPerson(
                new PersonDTO(-1,NewPersonDTO.NationalNo,NewPersonDTO.Gender, NewPersonDTO.FirstName,NewPersonDTO.SecondName,
                NewPersonDTO.ThirdName,NewPersonDTO.LastName,NewPersonDTO.Email,NewPersonDTO.Phone,NewPersonDTO.Address,
                NewPersonDTO.DateOfBirth, FilePath, NewPersonDTO.NationalityCountryID)
                );


            if (!Person.Save())
                return StatusCode(409, "Error Add Person ,! no row add");


            

            return CreatedAtRoute("GetPersonByID", new { PersonID = Person.PersonID }, new
            {
                personInfo = Person.personDTO,
                countryInfo = Person.CountryInfo.countryDTO
            });


        }


        [HttpPut("UpdatePerson", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdatePerson(int PersonID,[FromForm] PersonDTO UpdatedPersonDTO, IFormFile? ImageFile)
        {


            


            clsPerson Person = clsPerson.Find(PersonID);
            if (Person == null)
                return NotFound("Person With ID: " + Person + " Not Found!");

            if(Person.Phone!=UpdatedPersonDTO.Phone && !clsValidation.IsNumber(UpdatedPersonDTO.Phone))
                return BadRequest("Phone is not valide");

            if (Person.Gender != (byte)UpdatedPersonDTO.Gender && !Enum.IsDefined(typeof(clsPerson.enGender), (Int32)UpdatedPersonDTO.Gender))
                return BadRequest("Gender Not defind");

            if (Person.Email!=UpdatedPersonDTO.Email && clsPerson.ISPersonExist(UpdatedPersonDTO.Email))
                return BadRequest("Email already Exist");


            if (Person.NationalityCountryID!=UpdatedPersonDTO.NationalityCountryID &&  (clsCountry.Find(UpdatedPersonDTO.NationalityCountryID) == null))
                return NotFound("Country With id " + UpdatedPersonDTO.NationalityCountryID + " Not Found");



            if (Person.NationalNo!=UpdatedPersonDTO.NationalNo &&  clsPerson.ISPersonExist(Convert.ToString(UpdatedPersonDTO.NationalNo)))
                return BadRequest("National Number  already exist");

            string ImagePath = Person.ImagePath;

            if (ImageFile != null && ImageFile.Length != 0)
            {
                if (!ImageFile.ContentType.StartsWith("image/"))
                    return BadRequest("The uploaded file is not an image.");

                if(!clsUtil.DeleteImageFromProjectImagesFolder(Person.ImagePath))
                    return StatusCode(409, "Error delete old image person");
                if((ImagePath = await clsUtil.CopyImageToProjectImagesFolder(ImageFile))==null)
                    return StatusCode(409, "Error change image person");


            }


            Person.NationalNo=UpdatedPersonDTO.NationalNo;
            Person.FirstName=UpdatedPersonDTO.FirstName;
            Person.SecondName=UpdatedPersonDTO.SecondName;
            Person.ThirdName = UpdatedPersonDTO.ThirdName;
            Person.LastName = UpdatedPersonDTO.LastName;
            Person.Email = UpdatedPersonDTO.Email;
            Person.Gender = UpdatedPersonDTO.Gender;
            Person.Address = UpdatedPersonDTO.Address;
            Person.Phone = UpdatedPersonDTO.Phone;
            Person.DateOfBirth = UpdatedPersonDTO.DateOfBirth;
            Person.NationalityCountryID=UpdatedPersonDTO.NationalityCountryID;
            Person.ImagePath = ImagePath;



            if (!Person.Save())
                return StatusCode(409, "Error update Person");




            return Ok( new
            {
                personInfo = Person.personDTO,
                countryInfo = Person.CountryInfo.countryDTO
            });

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
            var MimeType = clsUtil.GetMimeType(FilePath);




            return File(Image, MimeType);
        }

       

    }
}
