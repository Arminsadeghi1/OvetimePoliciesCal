﻿using Microsoft.AspNetCore.Mvc;
using OvetimePolicies_Core.Dtos;
using OvetimePolicies_Data.Handlers;

namespace OvetimePolicies_api.Controllers;

[Route("{datatype}/[controller]/")]
[ApiController]
public class OvetimePoliciesController : ControllerBase
{
    public OvetimePoliciesController()
    {

    }

    [HttpPost("add")]
    [Consumes("application/custom")]
    public async Task<ActionResult> OvetimePolicies([FromBody] AddCommandDto command, [FromServices] AddSalaryHandler handler)
    {
        try
        {
            return Ok(handler.AddSalary(command));
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpPost("edit")]
    [Consumes("application/custom")]
    public async Task<ActionResult> EditSalary([FromBody] EditPersonDto command, [FromServices] EditSalaryHandler handler)
    {
        try
        {
            return Ok(handler.Handle(command));
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpPost("delete/{id}")]
    public async Task<ActionResult> DeleteSalary([FromRoute] Guid id, [FromServices] DeleteSalaryHandler handler)
    {
        try
        {
            return Ok(handler.Handle(id));
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult> Get([FromRoute] Guid id, [FromServices] GetByIdHandler handler)
    {
        try
        {
            return Ok(handler.Handle(id));
        }
        catch
        {
            return NoContent();
        }
    }
    [HttpGet("get-all")]
    public async Task<ActionResult> GetAll([FromServices] GetAllHandler handler)
    {
        try
        {
            return Ok(handler.Handle());
        }
        catch
        {
            return NoContent();
        }
    }

}
