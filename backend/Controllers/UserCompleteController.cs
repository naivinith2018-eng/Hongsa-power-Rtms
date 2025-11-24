using Microsoft.AspNetCore.Mvc;
using Hongsa.Rtms.Api.Models;

namespace Hongsa.Rtms.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // api/User
 public class UserCompleteController: ControllerBase

{
    [HttpGet]

    public string GetUser()
    {
       return "Hello User" ;
    }

    [HttpGet ("WelcomeUser")]

    public string WelcomeUser()
    {
       return "Welcome User" ;
    }

    [HttpPost ("PostUser")]

    public string PostUser()
    {
       return "Post User" ;
    }

    [HttpPut ("PutUser")]

    public string PutUser()
    {
       return "Put User" ;
    }

    [HttpDelete ("DeleteUser")]

    public string DeleteUser()
    {
       return "Delete User" ;
    }

}