using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using DAL.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsApp.Controllers {

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase {

        private readonly ApplicationDbContext _applicationDbContext;

        //Create contrutor 
        public PatientsController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tbl_Patients>>> Getpatinet()
        {

            var patient = await _applicationDbContext.Patients.Select(p => new Tbl_Patients
                            {
                                PatientIDcard = p.PatientIDcard,
                                PatientHN = p.PatientHN,
                                PatientPrefix = p.PatientPrefix,
                                PatientFirstName = p.PatientFirstName,
                                PatientLastName = p.PatientLastName,
                                DateOfBirth = p.DateOfBirth,
                                Age = p.Age,
                                GenderID = p.GenderID,
                                StatusID = p.StatusID,
                                Race = p.Race,
                                Nationality = p.Nationality,
                                Religion = p.Religion,
                                PatientAddress = p.PatientAddress,
                                PatientTel = p.PatientTel,
                                PatientEmail = p.PatientEmail,
                                ContactemergencyID = p.ContactemergencyID,
                                BloodTypeID = p.BloodTypeID,
                                TreatmentID = p.TreatmentID,
                                DrugAllergyID = p.DrugAllergyID,
                                Createby = p.Createby,
                                CreateDate = p.CreateDate,
                                UpdateBy = p.UpdateBy,
                                UpdateDate = p.UpdateDate

                            }).ToListAsync();
                          
            return patient;
        }
        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
