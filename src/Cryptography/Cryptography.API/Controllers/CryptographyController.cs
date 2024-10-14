using Cryptography.API.DTOs;
using Cryptography.Entities;
using Cryptography.Repositories;
using Cryptography.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cryptography.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptographyController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;

    public CryptographyController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _paymentRepository.GetAllAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPaymentById([FromRoute]int id)
    {
        try
        {
            var paymentEntity = await _paymentRepository.FindAsync(id);
            var res = new PaymentRS
            {
                Id = paymentEntity.Id,
                UserDocument = paymentEntity.UserDocument,
                CreditCardToken = paymentEntity.CreditCardToken,
                Value = paymentEntity.Value
            };
            
            return Ok(res);
        }
        catch (Exception ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentRQ req)
    {
        try
        {
            var payment = new Payment
            {
                UserDocument = Convert.ToBase64String(CryptographyServices.GeneratePbkdf2Hash(req.UserDocument)),
                CreditCardToken = Convert.ToBase64String(CryptographyServices.GeneratePbkdf2Hash(req.CreditCardToken)),
                Value = req.Value
            };

            await _paymentRepository.CreateAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PaymentRQ req)
    {
        try
        {
            var payment = await _paymentRepository.FindAsync(id);

            payment.UserDocument = string.IsNullOrEmpty(req.UserDocument) ? payment.UserDocument : req.UserDocument;
            payment.CreditCardToken = string.IsNullOrEmpty(req.CreditCardToken)
                ? payment.CreditCardToken
                : req.CreditCardToken;
            payment.Value = req.Value > 0 ? req.Value : payment.Value;

            await _paymentRepository.UpdateAsync(payment);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        try
        {
            var payment = await _paymentRepository.FindAsync(id);
            if (payment.Id == 0)
                return NotFound();

            await _paymentRepository.RemoveAsync(payment.Id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}