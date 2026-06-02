namespace Hl7Engine.Core.Application.Message.Dto;

/// <summary>
/// DTO que contiene toda la información extraída de un mensaje HL7v2.
/// </summary>
public  abstract class MessageDto 
{
    private readonly Dictionary<string, string> _fields = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<string> _segments = new();

    public string Format { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;

    public void AddField(string key, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            _fields.TryAdd(key, value);
    }

    public void AddSegment(string segmentName)
    {
        if (!string.IsNullOrWhiteSpace(segmentName))
            _segments.Add(segmentName);
    }

    public bool HasField(string key) => _fields.ContainsKey(key);
    public bool HasSegments(string key) => _segments.Contains(key);
    public string? GetField(string key) => _fields.TryGetValue(key, out var val) ? val : null;

    // Propiedades de solo lectura opcionales para consultas externas
    public IReadOnlyDictionary<string, string> Fields => _fields;
    public IReadOnlyCollection<string> PresentSegments => _segments;
}

/*

    // ====================
    // MSH (todos los mensajes)
    // ====================
    [Hl7Field("MSH-10")] public string MessageId { get; set; }               // MSH-10
    [Hl7Field("MSH-9")] public string MessageType { get; set; }             // MSH-9 completo (ej. "ADT^A01")
    [Hl7Field("MSH-9-1")] public string MessageTypeCode { get; set; }         // MSH-9-1 (ej. "ADT")
    [Hl7Field("MSH-9-2")] public string TriggerEvent { get; set; }            // MSH-9-2 (ej. "A01")
    [Hl7Field("MSH-3-1")] public string SendingApplication { get; set; }      // MSH-3-1
    [Hl7Field("MSH-4-1")] public string SendingFacility { get; set; }         // MSH-4-1
    [Hl7Field("MSH-5-1")] public string ReceivingApplication { get; set; }    // MSH-5-1
    [Hl7Field("MSH-6-1")] public string ReceivingFacility { get; set; }       // MSH-6-1
    [Hl7Field("MSH-7")] public DateTime? MessageDateTime { get; set; }      // MSH-7
    [Hl7Field("MSH-12")] public string? VersionId { get; set; }              // MSH-12

    // ====================
    // EVN (Evento)
    // ====================
    [Hl7Field("EVN-1")] public string EventTypeCode { get; set; }           // EVN-1 (ej. "A01")
    [Hl7Field("EVN-2")] public DateTime? EventDateTime { get; set; }        // EVN-2

    // ====================
    // PID (Paciente) – común en ADT y ORU
    // ====================
    [Hl7Field("PID-3-1")] public string PatientId { get; set; }               // PID-3-1
    [Hl7Field("PID-5-1")] public string PatientName { get; set; }             // PID-5-1 (primer apellido)
    [Hl7Field("PID-7")] public DateTime? DateOfBirth { get; set; }           // PID-7
    [Hl7Field("PID-8")] public string AdministrativeSex { get; set; }       // PID-8

    // ====================
    // PV1 (Visita) – ADT principalmente
    // ====================
    [Hl7Field("PV1-2")] public string PatientClass { get; set; }            // PV1-2 (I/O)
    [Hl7Field("PV1-3-1")] public string AssignedLocation { get; set; }        // PV1-3-1
    [Hl7Field("PV1-44")] public DateTime? AdmissionDate { get; set; }         // PV1-44
    [Hl7Field("PV1-45")] public DateTime? DischargeDate { get; set; }         // PV1-45

    // ====================
    // Segmentos repetibles – ADT
    // ====================
    public List<NextOfKinDto> NextOfKins { get; init; } = new();   // NK1
    public List<InsuranceDto> Insurances { get; init; } = new();    // IN1
    public List<AllergyDto> Allergies { get; init; } = new();       // AL1
    public List<DiagnosisDto> Diagnoses { get; init; } = new();     // DG1

    // ====================
    // ORU – Laboratorio
    // ====================
    [Hl7Field("OBR-2")] public string OrderNumber { get; set; }             // OBR-2 (o lista si se repite)
    public List<ObservationResultDto> ObservationResults { get; init; } = new(); // OBX

    // ====================
    // Segmentos presentes (útil para validación)
    // ====================

}

// ---------- Sub‑DTOs para segmentos repetibles ----------

public class NextOfKinDto
{
    [Hl7Field("NK1-2")] public string Name { get; set; }                    // NK1-2
    [Hl7Field("NK1-3")] public string Relationship { get; set; }            // NK1-3
    [Hl7Field("NK1-5")] public string PhoneNumber { get; set; }             // NK1-5
}

public class InsuranceDto
{
    [Hl7Field("IN1-1")] public string SetId { get; set; }                   // IN1-1
    [Hl7Field("IN1-4")] public string InsuranceCompanyName { get; set; }    // IN1-4
    [Hl7Field("IN1-36")] public string PolicyNumber { get; set; }            // IN1-36
}

public class AllergyDto
{
    [Hl7Field("AL1-1")] public string SetId { get; set; }                   // AL1-1
    [Hl7Field("AL1-2")] public string AllergyType { get; set; }             // AL1-2
    [Hl7Field("AL1-3")] public string AllergyDescription { get; set; }      // AL1-3
}

public class DiagnosisDto
{
    [Hl7Field("DG1-1")] public string SetId { get; set; }                   // DG1-1
    [Hl7Field("DG1-3-1")] public string DiagnosisCode { get; set; }           // DG1-3-1
    [Hl7Field("DG1-3-2")] public string DiagnosisDescription { get; set; }    // DG1-3-2
}

public class ObservationResultDto
{
    [Hl7Field("OBX-3-1")] public string ObservationId { get; set; }           // OBX-3-1
    [Hl7Field("OBX-5")] public string ObservationValue { get; set; }        // OBX-5
    [Hl7Field("OBX-6")] public string Units { get; set; }                   // OBX-6
    [Hl7Field("OBX-7")] public string ReferenceRange { get; set; }          // OBX-7
    [Hl7Field("OBX-14")] public DateTime? ObservationDate { get; set; }       // OBX-14
}*/