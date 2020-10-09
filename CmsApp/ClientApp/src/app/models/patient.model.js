"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Patient = /** @class */ (function () {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    function Patient(patientIDcard, patientHN, patientprefix, patientFirstname, patientLastname, dateOfbrith, age) {
        this.patientIDcard = patientIDcard;
        this.patientHN = patientHN;
        this.patientprefix = patientprefix;
        this.patientFirstname = patientFirstname;
        this.patientLastname = patientLastname;
        this.dateOfbrith = dateOfbrith;
        this.age = age;
    }
    return Patient;
}());
exports.Patient = Patient;
//# sourceMappingURL=patient.model.js.map