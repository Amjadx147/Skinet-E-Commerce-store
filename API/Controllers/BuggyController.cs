using System;
using API.Controllers;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

public class BuggyController : BaseApiController
{
   [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {

        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadeQuest()
    {

        return BadRequest("Not a good request");
    }



      [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {

        return NotFound();
    }


      [HttpGet("internalerror")]
    public IActionResult Getinternalerror()
    {

        throw new Exception(" This is a test exception");
    }



    [HttpPost("validationerror")]
    public IActionResult GetValidationError(CreatProductDTOs product)
    {
        return Ok();
    }

}
