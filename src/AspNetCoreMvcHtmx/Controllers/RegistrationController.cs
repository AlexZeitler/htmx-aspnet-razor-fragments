using AspNetCoreMvcHtmx.Models;
using Easy_Password_Validator;
using Easy_Password_Validator.Models;
using Microsoft.AspNetCore.Mvc;
using static AspNetCoreMvcHtmx.Controllers.Constants;

namespace AspNetCoreMvcHtmx.Controllers;

public class RegistrationController : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  [Route("/api/registration")] //same route but different "Consumes"
  [Consumes(FormUrlEncoded)]
  public ActionResult PostForm([FromForm] Registration registration)
  {
    if (!ModelState.IsValid)
    {
      return PartialView("_RegistrationForm", registration);
    }


    return PartialView("_RegistrationSucceeded", registration);
  }

  [HttpPost]
  [Route("/api/registration")]
  [Consumes(ApplicationJson)]
  public ActionResult PostJson([FromBody] Registration registration)
  {
    if (!ModelState.IsValid)
    {
      var validation = new ValidationProblemDetails(ModelState)
      {
        Status = StatusCodes.Status422UnprocessableEntity
      };
      return UnprocessableEntity(validation);
    }


    return Created($"/user/{Guid.NewGuid()}", new { registration.FirstName });
  }

  [HttpPost]
  [Route("/api/passwordvalidation", Name = "passwordvalidation")]
  public IActionResult PasswordValidation([FromForm] PasswordValidation passwordValidation)
  {
    if (string.IsNullOrWhiteSpace(passwordValidation.Password))
    {
      return PartialView("_PasswordStrength", new PasswordValidation() { PasswordScore = null });
    }

    var validator = new PasswordValidatorService(new PasswordRequirements());
    validator.TestAndScore(passwordValidation.Password);
    return PartialView("_PasswordStrength", new PasswordValidation() { PasswordScore = validator.Score });
  }
}
