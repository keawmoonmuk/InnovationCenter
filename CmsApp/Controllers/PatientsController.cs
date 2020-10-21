using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        //https://localhost:port/api/Patients/
        //api/<Patients>
        [HttpGet("patientall")]
        public async Task<ActionResult<IEnumerable<Tbl_Patients>>> Getallpatinet()
        {

            var patients  = await _applicationDbContext.Patients.ToListAsync();

            if (patients.Count == 0) {

                return NotFound(HttpStatusCode.NotFound);
            }


            return Ok(patients);

            //var patient = await _applicationDbContext.Patients.Select(p => new Tbl_Patients
            //{
            //    PatientIDcard = p.PatientIDcard,
            //    PatientHN = p.PatientHN,
            //    PatientPrefix = p.PatientPrefix,
            //    PatientFirstName = p.PatientFirstName,
            //    PatientLastName = p.PatientLastName,
            //    DateOfBirth = p.DateOfBirth,
            //    Age = p.Age,
            //    GenderID = p.GenderID,
            //    StatusID = p.StatusID,
            //    Race = p.Race,
            //    Nationality = p.Nationality,
            //    Religion = p.Religion,
            //    PatientAddress = p.PatientAddress,
            //    PatientTel = p.PatientTel,
            //    PatientEmail = p.PatientEmail,
            //    ContactemergencyID = p.ContactemergencyID,
            //    BloodTypeID = p.BloodTypeID,
            //    TreatmentID = p.TreatmentID,
            //    DrugAllergyID = p.DrugAllergyID,
            //    Createby = p.Createby,
            //    CreateDate = p.CreateDate,
            //    UpdateBy = p.UpdateBy,
            //    UpdateDate = p.UpdateDate

            //}).ToListAsync();

            //return patient;
        }

        // GET api/<PatientsController>/idcard
        [HttpGet("patientidcard/{idcard}")]
        public async Task<ActionResult<IEnumerable<Tbl_Patients>>> Getidcard(string idcard)
        {
      
            var getidcardpatient = await _applicationDbContext.Patients.FindAsync(idcard);

            if (getidcardpatient == null) {

                return NotFound();
            }

            return Ok(getidcardpatient);
        }

        //GET Treatmetn  ALL ==>  api/tbl_treatment
        [HttpGet("treatment")]
        public async Task<ActionResult<IEnumerable<Tbl_TreatMent>>> GetTreatment()
        {
            var treatment = await _applicationDbContext.TreatMent.ToListAsync();

            if (treatment == null) return NotFound();

            return Ok(treatment);
        }

        //GET Threatment Id  ==> api/treatment/id
        [HttpGet("treatment/{idtreatment}")]
        public async Task<ActionResult<IEnumerable<Tbl_TreatMent>>> GetIdtreatment(int idtreatment)
        {
            var getIdtreatment = await _applicationDbContext.TreatMent.FindAsync(idtreatment);

            if (getIdtreatment == null) {
                return NotFound();
            }
            return Ok(getIdtreatment);

        }

        //Get Status All ==> api/status
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Tbl_Status>>> GetStatus()
        {
            var getstatus = await _applicationDbContext.Status.ToListAsync();

            if (getstatus == null) {
                return NotFound();
            }
            return Ok(getstatus);
        }

        //get id status  ==> api/patient/status/{id}
        [HttpGet("status/{idstatus}")]
        public async Task<ActionResult<IEnumerable<Tbl_Status>>> GetIdstatus(int idstatus)
        {
            var getIdstatus = await _applicationDbContext.Status.FindAsync(idstatus);

            if (getIdstatus == null) {

                var notdata = "not data in table on database";
                return Ok(notdata);
            }

            return Ok(getIdstatus);


        }

        //get Finance  ==> api/patient/finance
        [HttpGet("finances")]
        public async Task<ActionResult<IEnumerable<Tbl_Finance>>> Getdatafinance()
        {
            var getdatafinances = await _applicationDbContext.Finances.ToListAsync();

            Console.WriteLine(getdatafinances);

            if (getdatafinances.Count == 0) {

                return NotFound(HttpStatusCode.NotFound);
            }
            return Ok(getdatafinances);
        }

        //get id finance ==> api/patient/finace/idfinance
        [HttpGet("finance/{idfinance}")]
        public async Task<ActionResult<IEnumerable<Tbl_Finance>>> GetIdfinance(int idfinance)
        {
            var getidfinances = await _applicationDbContext.Finances.FindAsync(idfinance);

            if (getidfinances == null) {

                return NotFound(HttpStatusCode.NotFound);
            }

            return Ok(getidfinances);
        }

        //get contextemergencys all
        [HttpGet("contextemergencyall")]
        public async Task<ActionResult<IEnumerable<Tbl_ContactEmergency>>> GetAllcontextEmergency()
        {
            var getallcontextemergency = await _applicationDbContext.ContextEmergencys.ToListAsync();

            Console.WriteLine(getallcontextemergency);

            if(getallcontextemergency == null) {

               // var notdata = "No data in table database... ";

                return NotFound(HttpStatusCode.NotFound);
            }

            return Ok(getallcontextemergency);
        }
      
        //get id contextemergency id
        [HttpGet("contextemergencyid/{idcontextemergency}")]
        public async Task<ActionResult<IEnumerable<Tbl_ContactEmergency>>> GetIdcontextemergency (int idcontactemergency)
        {
            var getidcontextemergency = await _applicationDbContext.ContextEmergencys.FindAsync(idcontactemergency);

            Console.WriteLine(getidcontextemergency);

            if(getidcontextemergency == null) {

               // var notdata = "No data in table database";
                return NotFound(HttpStatusCode.NotFound);
            }

            return Ok(getidcontextemergency);

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
