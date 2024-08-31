using DVLD.Global.Classes;
using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DVLD.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UsersListDTO>> GetAllUsers()
        {
            List<UsersListDTO> UsersList = clsUser.GetAllUsers();

            if (UsersList.Count == 0)
                return NotFound("Not  Users Found !");

            return Ok(UsersList);
        }



        [HttpGet("GetUserByID/{UserID}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> GetUserByID(int UserID)
        {
            if (UserID < 1)
                return BadRequest("Not Accepted ID : " + UserID);

            clsUser User = clsUser.FindByUserID(UserID);

            if (User == null)
                return NotFound("User With ID : " + UserID + " Not Found !");

            return Ok(User.userDTO);

        }



        [HttpGet("GetUserByPersonID/{PersonID}", Name = "GetUserByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> GetUserByPersonID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            clsUser User = clsUser.FindByPersonID(PersonID);

            if (User == null)
                return NotFound("User With Person ID : " + PersonID + " Not Found !");

            return Ok(User.userDTO);

        }

        [HttpGet("GetUserByUserNameAndPassword/{UserName},{Password}", Name = "GetUserByUserNameAndPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserNameAndPassword(string UserName,string Password)
        {
            string PasswordHashed=clsCryptography.ComputeHash(Password);
            clsUser User = clsUser.FindByUserNameAndPassword(UserName, PasswordHashed);

            if (User == null)
                return NotFound("User With  UserName : " + UserName + " And Password : "+Password+ " Not Found !");

            return Ok(User.userDTO);

        }



        [HttpGet("GetUserByUserName/{UserName}", Name = "GetUserByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserName(string UserName)
        {
            
            clsUser User = clsUser.FindByUserName(UserName);

            if (User == null)
                return NotFound("User With  UserName : " + UserName + " Not Found !");

            return Ok(User.userDTO);

        }



        [HttpGet("IsUserExistByUserID/{UserID}", Name = "IsUserExistByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsUserExistByUserID(int UserID)
        {
            if(UserID < 1)
                return BadRequest("Not Accepted ID : " + UserID);

            bool IsUserExist = clsUser.IsUserExist(UserID);

            if (!IsUserExist)
               return NotFound("User With  ID : " + UserID + " Not Found -(");

            return Ok("User With  ID : " + UserID + " Exist -)");

        }


        [HttpGet("IsUserExistByUserName/{UserName}", Name = "IsUserExistByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult IsUserExistByUserName(string UserName)
        {
            

            bool IsUserExist = clsUser.IsUserExist(UserName);

            if (!IsUserExist)
                return NotFound("User With  UserName : " + UserName + " Not Found -(");

            return Ok("User With  UserName : " + UserName + " Exist -)");

        }

        [HttpGet("IsUserExistByPersonID/{PersonID}", Name = "IsUserExistByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult IsUserExistByPersonID(int PersonID)
        {
            if (PersonID < 1)
                return BadRequest("Not Accepted ID : " + PersonID);

            bool IsUserExist = clsUser.IsUserExist(PersonID);

            if (!IsUserExist)
                return NotFound("User With Person ID : " + PersonID + " Not Found -(");

            return Ok("User With  Person ID : " + PersonID + " Exist -)");

        }

        [HttpDelete("DeleteUser/{UserID}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeleteUser(int UserID)
        {
            if (UserID < 1)
                return BadRequest("Not Accepted ID : " + UserID);

            if(!clsUser.IsUserExist(UserID))
                return NotFound("User With  ID : " + UserID + " Not Found -(");


            if (!clsUser.DeleteUser(UserID))
                return StatusCode(409,"Error Delete User , ! .no row deleted");


            return Ok("User with id : " + UserID + " has been deleted");

        }

        [HttpPost("AddUser",Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UserDTO> AddUser(UserDTO NewUserDTO)
        {
            if (NewUserDTO == null || string.IsNullOrEmpty(NewUserDTO.UserName) ||
                 string.IsNullOrEmpty(NewUserDTO.Password) || NewUserDTO.PersonID<1)
                
                return BadRequest("Invalid User Data !");

            if (!clsPerson.IsPersonExist(NewUserDTO.PersonID))
                return NotFound("Person With ID " + NewUserDTO.PersonID + " Not Found !");



            if (clsUser.IsUserExistForPersonID(NewUserDTO.PersonID))
                return StatusCode(409,"Person With id "+ NewUserDTO.PersonID +" Already User !");


            string PasswordHashed=clsCryptography.ComputeHash(NewUserDTO.Password);
            clsUser User = new clsUser(
                new UserDTO(NewUserDTO.PersonID,NewUserDTO.UserID,NewUserDTO.UserName,
                PasswordHashed, NewUserDTO.IsActive)
                );


            if(!User.Save())
                return StatusCode(409, "Error Add User ,! no row add");


            NewUserDTO.UserID = User.UserID;
            NewUserDTO.Password= PasswordHashed;

            return CreatedAtRoute("GetUserByID", new { UserID = NewUserDTO.UserID }, NewUserDTO);


        }

        [HttpPut("UpdateUser/{UserID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int UserID, UserDTO UpdatedUserDTO)
        {
            if (UpdatedUserDTO == null || string.IsNullOrEmpty(UpdatedUserDTO.UserName) ||
                 string.IsNullOrEmpty(UpdatedUserDTO.Password) || UserID < 1 || UpdatedUserDTO.PersonID<1)

                return BadRequest("Invalid User Data !");


            clsUser User = clsUser.FindByUserID(UserID);

            if (User == null)
                return NotFound("User With ID: " + UserID + " Not Found!");


            if (User.PersonID!=UpdatedUserDTO.PersonID &&  !clsPerson.IsPersonExist(UpdatedUserDTO.PersonID))
                return NotFound("Person With ID : " + UpdatedUserDTO.PersonID + " Not Found !");



            if (User.PersonID != UpdatedUserDTO.PersonID && clsUser.IsUserExistForPersonID(UpdatedUserDTO.PersonID))
                return StatusCode(409, "Person With id " + UpdatedUserDTO.PersonID + " Already User !");


            if ( User.UserName!=UpdatedUserDTO.UserName && clsUser.IsUserExist(UpdatedUserDTO.UserName))
                return BadRequest("User Name Already Exist !");


            User.UserName = UpdatedUserDTO.UserName;
            User.Password = clsCryptography.ComputeHash(UpdatedUserDTO.Password);
            User.IsActive= UpdatedUserDTO.IsActive;
            User.PersonID = UpdatedUserDTO.PersonID;
           
            if (!User.Save())
                return StatusCode(409, "Error Updating User");


            return Ok(User.userDTO);

        }


    }
}
