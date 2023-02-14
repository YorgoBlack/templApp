using Microsoft.AspNetCore.Mvc;

namespace Templ.API.Controllers;
using Application.Services;
using Application.Dtos;
using System.Runtime.Serialization;
using Templ.Domain.Customers.ValueObjects;

[ApiController]
[Route("[controller]/[action]")]
public partial class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost(Name = "Query")]
    [ProducesResponseType(typeof(CustomerQueryResult), StatusCodes.Status200OK)]
    public IActionResult Query([FromBody] CustomerQueryParams queryParams)
    {
        return Ok(_customerService.Query(queryParams));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid id) 
    {
        var result = await _customerService.GetById(id);
        if( result != null )
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet(Name = "ValidateCustomerName")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateCustomerName(Guid? id, string customerName)
    {
        return Ok(await _customerService.ValidateCustomerName(id, customerName));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get() => Ok(await _customerService.GetAll());

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CustomerDto model) 
        => Ok(await _customerService.Create(model));

    [HttpPut]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put([FromBody] CustomerDto model)
    {
        var result = await _customerService.Update(model);
        if( result != null )
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _customerService.Delete(id);
        if( result > 0)
        {
            return Ok();
        }
        return NotFound(); 
    }
}