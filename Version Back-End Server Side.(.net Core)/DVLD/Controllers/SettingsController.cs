using DVLD_Buisness;
using DVLD_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("api/Settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpGet("GetDefaultValidityLengthForAnInternationalLicense", Name = "GetDefaultValidityLengthForAnInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<byte> GetDefaultValidityLengthForAnInternationalLicense()
        {
            byte DefaultValidityLengthForAnInternationalLicense = clsSettingData.GetDefaultValidityLengthForAnInternationalLicense();

            return Ok(DefaultValidityLengthForAnInternationalLicense);

        }
    }
}
