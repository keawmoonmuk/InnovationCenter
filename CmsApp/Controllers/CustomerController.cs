
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using CmsApp.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CmsApp.Helpers;

namespace CmsApp.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;               //IMapper   
        private readonly IUnitOfWork _unitOfWork;       //IUnitOfWork
        private readonly ILogger _logger;               //logger
        private readonly IEmailSender _emailSender;     //IEmailSender


        public CustomerController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IEmailSender emailSender)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var allCustomers = _unitOfWork.Customers.GetAllCustomersData();
            return Ok(_mapper.Map<IEnumerable<CustomerViewModel>>(allCustomers));
        }


        //GET : api/Customer/throw
        [HttpGet("throw")]
        public IEnumerable<CustomerViewModel> Throw()
        {
            throw new InvalidOperationException("This is a test exception: " + DateTime.Now);
        }


        //GET : api/Customer/email
        [HttpGet("email")]
        public async Task<string> Email()
        {
            string recepientName = "Kreangkrai";                //  <===== Put the recepient's name here
            string recepientEmail = "keawmoonmuk.k@gmail.com"; //   <===== Put the recepient's email here

            string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

            (bool success, string errorMsg) = await _emailSender.SendEmailAsync(recepientName, recepientEmail, "Test Email from QuickApp", message);

            if (success)
                return "Success";

            return "Error: " + errorMsg;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value: " + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
